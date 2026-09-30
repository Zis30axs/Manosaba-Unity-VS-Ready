using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20002DA")]
    public readonly struct DeferredAction : IDisposable
    {
    	[Token(Token = "0x4000B2E")]
    	[FieldOffset(Offset = "0x0")]
    	private readonly Action action;

    	[Token(Token = "0x6000DBF")]
    	[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
    	public DeferredAction(Action action)
    	{ this = default; }

    	[Token(Token = "0x6000DC0")]
    	[Address(RVA = "0x547EF0", Offset = "0x5468F0", VA = "0x180547EF0", Slot = "4")]
    	public void Dispose()
    	{
    	}
    }
}
