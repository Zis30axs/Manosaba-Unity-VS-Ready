using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20001D4")]
    public interface IPromise<T> : IResolvePromise<T>, IRejectPromise, ICancelPromise
    {
    }
    [Token(Token = "0x20001D5")]
    public interface IPromise : IResolvePromise, IRejectPromise, ICancelPromise
    {
    }
}
