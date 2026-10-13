using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Naninovel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    /// <summary>Changes only the two portrait sprites on a spawned small cut-in; never edits its prefab or transforms.</summary>
    public sealed class CorrectedCutInPortraitAdapter : MonoBehaviour
    {
        public const string AddressPrefix = "Naninovel/CorrectedCutInPortraits/";
        public static readonly string[] Characters = {
            "Alisa", "AnAn", "Coco", "Ema", "Hanna", "Hiro", "Leia", "Margo", "Meruru", "Miria", "Nanoka", "Noah", "Sherry"
        };

        public string Character { get; private set; }
        public Sprite OriginalSprite { get; private set; }
        public Sprite CorrectedSprite => held?.Asset;
        public bool UsingCorrected => held != null && main && shadow && main.sprite == held.Asset && shadow.sprite == held.Asset;
        public bool HasResourceHandle => leases.Any(lease => lease.Valid);
        public bool HasPendingLoad => pending != null;
        public string LastFailure { get; private set; }

        private sealed class Lease
        {
            private AsyncOperationHandle<Sprite> handle;
            private bool released;
            public bool Valid => !released && handle.IsValid();
            public bool Done => Valid && handle.IsDone;
            public Sprite Asset => Valid && handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null;
            public Lease(string address) { handle = Addressables.LoadAssetAsync<Sprite>(address); }
            public void Release()
            {
                if (released) return;
                released = true;
                if (handle.IsValid()) Addressables.Release(handle);
            }
        }

        private readonly HashSet<Lease> leases = new HashSet<Lease>();
        private SpriteRenderer main, shadow;
        private EnhancementSettings settings;
        private bool subscribed;
        private Lease held;
        private CancellationTokenSource pending;
        private int epoch;

        /// <summary>Returns null for every prefab outside the exact thirteen small cut-ins.</summary>
        public static CorrectedCutInPortraitAdapter Attach(GameObject root)
        {
            if (!root) return null;
            string name = root.name.EndsWith("(Clone)", StringComparison.Ordinal)
                ? root.name.Substring(0, root.name.Length - 7).TrimEnd() : root.name;
            const string prefix = "CutinSmall_";
            if (!name.StartsWith(prefix, StringComparison.Ordinal)) return null;
            string character = name.Substring(prefix.Length);
            // The shipped prefab uses Anan; its portrait texture and reviewed payload use AnAn.
            if (character == "Anan") character = "AnAn";
            if (!Characters.Contains(character)) return null;
            var existing = root.GetComponent<CorrectedCutInPortraitAdapter>();
            if (existing) return existing;
            var foreground = root.transform.Find("CharaRig/Chara")?.GetComponent<SpriteRenderer>();
            var silhouette = root.transform.Find("CharaRig/Chara_Shadow")?.GetComponent<SpriteRenderer>();
            if (!foreground || !silhouette || !foreground.sprite || foreground.sprite != silhouette.sprite)
            {
                Debug.LogWarning("Corrected portrait left the original cut-in unchanged: missing or differing sprite slots on " + name);
                return null;
            }
            var adapter = root.AddComponent<CorrectedCutInPortraitAdapter>();
            adapter.Character = character;
            adapter.main = foreground;
            adapter.shadow = silhouette;
            adapter.OriginalSprite = foreground.sprite;
            adapter.Subscribe();
            return adapter;
        }

        /// <summary>Waits for this instance's selection before its existing Timeline starts.</summary>
        public async UniTask ApplyCurrentSetting(AsyncToken token = default)
        {
            token.ThrowIfCanceled();
            Subscribe();
            if (!isActiveAndEnabled || settings == null || !settings.UseCorrectedCutInPortraits)
            {
                RestoreAndRelease();
                return;
            }
            if (UsingCorrected) return;
            // A pooled OnEnable and its production AwaitSpawn may arrive together.
            // Share the existing request instead of canceling the caller that is
            // waiting to start the Timeline. Each caller still observes its own token.
            if (pending != null)
            {
                var existing = pending;
                int existingRequest = epoch;
                try
                {
                    await UniTask.WaitUntil(() => !this || pending != existing || existingRequest != epoch || UsingCorrected,
                        cancellationToken: token.CancellationToken);
                    token.ThrowIfCanceled();
                }
                catch (OperationCanceledException)
                {
                    if (token.Canceled && existingRequest == epoch) RestoreAndRelease();
                    token.ThrowIfCanceled();
                }
                // An actual portrait change may have superseded the request. Await
                // its replacement while corrected art is still the selected mode.
                if (this && isActiveAndEnabled && settings != null && settings.UseCorrectedCutInPortraits &&
                    !UsingCorrected && pending != null && existingRequest != epoch)
                    await ApplyCurrentSetting(token);
                return;
            }
            RestoreAndRelease();
            int request = epoch;
            LastFailure = null;
            var cancellation = CancellationTokenSource.CreateLinkedTokenSource(token.CancellationToken);
            pending = cancellation;
            Lease lease = null;
            bool adopted = false;
            try
            {
                lease = new Lease(AddressPrefix + Character);
                leases.Add(lease);
                await UniTask.WaitUntil(() => lease.Done, cancellationToken: cancellation.Token);
                token.ThrowIfCanceled();
                if (this && request == epoch && isActiveAndEnabled && settings != null && settings.UseCorrectedCutInPortraits)
                {
                    Sprite sprite = lease.Asset;
                    if (!sprite || sprite.rect.width <= 0 || sprite.rect.height <= 0 || sprite.pixelsPerUnit <= 0)
                        throw new InvalidOperationException("The corrected sprite could not be loaded: " + AddressPrefix + Character);
                    main.sprite = sprite;
                    shadow.sprite = sprite;
                    held = lease;
                    adopted = true;
                    LastFailure = null;
                }
            }
            catch (OperationCanceledException)
            {
                // A user preference change/disposal is normal; caller cancellation must still stop the spawn.
                token.ThrowIfCanceled();
            }
            catch (Exception error)
            {
                if (this && request == epoch)
                {
                    LastFailure = error.Message;
                    RestoreSprites();
                    Debug.LogWarning("Corrected portrait unavailable; retaining the original " + Character + ": " + error.Message);
                }
            }
            finally
            {
                if (!adopted && lease != null) { lease.Release(); leases.Remove(lease); }
                if (ReferenceEquals(pending, cancellation)) pending = null;
                cancellation.Dispose();
            }
            if (this && isActiveAndEnabled && settings != null && settings.UseCorrectedCutInPortraits &&
                !UsingCorrected && pending != null && request != epoch)
                await ApplyCurrentSetting(token);
        }

        private void Subscribe()
        {
            var current = Engine.GetService<EnhancementSettings>();
            if (!ReferenceEquals(current, settings))
            {
                if (settings != null && subscribed) settings.Changed -= HandleSettingChanged;
                subscribed = false;
                settings = current;
            }
            if (settings != null && isActiveAndEnabled && !subscribed)
            {
                settings.Changed += HandleSettingChanged;
                subscribed = true;
            }
        }

        private void HandleSettingChanged()
        {
            if (!isActiveAndEnabled || string.IsNullOrEmpty(Character)) return;
            if (settings == null || !settings.UseCorrectedCutInPortraits) RestoreAndRelease();
            // Changed covers both preferences. Toggling the epilogue must not
            // restart an in-flight portrait load or release its current sprite.
            else if (!UsingCorrected && pending == null) ApplyCurrentSetting().Forget();
        }

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(Character)) return;
            Subscribe();
            HandleSettingChanged();
        }

        private void RestoreSprites()
        {
            if (main) main.sprite = OriginalSprite;
            if (shadow) shadow.sprite = OriginalSprite;
        }

        /// <summary>Immediately restores both original references and releases pending and active loads.</summary>
        public void RestoreAndRelease()
        {
            ++epoch;
            var previous = pending;
            pending = null;
            previous?.Cancel();
            RestoreSprites();
            held = null;
            foreach (Lease lease in leases.ToArray()) lease.Release();
            leases.Clear();
        }

        private void OnDisable()
        {
            if (settings != null && subscribed) settings.Changed -= HandleSettingChanged;
            subscribed = false;
            settings = null;
            RestoreAndRelease();
        }

        private void OnDestroy() => OnDisable();
    }
}
