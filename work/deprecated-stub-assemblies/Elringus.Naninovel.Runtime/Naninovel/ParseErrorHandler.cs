using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel
{
    [Token(Token = "0x2000413")]
    public class ParseErrorHandler : IErrorHandler
    {
    	[Token(Token = "0x170002B9")]
    	public ICollection<ScriptParseError> Errors
    	{
    		[Token(Token = "0x60013C0")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60013C1")]
    		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x170002BA")]
    	public int LineIndex
    	{
    		[Token(Token = "0x60013C2")]
    		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    		[Token(Token = "0x60013C3")]
    		[Address(RVA = "0x39B780", Offset = "0x39A180", VA = "0x18039B780")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x60013C4")]
    	[Address(RVA = "0x5B27A0", Offset = "0x5B11A0", VA = "0x1805B27A0", Slot = "4")]
    	public void HandleError(ParseError error)
    	{
    	}

    	[Token(Token = "0x60013C5")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public ParseErrorHandler()
    	{
    	}
    }
}
