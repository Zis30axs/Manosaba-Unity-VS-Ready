using System;
using System.Collections;

namespace Naninovel
{
    public readonly partial struct UniTask
    {
        public static IEnumerator ToCoroutine (Func<UniTask> taskFactory, Action<Exception> exceptionHandler = null)
        {
            return taskFactory().ToCoroutine(exceptionHandler);
        }
    }
}
