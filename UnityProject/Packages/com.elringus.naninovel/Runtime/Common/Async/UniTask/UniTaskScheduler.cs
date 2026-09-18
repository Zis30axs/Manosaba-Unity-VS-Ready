using System;
using System.Collections.Generic;
using System.Threading;

namespace Naninovel.Async
{
    // UniTask has no scheduler like TaskScheduler.
    // Only handle unobserved exception.

    public static class UniTaskScheduler
    {
        public static event Action<Exception> UnobservedTaskException;

        /// <summary>
        /// Write log type when catch unobserved exception and not registered UnobservedTaskException. Default is Warning.
        /// </summary>
        public static UnityEngine.LogType UnobservedExceptionWriteLogType = UnityEngine.LogType.Warning;

        /// <summary>
        /// Dispatch exception event to Unity MainThread.
        /// </summary>
        public static bool DispatchUnityMainThread = true;

        /// <summary>
        /// When exception is thrown, will check if any predicates here return true for the exception, in which
        /// case the exception won't be propagated by the scheduler.
        /// </summary>
        public static HashSet<Predicate<Exception>> IgnoredExceptions { get; } = new();

        // cache delegate.
        private static readonly SendOrPostCallback handleExceptionInvoke = InvokeUnobservedTaskException;

        internal static void PublishUnobservedTaskException (Exception ex)
        {
            if (ex == null || ex is OperationCanceledException) return;

            foreach (var ignore in IgnoredExceptions)
                if (ignore(ex))
                    return;

            if (UnobservedTaskException != null)
            {
                if (Thread.CurrentThread.ManagedThreadId == PlayerLoopHelper.MainThreadId)
                    UnobservedTaskException.Invoke(ex); // allows inlining call.
                else PlayerLoopHelper.UnitySynchronizationContext.Post(handleExceptionInvoke, ex);
            }
            else
            {
                string msg = null;
                if (UnobservedExceptionWriteLogType != UnityEngine.LogType.Exception)
                    msg = ex.ToString();
                switch (UnobservedExceptionWriteLogType)
                {
                    case UnityEngine.LogType.Error:
                        UnityEngine.Debug.LogError(msg);
                        break;
                    case UnityEngine.LogType.Assert:
                        UnityEngine.Debug.LogAssertion(msg);
                        break;
                    case UnityEngine.LogType.Warning:
                        UnityEngine.Debug.LogWarning(msg);
                        break;
                    case UnityEngine.LogType.Log:
                        UnityEngine.Debug.Log(msg);
                        break;
                    case UnityEngine.LogType.Exception:
                        UnityEngine.Debug.LogException(ex);
                        break;
                }
            }
        }

        private static void InvokeUnobservedTaskException (object state)
        {
            UnobservedTaskException?.Invoke((Exception)state);
        }
    }
}
