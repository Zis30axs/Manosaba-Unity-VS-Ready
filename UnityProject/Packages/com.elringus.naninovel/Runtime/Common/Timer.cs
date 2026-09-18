using System;

namespace Naninovel
{
    public class Timer
    {
        public bool Running { get; private set; }
        public bool Loop { get; private set; }
        public bool TimeScaleIgnored { get; private set; }
        public float Duration { get; private set; }

        private readonly Action onLoop;
        private readonly Action onCompleted;
        private UnityEngine.Object target;
        private bool targetSpecified;
        private Guid lastRunGuid;

        public Timer (float duration = 0f, bool loop = false, bool ignoreTimeScale = false,
            Action onCompleted = null, Action onLoop = null)
        {
            Duration = duration;
            Loop = loop;
            TimeScaleIgnored = ignoreTimeScale;

            this.onLoop += onLoop;
            this.onCompleted += onCompleted;
        }

        public void Run (float duration, bool loop = false, bool ignoreTimeScale = false,
            AsyncToken token = default, UnityEngine.Object target = default)
        {
            if (Running) CompleteInstantly();

            Duration = duration;
            Loop = loop;
            TimeScaleIgnored = ignoreTimeScale;
            Running = true;

            targetSpecified = this.target = target;

            if (Loop) WaitAndLoop(token).Forget();
            else WaitAndComplete(token).Forget();
        }

        public void Run (AsyncToken token = default, UnityEngine.Object target = default)
            => Run(Duration, Loop, TimeScaleIgnored, token, target);

        public void Stop ()
        {
            lastRunGuid = Guid.Empty;
            Running = false;
        }

        public void CompleteInstantly ()
        {
            Stop();
            onCompleted?.Invoke();
        }

        protected virtual async UniTaskVoid WaitAndComplete (AsyncToken token = default)
        {
            lastRunGuid = Guid.NewGuid();
            var currentRunGuid = lastRunGuid;
            var startTime = GetTime();

            while (!WaitedEnough(startTime) && token.EnsureNotCanceledOrCompleted(targetSpecified ? target : null))
                await AsyncUtils.WaitEndOfFrame(token);

            if (lastRunGuid != currentRunGuid) return; // The timer was completed instantly or stopped.

            if (token.Completed) CompleteInstantly();
            else
            {
                Running = false;
                onCompleted?.Invoke();
            }
        }

        protected virtual async UniTaskVoid WaitAndLoop (AsyncToken token = default)
        {
            lastRunGuid = Guid.NewGuid();
            var currentRunGuid = lastRunGuid;
            var startTime = GetTime();

            while (token.EnsureNotCanceledOrCompleted(targetSpecified ? target : null))
            {
                await AsyncUtils.WaitEndOfFrame(token);
                if (targetSpecified && !target) throw new AsyncOperationDestroyedException(target);
                if (lastRunGuid != currentRunGuid) return; // The timer was stopped.
                if (WaitedEnough(startTime))
                {
                    onLoop?.Invoke();
                    startTime = GetTime();
                }
            }

            if (token.Completed) CompleteInstantly();
        }

        private float GetTime () => TimeScaleIgnored ? Engine.Time.UnscaledTime : Engine.Time.Time;

        private bool WaitedEnough (float startTime) => GetTime() - startTime >= Duration;
    }
}
