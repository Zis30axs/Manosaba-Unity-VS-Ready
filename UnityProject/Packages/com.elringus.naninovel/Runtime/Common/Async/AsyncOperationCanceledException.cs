using System;

namespace Naninovel
{
    /// <summary>
    /// Thrown upon cancellation of an async operation via <see cref="AsyncToken"/>.
    /// </summary>
    public class AsyncOperationCanceledException : OperationCanceledException
    {
        public AsyncOperationCanceledException (AsyncToken token)
            : base(token.CancellationToken)
        {
            if (!token.Canceled) throw new ArgumentException("Specified token is not canceled.", nameof(token));
        }

        protected AsyncOperationCanceledException () { }
    }
}
