using System;
using System.Threading.Tasks;

namespace Naninovel
{
    /// <summary>
    /// Invokes action specified in constructor on dispose.
    /// </summary>
    /// <remarks>
    /// Disposable structs won't box inside "using" context due to a .NET runtime optimization.
    /// </remarks>
    public readonly struct DeferredAsyncAction : IAsyncDisposable
    {
        private readonly Func<UniTask> action;

        public DeferredAsyncAction (Func<UniTask> action)
        {
            this.action = action;
        }

        public async ValueTask DisposeAsync ()
        {
            await action();
        }
    }
}
