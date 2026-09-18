using System;

namespace Naninovel
{
    /// <summary>
    /// Invokes action specified in constructor on dispose.
    /// </summary>
    /// <remarks>
    /// Disposable structs won't box inside "using" context due to a .NET runtime optimization.
    /// </remarks>
    public readonly struct DeferredAction : IDisposable
    {
        private readonly Action action;

        public DeferredAction (Action action)
        {
            this.action = action;
        }

        public void Dispose () => action.Invoke();
    }
}
