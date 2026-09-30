using System;
using Il2CppDummyDll;
using Naninovel.UI;

namespace Naninovel
{
    [Token(Token = "0x20004C0")]
    public class InteractionBlocker : IDisposable
    {
    	[Token(Token = "0x4001210")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly IClickThroughPanel panel;

    	[Token(Token = "0x60017BD")]
    	[Address(RVA = "0x5F9A00", Offset = "0x5F8400", VA = "0x1805F9A00")]
    	public InteractionBlocker()
    	{
    	}

    	[Token(Token = "0x60017BE")]
    	[Address(RVA = "0x5F9B70", Offset = "0x5F8570", VA = "0x1805F9B70", Slot = "4")]
    	public void Dispose()
    	{
    	}
    }
}
