using System.Diagnostics.CodeAnalysis;
using Il2CppDummyDll;

namespace Naninovel.Csv
{
    [Token(Token = "0x20000BF")]
    internal sealed class Field
    {
    	[Token(Token = "0x4000225")]
    	[FieldOffset(Offset = "0x10")]
    	public int Start;

    	[Token(Token = "0x4000226")]
    	[FieldOffset(Offset = "0x14")]
    	public int End;

    	[Token(Token = "0x4000227")]
    	[FieldOffset(Offset = "0x18")]
    	public bool Quoted;

    	[Token(Token = "0x4000228")]
    	[FieldOffset(Offset = "0x1C")]
    	public int EscapedQuotesCount;

    	[Token(Token = "0x170000E9")]
    	public int Length
    	{
    		[Token(Token = "0x60003DB")]
    		[Address(RVA = "0x1102430", Offset = "0x1100E30", VA = "0x181102430")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x60003DC")]
    	[Address(RVA = "0x1102440", Offset = "0x1100E40", VA = "0x181102440")]
    	public Field Reset(int start)
    	{
    		return null;
    	}

    	[Token(Token = "0x60003DD")]
    	[Address(RVA = "0x1102460", Offset = "0x1100E60", VA = "0x181102460")]
    	public string GetValue(char[] buf)
    	{
    		return null;
    	}

    	[Token(Token = "0x60003DE")]
    	[Address(RVA = "0x1102620", Offset = "0x1101020", VA = "0x181102620")]
    	[ExcludeFromCodeCoverage]
    	private string GetString(char[] buf, int start, int len)
    	{
    		return null;
    	}

    	[Token(Token = "0x60003DF")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public Field()
    	{
    	}
    }
}
