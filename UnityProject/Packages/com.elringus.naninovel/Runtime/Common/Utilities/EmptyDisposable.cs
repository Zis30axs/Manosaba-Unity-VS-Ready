using System;

namespace Naninovel
{
    /// <summary>
    /// A mock disposable that doesn't nothing on dispose.
    /// Use to prevent allocation when allocating actual disposable object is not necessary.
    /// </summary>
    /// <remarks>
    /// Disposable structs won't box inside "using" context due to a .NET runtime optimization.
    /// </remarks>
    public readonly struct EmptyDisposable : IDisposable
    {
        public void Dispose () { }
    }
}
