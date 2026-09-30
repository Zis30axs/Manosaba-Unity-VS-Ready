using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000040")]
    public class NamedValueParser
    {
    	[Token(Token = "0x40000B0")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private ISyntax _003Cstx_003EP;

    	[Token(Token = "0x40000B1")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly Utilities utils;

    	[Token(Token = "0x6000181")]
    	[Address(RVA = "0x10E1020", Offset = "0x10DFA20", VA = "0x1810E1020")]
    	public NamedValueParser(ISyntax stx)
    	{
    	}

    	[Token(Token = "0x6000182")]
    	[Address(RVA = "0x10E1170", Offset = "0x10DFB70", VA = "0x1810E1170")]
    	public (string, string) Parse(string value)
    	{
    		return default((string, string));
    	}

    	[Token(Token = "0x6000183")]
    	[Address(RVA = "0x10E1430", Offset = "0x10DFE30", VA = "0x1810E1430")]
    	private string Unescape(string value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000184")]
    	[Address(RVA = "0x10E1520", Offset = "0x10DFF20", VA = "0x1810E1520")]
    	private int FindDelimiterIndex(string value)
    	{
    		return default(int);
    	}
    }
}
