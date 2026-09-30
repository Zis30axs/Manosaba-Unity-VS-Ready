using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000063")]
    internal class Utilities
    {
    	[Token(Token = "0x400012B")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private ISyntax _003Cstx_003EP;

    	[Token(Token = "0x600021B")]
    	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    	public Utilities(ISyntax stx)
    	{
    	}

    	[Token(Token = "0x600021C")]
    	[Address(RVA = "0x10E92D0", Offset = "0x10E7CD0", VA = "0x1810E92D0")]
    	public bool IsPlainTextControlChar(char ch, char next = '\0')
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600021D")]
    	[Address(RVA = "0x10E9490", Offset = "0x10E7E90", VA = "0x1810E9490")]
    	public bool IsEscaped(string value, int i)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600021E")]
    	[Address(RVA = "0x10E94E0", Offset = "0x10E7EE0", VA = "0x1810E94E0")]
    	public string UnescapeCharacter(string content, string character)
    	{
    		return null;
    	}
    }
}
