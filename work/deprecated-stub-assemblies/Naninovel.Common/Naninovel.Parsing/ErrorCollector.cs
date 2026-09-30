using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000022")]
    public class ErrorCollector : List<ParseError>, IErrorHandler
    {
    	[Token(Token = "0x6000090")]
    	[Address(RVA = "0x10D3410", Offset = "0x10D1E10", VA = "0x1810D3410", Slot = "35")]
    	public void HandleError(ParseError error)
    	{
    	}

    	[Token(Token = "0x6000091")]
    	[Address(RVA = "0x10D3460", Offset = "0x10D1E60", VA = "0x1810D3460")]
    	public ErrorCollector()
    	{
    	}
    }
}
