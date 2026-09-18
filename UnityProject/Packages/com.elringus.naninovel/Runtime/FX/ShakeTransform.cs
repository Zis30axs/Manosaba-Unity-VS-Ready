using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel.FX
{
    /// <summary>
    /// Shakes a <see cref="Transform"/>.
    /// </summary>
    public abstract class ShakeTransform : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable
    {
        public virtual string SpawnedPath { get; private set; }
        public virtual string ObjectName { get; private set; }
        public virtual int ShakesCount { get; private set; }
        public virtual float ShakeDuration { get; private set; }
        public virtual float DurationVariation { get; private set; }
        public virtual float ShakeAmplitude { get; private set; }
        public virtual float AmplitudeVariation { get; private set; }
        public virtual bool ShakeHorizontally { get; private set; }
        public virtual bool ShakeVertically { get; private set; }

        protected virtual int DefaultShakesCount => defaultShakesCount;
        protected virtual float DefaultShakeDuration => defaultShakeDuration;
        protected virtual float DefaultDurationVariation => defaultDurationVariation;
        protected virtual float DefaultShakeAmplitude => defaultShakeAmplitude;
        protected virtual float DefaultAmplitudeVariation => defaultAmplitudeVariation;
        protected virtual bool DefaultShakeHorizontally => defaultShakeHorizontally;
        protected virtual bool DefaultShakeVertically => defaultShakeVertically;

        protected virtual ISpawnManager SpawnManager => Engine.GetServiceOrErr<ISpawnManager>();
        protected virtual Vector3 DeltaPos { get; private set; }
        protected virtual Vector3 InitialPos { get; private set; }
        protected virtual Transform ShakenTransform { get; private set; }
        protected virtual bool Loop { get; private set; }
        protected virtual Tweener<VectorTween> PositionTweener { get; } = new();
        protected virtual CancellationTokenSource CTS { get; private set; }

        [SerializeField] private int defaultShakesCount = 3;
        [SerializeField] private float defaultShakeDuration = .15f;
        [SerializeField] private float defaultDurationVariation = .25f;
        [SerializeField] private float defaultShakeAmplitude = .5f;
        [SerializeField] private float defaultAmplitudeVariation = .5f;
        [SerializeField] private bool defaultShakeHorizontally;
        [SerializeField] private bool defaultShakeVertically = true;

        private Vector3 lastChangedPosition;

        public virtual void SetSpawnParameters (IReadOnlyList<string> parameters, bool asap)
        {
            if (PositionTweener.Running)
                PositionTweener.CompleteInstantly();
            if (ShakenTransform)
                SetPositionSafe(InitialPos);

            SpawnedPath = gameObject.name;
            ObjectName = parameters?.ElementAtOrDefault(0);
            ShakesCount = Mathf.Abs(parameters?.ElementAtOrDefault(1)?.AsInvariantInt() ?? DefaultShakesCount);
            ShakeDuration = Mathf.Abs(parameters?.ElementAtOrDefault(2)?.AsInvariantFloat() ?? DefaultShakeDuration);
            DurationVariation = Mathf.Clamp01(parameters?.ElementAtOrDefault(3)?.AsInvariantFloat() ?? DefaultDurationVariation);
            ShakeAmplitude = Mathf.Abs(parameters?.ElementAtOrDefault(4)?.AsInvariantFloat() ?? DefaultShakeAmplitude);
            AmplitudeVariation = Mathf.Clamp01(parameters?.ElementAtOrDefault(5)?.AsInvariantFloat() ?? DefaultAmplitudeVariation);
            ShakeHorizontally = bool.Parse(parameters?.ElementAtOrDefault(6) ?? DefaultShakeHorizontally.ToString());
            ShakeVertically = bool.Parse(parameters?.ElementAtOrDefault(7) ?? DefaultShakeVertically.ToString());
            Loop = ShakesCount <= 0;
        }

        public virtual async UniTask AwaitSpawn (AsyncToken token = default)
        {
            ShakenTransform = GetShakenTransform();
            if (!ShakenTransform)
            {
                SpawnManager.DestroySpawned(SpawnedPath);
                Engine.Warn($"Failed to apply '{GetType().Name}' FX to '{ObjectName}': transform to shake not found.");
                return;
            }

            token = InitializeCTS(token);
            lastChangedPosition = InitialPos = ShakenTransform.position;
            DeltaPos = new(ShakeHorizontally ? ShakeAmplitude : 0, ShakeVertically ? ShakeAmplitude : 0, 0);

            if (Loop) LoopRoutine(token).Forget();
            else
            {
                for (int i = 0; i < ShakesCount; i++)
                    await ShakeSequence(token);
                if (SpawnManager.IsSpawned(SpawnedPath))
                    SpawnManager.DestroySpawned(SpawnedPath);
            }

            await AsyncUtils.WaitEndOfFrame(token); // Otherwise consequent shake won't work.
        }

        protected abstract Transform GetShakenTransform ();

        protected virtual async UniTask ShakeSequence (AsyncToken token)
        {
            var amplitude = DeltaPos + DeltaPos * Random.Range(-AmplitudeVariation, AmplitudeVariation);
            var duration = ShakeDuration + ShakeDuration * Random.Range(-DurationVariation, DurationVariation);
            await Move(InitialPos - amplitude * .5f, duration * .25f, token);
            await Move(InitialPos + amplitude, duration * .5f, token);
            await Move(InitialPos, duration * .25f, token);
        }

        protected virtual async UniTask Move (Vector3 position, float duration, AsyncToken token)
        {
            var tween = new VectorTween(ShakenTransform.position, position, new(duration, EasingType.SmoothStep), SetPositionSafe);
            await PositionTweener.RunAwaitable(tween, token, ShakenTransform);
        }

        protected virtual void OnDestroy ()
        {
            Loop = false;
            CTS?.Cancel();
            CTS?.Dispose();

            SetPositionSafe(InitialPos);

            if (Engine.Initialized && SpawnManager.IsSpawned(SpawnedPath))
                SpawnManager.DestroySpawned(SpawnedPath);
        }

        protected virtual async UniTaskVoid LoopRoutine (AsyncToken token)
        {
            while (Loop && Application.isPlaying && token.EnsureNotCanceledOrCompleted())
                await ShakeSequence(token);
        }

        protected virtual AsyncToken InitializeCTS (AsyncToken token)
        {
            CTS?.Cancel();
            CTS?.Dispose();
            CTS = CancellationTokenSource.CreateLinkedTokenSource(token.CancellationToken);
            return new(CTS.Token, token.CompletionToken);
        }

        protected virtual void SetPositionSafe (Vector3 pos)
        {
            if (!ShakenTransform) return;

            // Only change the position in case current value is the same as it was when we last changed it.
            // When different, it means something else has modified the position and we should no longer touch it.
            // Happens when fast-forwarding through un-awaited @char and @shake commands.
            if (ShakenTransform.position != lastChangedPosition)
            {
                PositionTweener.Stop();
                return;
            }

            lastChangedPosition = ShakenTransform.position = pos;
        }
    }
}
