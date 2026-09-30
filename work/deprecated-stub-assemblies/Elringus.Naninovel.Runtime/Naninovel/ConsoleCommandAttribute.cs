using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000316")]
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ConsoleCommandAttribute : Attribute
    {
    	[Token(Token = "0x170001F2")]
    	public string Alias
    	{
    		[Token(Token = "0x6000EB5")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000EB6")]
    	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    	public ConsoleCommandAttribute([Optional] string alias)
    	{
    	}
    }
}
