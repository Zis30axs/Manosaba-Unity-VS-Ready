using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20001D2")]
    public interface IRejectPromise
    {
    	[Token(Token = "0x6000821")]
    	bool TrySetException(Exception exception);
    }
}
