using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000007")]
    public class Error : Exception
    {
    	[Token(Token = "0x6000011")]
    	[Address(RVA = "0x10CFA50", Offset = "0x10CE450", VA = "0x1810CFA50")]
    	public Error()
    	{
    	}

    	[Token(Token = "0x6000012")]
    	[Address(RVA = "0x10CFAA0", Offset = "0x10CE4A0", VA = "0x1810CFAA0")]
    	public Error(string message)
    	{
    	}

    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0x10CFB00", Offset = "0x10CE500", VA = "0x1810CFB00")]
    	public Error(string message, Exception inner)
    	{
    	}
    }
}
