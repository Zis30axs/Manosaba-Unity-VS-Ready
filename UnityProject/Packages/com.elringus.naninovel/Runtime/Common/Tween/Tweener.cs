using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Performs tween animation of a <see cref="ITweenValue"/>.
    /// </summary>
    public interface ITweener<TTweenValue>
        where TTweenValue : struct, ITweenValue
    {
        TTweenValue Tween { get; }
        bool Running { get; }

        void Run (TTweenValue tween, AsyncToken token = default, UnityEngine.Object target = default);
        /// <remarks>This may allocate <see cref="UniTask"/>, so use Run() when the animation don't have to be awaited.</remarks>
        UniTask RunAwaitable (TTweenValue tween, AsyncToken token = default, UnityEngine.Object target = default);
        void Stop ();
        void CompleteInstantly ();
    }

    /// <inheritdoc cref="ITweener{TTweenValue}"/>
    public class Tweener<TTweenValue> : ITweener<TTweenValue>
        where TTweenValue : struct, ITweenValue
    {
        public TTweenValue Tween { get; private set; }
        public bool Running { get; private set; }

        private float elapsedTime;
        private Guid lastRunGuid;
        private UnityEngine.Object target;
        private bool targetSpecified;

        public void Run (TTweenValue tween, AsyncToken token = default, UnityEngine.Object target = default)
        {
            if (Running) CompleteInstantly();
            Tween = tween;
            targetSpecified = this.target = target;
            TweenAsyncAndForget(token).Forget();
        }

        public UniTask RunAwaitable (TTweenValue tween, AsyncToken token = default, UnityEngine.Object target = default)
        {
            if (Running) CompleteInstantly();
            Tween = tween;
            targetSpecified = this.target = target;
            return TweenAsync(token);
        }

        public void Stop ()
        {
            lastRunGuid = Guid.Empty;
            Running = false;
        }

        public void CompleteInstantly ()
        {
            Stop();
            if (Tween.Props.Complete)
                Tween.Tween(1f);
        }

        protected async UniTask TweenAsync (AsyncToken token = default)
        {
            PrepareTween();
            if (Tween.Props.Duration <= 0f)
            {
                CompleteInstantly();
                return;
            }

            var currentRunGuid = lastRunGuid;
            while (elapsedTime <= Tween.Props.Duration && token.EnsureNotCanceledOrCompleted(targetSpecified ? target : null))
            {
                PerformTween();
                await AsyncUtils.WaitEndOfFrame(token);
                if (lastRunGuid != currentRunGuid) return; // The tweener was completed instantly or stopped.
            }

            if (token.Completed) CompleteInstantly();
            else FinishTween();
        }

        // Required to prevent allocation when await is not required (fire and forget).
        // Remember to keep this method identical with TweenAsync().
        protected async UniTaskVoid TweenAsyncAndForget (AsyncToken token = default)
        {
            PrepareTween();
            if (Tween.Props.Duration <= 0f)
            {
                CompleteInstantly();
                return;
            }

            var currentRunGuid = lastRunGuid;
            while (elapsedTime <= Tween.Props.Duration && token.EnsureNotCanceledOrCompleted(targetSpecified ? target : null))
            {
                PerformTween();
                await AsyncUtils.WaitEndOfFrame(token);
                if (lastRunGuid != currentRunGuid) return; // The tweener was completed instantly or stopped.
            }

            if (token.Completed) CompleteInstantly();
            else FinishTween();
        }

        private void PrepareTween ()
        {
            Running = true;
            elapsedTime = 0f;
            lastRunGuid = Guid.NewGuid();
        }

        private void PerformTween ()
        {
            elapsedTime += Tween.Props.Scale ? Engine.Time.DeltaTime : Engine.Time.UnscaledDeltaTime;
            var tweenPercent = Mathf.Clamp01(elapsedTime / Tween.Props.Duration);
            Tween.Tween(tweenPercent);
        }

        private void FinishTween ()
        {
            Running = false;
        }
    }
}
