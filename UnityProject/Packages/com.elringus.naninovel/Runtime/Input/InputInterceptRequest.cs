using System.Threading;

namespace Naninovel
{
    /// <summary>
    /// Keeps data associated with <see cref="IInputSampler.InterceptNext"/>.
    /// </summary>
    public readonly struct InputInterceptRequest
    {
        /// <summary>
        /// Token returned to the handler of the request; cancelled when input starts activation.
        /// </summary>
        public readonly CancellationTokenSource CTS;
        /// <summary>
        /// Token specified by the handler of the request; the request is ignored in case the token
        /// is cancelled when input event occurs.
        /// </summary>
        public readonly CancellationToken HandlerToken;

        public InputInterceptRequest (CancellationTokenSource cts, CancellationToken handlerToken)
        {
            CTS = cts;
            HandlerToken = handlerToken;
        }
    }
}
