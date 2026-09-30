using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x20000A0")]
    public class Function : IExpression
    {
    	[Token(Token = "0x170000DA")]
    	public string Name
    	{
    		[Token(Token = "0x600039A")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170000DB")]
    	public IReadOnlyList<IExpression> Parameters
    	{
    		[Token(Token = "0x600039B")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000399")]
    	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
    	public Function(string name, IReadOnlyList<IExpression> @params)
    	{
    	}
    }
}
