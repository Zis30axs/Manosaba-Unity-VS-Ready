using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Naninovel
{
    /// <summary>
    /// Replacement for <see cref="SemaphoreSlim"/> that runs on Unity scheduler.
    /// Required for platforms without threading support, such as WebGL.
    /// </summary>
    public class Semaphore : IDisposable
    {
        private readonly ConcurrentQueue<UniTaskCompletionSource> waiters = new();
        private readonly int maxCount;
        private int count;

        public Semaphore (int initialCount, int maxCount = int.MaxValue)
        {
            count = initialCount;
            this.maxCount = maxCount;
        }

        public UniTask Wait () => Wait(CancellationToken.None);

        public async UniTask Wait (AsyncToken token)
        {
            if (count > 0)
            {
                count--;
                return;
            }

            var tcs = new UniTaskCompletionSource();
            if (token.CancellationToken.CanBeCanceled)
                token.CancellationToken.Register(() => tcs.TrySetCanceled());
            waiters.Enqueue(tcs);
            try { await tcs.Task; }
            finally { token.ThrowIfCanceled(); }
        }

        public void Release () => Release(1);

        public void Release (int releaseCount)
        {
            for (int i = 0; i < releaseCount; i++)
            {
                if (count + 1 > maxCount) break;
                if (waiters.TryDequeue(out var waiter))
                    waiter.TrySetResult();
                count++;
            }
        }

        public void Dispose ()
        {
            while (!waiters.IsEmpty)
                if (waiters.TryDequeue(out var waiter))
                    waiter.TrySetCanceled();
        }
    }
}
