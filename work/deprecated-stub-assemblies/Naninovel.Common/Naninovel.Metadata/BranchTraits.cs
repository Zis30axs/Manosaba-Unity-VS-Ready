using System;
using Il2CppDummyDll;

namespace Naninovel.Metadata
{
    [Token(Token = "0x2000065")]
    [Flags]
    public enum BranchTraits
    {
    	[Token(Token = "0x4000130")]
    	Endpoint = 1,
    	[Token(Token = "0x4000131")]
    	Nest = 2,
    	[Token(Token = "0x4000132")]
    	Interactive = 4,
    	[Token(Token = "0x4000133")]
    	Return = 8,
    	[Token(Token = "0x4000134")]
    	Switch = 0x10
    }
}
