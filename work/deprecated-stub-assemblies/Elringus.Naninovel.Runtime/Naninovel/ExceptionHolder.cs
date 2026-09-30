using System.Runtime.ExceptionServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20001CF")]
    internal class ExceptionHolder
    {
    	[Token(Token = "0x40007A0")]
    	[FieldOffset(Offset = "0x10")]
    	private ExceptionDispatchInfo exception;

    	[Token(Token = "0x40007A1")]
    	[FieldOffset(Offset = "0x18")]
    	private bool calledGet;

    	[Token(Token = "0x600081C")]
    	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    	public ExceptionHolder(ExceptionDispatchInfo exception)
    	{
    	}

    	[Token(Token = "0x600081D")]
    	[Address(RVA = "0x519990", Offset = "0x518390", VA = "0x180519990")]
    	public ExceptionDispatchInfo GetException()
    	{
    		return null;
    	}

    	[Token(Token = "0x600081E")]
    	[Address(RVA = "0x5199F0", Offset = "0x5183F0", VA = "0x1805199F0", Slot = "1")]
    	~ExceptionHolder()
    	{
    	}
    }
}
