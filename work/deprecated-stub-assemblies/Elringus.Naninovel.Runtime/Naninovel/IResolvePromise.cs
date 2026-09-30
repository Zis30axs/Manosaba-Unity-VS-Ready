using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20001D0")]
    public interface IResolvePromise
    {
    	[Token(Token = "0x600081F")]
    	bool TrySetResult();
    }
    [Token(Token = "0x20001D1")]
    public interface IResolvePromise<T>
    {
    	[Token(Token = "0x6000820")]
    	bool TrySetResult(T value);
    }
}
