using System;
using System.Threading;
using Naninovel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace WitchTrials.Views
{
    /// <summary>Owns the optional epilogue's transient audio and effects; never writes progression or saves.</summary>
    public static class FireworksEpilogueSession
    {
        public const string ScriptPath = "System/System_FireworksEpilogue";
        public const string BgmLoop = "Bgm_40_1_Loop";
        public const string BgmIntro = "Bgm_40_1_Intro";
        public const string EffectPath = "TitleScreenEffect_3";

        public static bool Active { get; private set; }
        public static string Phase { get; private set; } = "inactive";
        public static float TargetVolume { get; private set; }
        public static bool CreditsVisible => credits && credits.Visible;
        // Also invalidates an older delayed autosave after the session has already ended.
        internal static int SaveEpoch { get; private set; }

        private static CancellationTokenSource lifetime;
        private static IAudioManager audio;
        private static ISpawnManager spawns;
        private static IStateManager state;
        private static IScriptPlayer player;
        private static bool musicStarted;
        private static FireworksEpilogueCreditsOverlay credits;

        public static async UniTask ShowCredits(AsyncToken token = default)
        {
            if (!Active) throw new InvalidOperationException("The fireworks epilogue has not begun.");
            if (credits) return;
            using var linked = CancellationTokenSource.CreateLinkedTokenSource(lifetime.Token, token.CancellationToken);
            var handle = Addressables.LoadAssetAsync<Texture2D>(FireworksEpilogueCreditsOverlay.TextureAddress);
            bool adopted = false;
            try
            {
                await UniTask.WaitUntil(() => handle.IsDone, cancellationToken: linked.Token);
                linked.Token.ThrowIfCancellationRequested();
                if (handle.Status != AsyncOperationStatus.Succeeded || !handle.Result ||
                    handle.Result.width != 1260 || handle.Result.height != 709)
                    throw new InvalidOperationException("The original 1260 x 709 epilogue attribution poster is missing or changed.");
                credits = FireworksEpilogueCreditsOverlay.Create(handle);
                adopted = true;
            }
            finally { if (!adopted && handle.IsValid()) Addressables.Release(handle); }
        }

        public static async UniTask Begin(AsyncToken token = default)
        {
            Exit();
            token.ThrowIfCanceled();
            audio = Engine.GetServiceOrErr<IAudioManager>();
            spawns = Engine.GetServiceOrErr<ISpawnManager>();
            state = Engine.GetServiceOrErr<IStateManager>();
            player = Engine.GetServiceOrErr<IScriptPlayer>();
            lifetime = new CancellationTokenSource();
            Active = true;
            Phase = "empty";
            TargetVolume = 0;
            musicStarted = false;
            SaveEpoch++;
            state.OnResetStarted += Exit;
            state.OnGameLoadStarted += HandleLoad;
            player.OnPlay += HandlePlay;
            Engine.OnDestroyed += HandleEngineDestroyed;
            int epoch = SaveEpoch;
            try
            {
                using var linked = CancellationTokenSource.CreateLinkedTokenSource(lifetime.Token, token.CancellationToken);
                // Both the ending and the title can leave music/effects behind. The empty scene is silent.
                audio.StopVoice();
                DestroyEffect();
                await audio.StopAllBgm(token: new AsyncToken(linked.Token, token.CompletionToken));
            }
            catch
            {
                if (SaveEpoch == epoch) Exit();
                throw;
            }
        }

        public static async UniTask SetMood(string mood, AsyncToken token = default)
        {
            if (!Active) throw new InvalidOperationException("The fireworks epilogue has not begun.");
            float volume;
            float duration;
            switch (mood)
            {
                case "reveal": volume = .65f; duration = 2f; break;
                case "quiet": volume = .40f; duration = 1f; break;
                case "together": volume = .65f; duration = 2f; break;
                default: throw new ArgumentException("Unknown fireworks mood: " + mood, nameof(mood));
            }
            if (!musicStarted && mood != "reveal")
                throw new InvalidOperationException("Reveal must start the fireworks music before changing its volume.");
            int epoch = SaveEpoch;
            using var linked = CancellationTokenSource.CreateLinkedTokenSource(lifetime.Token, token.CancellationToken);
            var linkedToken = new AsyncToken(linked.Token, token.CompletionToken);
            Phase = mood;
            TargetVolume = volume;
            try
            {
                linkedToken.ThrowIfCanceled();
                if (!musicStarted)
                {
                    musicStarted = true;
                    // Per-track gain is multiplied by the existing BGM/master mixer settings.
                    // Never change those settings, and never replay the Intro on subsequent mood changes.
                    await audio.PlayBgm(BgmLoop, volume, duration, true, BgmIntro, token: linkedToken);
                }
                else await audio.ModifyBgm(BgmLoop, volume, true, duration, linkedToken);
            }
            catch
            {
                if (SaveEpoch == epoch) Exit();
                throw;
            }
        }

        public static async UniTask Finish(AsyncToken token = default)
        {
            if (!Active) return;
            int epoch = SaveEpoch;
            using var linked = CancellationTokenSource.CreateLinkedTokenSource(lifetime.Token, token.CancellationToken);
            Phase = "ending";
            TargetVolume = 0;
            try
            {
                var linkedToken = new AsyncToken(linked.Token, token.CompletionToken);
                await UniTask.WhenAll(musicStarted ? audio.StopBgm(BgmLoop, 3f, linkedToken) : UniTask.CompletedTask,
                    credits ? credits.FadeOut(3f, linkedToken) : UniTask.CompletedTask);
                linked.Token.ThrowIfCancellationRequested();
                DestroyEffect();
                if (credits) UnityEngine.Object.Destroy(credits.gameObject);
                credits = null;
                musicStarted = false;
                Phase = "finished";
                // Keep save protection until ReturnToTitle/reset actually leaves this script.
            }
            catch
            {
                if (SaveEpoch == epoch) Exit();
                throw;
            }
        }

        public static void Exit() => Cleanup(true);

        private static void Cleanup(bool servicesAvailable)
        {
            if (!Active) return;
            var ownedLifetime = lifetime;
            var ownedAudio = audio;
            var ownedSpawns = spawns;
            var ownedState = state;
            var ownedPlayer = player;
            var ownedCredits = credits;
            bool ownedMusic = musicStarted;
            Active = false;
            int exitEpoch = ++SaveEpoch;
            Phase = "inactive";
            TargetVolume = 0;
            lifetime = null;
            musicStarted = false;
            audio = null;
            spawns = null;
            state = null;
            player = null;
            credits = null;
            ownedState.OnResetStarted -= Exit;
            ownedState.OnGameLoadStarted -= HandleLoad;
            ownedPlayer.OnPlay -= HandlePlay;
            Engine.OnDestroyed -= HandleEngineDestroyed;
            // Detach first: cancellation can resume an older awaiter inline. It must never dispose
            // a replacement lifetime or stop the new session's track/effect through these static fields.
            ownedLifetime?.Cancel();
            ownedLifetime?.Dispose();
            if (ownedCredits) UnityEngine.Object.Destroy(ownedCredits.gameObject);
            if (servicesAvailable && SaveEpoch == exitEpoch)
            {
                if (ownedSpawns.IsSpawned(EffectPath)) ownedSpawns.DestroySpawned(EffectPath);
                // Zero-duration StopBgm stops the track/tweener synchronously before returning its task.
                if (ownedMusic) ownedAudio.StopBgm(BgmLoop).Forget();
                if (SaveEpoch == exitEpoch) ownedAudio.StopVoice();
            }
        }

        private static void DestroyEffect()
        {
            if (spawns != null && spawns.IsSpawned(EffectPath)) spawns.DestroySpawned(EffectPath);
        }

        private static void HandleLoad(GameSaveLoadArgs args) => Exit();
        private static void HandlePlay(Script script)
        {
            // UI commands may use transient scripts without leaving the current scenario.
            if (script && script.Path != ScriptPath && !script.Path.StartsWith(Script.TransientPathPrefix, StringComparison.Ordinal))
                Exit();
        }
        private static void HandleEngineDestroyed() => Cleanup(false);
    }
}
