using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000026")]
    public class ParseError
    {
    	[Token(Token = "0x17000014")]
    	public string Message
    	{
    		[Token(Token = "0x6000096")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000015")]
    	public int StartIndex
    	{
    		[Token(Token = "0x6000097")]
    		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000016")]
    	public int Length
    	{
    		[Token(Token = "0x6000098")]
    		[Address(RVA = "0x38F5A0", Offset = "0x38DFA0", VA = "0x18038F5A0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000017")]
    	public int EndIndex
    	{
    		[Token(Token = "0x6000099")]
    		[Address(RVA = "0x10D34D0", Offset = "0x10D1ED0", VA = "0x1810D34D0")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x6000095")]
    	[Address(RVA = "0xF415B0", Offset = "0xF3FFB0", VA = "0x180F415B0")]
    	public ParseError(string message, int startIndex, int length)
    	{
    	}

    	[Token(Token = "0x600009A")]
    	[Address(RVA = "0x10D34E0", Offset = "0x10D1EE0", VA = "0x1810D34E0")]
    	public ParseError(Token token)
    	{
    	}
    }
}
