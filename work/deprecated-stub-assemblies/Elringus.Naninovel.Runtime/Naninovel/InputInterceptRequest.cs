using System.Threading;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200036A")]
    public readonly struct InputInterceptRequest
    {
    	[Token(Token = "0x4000CA4")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly CancellationTokenSource CTS;

    	[Token(Token = "0x4000CA5")]
    	[FieldOffset(Offset = "0x8")]
    	public readonly CancellationToken HandlerToken;

    	[Token(Token = "0x6001065")]
    	[Address(RVA = "0x457060", Offset = "0x455A60", VA = "0x180457060")]
    	public InputInterceptRequest(CancellationTokenSource cts, CancellationToken handlerToken)
    	{ this = default; }
    }
}
