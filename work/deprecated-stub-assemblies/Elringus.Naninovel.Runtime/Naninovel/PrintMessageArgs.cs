using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200006B")]
    public readonly struct PrintMessageArgs : IEquatable<PrintMessageArgs>
    {
    	[Token(Token = "0x400015F")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly ITextPrinterActor Printer;

    	[Token(Token = "0x4000160")]
    	[FieldOffset(Offset = "0x8")]
    	public readonly PrintedMessage Message;

    	[Token(Token = "0x4000161")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly bool Append;

    	[Token(Token = "0x4000162")]
    	[FieldOffset(Offset = "0x24")]
    	public readonly float Speed;

    	[Token(Token = "0x60001D7")]
    	[Address(RVA = "0x457980", Offset = "0x456380", VA = "0x180457980")]
    	public PrintMessageArgs(ITextPrinterActor printer, PrintedMessage message, bool append, float speed)
    	{ this = default; }

    	[Token(Token = "0x60001D8")]
    	[Address(RVA = "0x457A60", Offset = "0x456460", VA = "0x180457A60", Slot = "4")]
    	public bool Equals(PrintMessageArgs other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001D9")]
    	[Address(RVA = "0x457BA0", Offset = "0x4565A0", VA = "0x180457BA0", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001DA")]
    	[Address(RVA = "0x457C50", Offset = "0x456650", VA = "0x180457C50", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
