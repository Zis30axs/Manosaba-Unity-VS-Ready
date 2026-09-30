using System.Collections.Generic;
using System.Text;
using Il2CppDummyDll;

namespace Naninovel.ManagedText
{
    [Token(Token = "0x2000086")]
    public class InlineManagedTextParser
    {
    	[Token(Token = "0x2000087")]
    	public class SyntaxError : Error
    	{
    		[Token(Token = "0x6000322")]
    		[Address(RVA = "0x10CFAA0", Offset = "0x10CE4A0", VA = "0x1810CFAA0")]
    		public SyntaxError(string message)
    		{
    		}
    	}

    	[Token(Token = "0x40001CC")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly HashSet<ManagedTextRecord> records;

    	[Token(Token = "0x40001CD")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly StringBuilder commentBuilder;

    	[Token(Token = "0x40001CE")]
    	[FieldOffset(Offset = "0x20")]
    	private string header;

    	[Token(Token = "0x600031D")]
    	[Address(RVA = "0x10F2290", Offset = "0x10F0C90", VA = "0x1810F2290")]
    	public ManagedTextDocument Parse(string text)
    	{
    		return null;
    	}

    	[Token(Token = "0x600031E")]
    	[Address(RVA = "0x10F2650", Offset = "0x10F1050", VA = "0x1810F2650")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x600031F")]
    	[Address(RVA = "0x10F2710", Offset = "0x10F1110", VA = "0x1810F2710")]
    	private void ParseCommentLine(string line, int index)
    	{
    	}

    	[Token(Token = "0x6000320")]
    	[Address(RVA = "0x10F2C50", Offset = "0x10F1650", VA = "0x1810F2C50")]
    	private void ParseRecordLine(string line, int index)
    	{
    	}

    	[Token(Token = "0x6000321")]
    	[Address(RVA = "0x10F30B0", Offset = "0x10F1AB0", VA = "0x1810F30B0")]
    	public InlineManagedTextParser()
    	{
    	}
    }
}
