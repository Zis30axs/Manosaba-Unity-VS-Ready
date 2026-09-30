using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000013")]
    public interface ISerializer
    {
    	[Token(Token = "0x6000044")]
    	string Serialize(object poco);

    	[Token(Token = "0x6000045")]
    	string Serialize(object poco, Type type);

    	[Token(Token = "0x6000046")]
    	object Deserialize(string serialized, Type type);
    }
}
