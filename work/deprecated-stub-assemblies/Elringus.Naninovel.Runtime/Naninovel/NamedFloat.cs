using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000211")]
    public class NamedFloat : Named<NullableFloat>
    {
    	[Token(Token = "0x6000944")]
    	[Address(RVA = "0x525B70", Offset = "0x524570", VA = "0x180525B70")]
    	public NamedFloat()
    	{
    	}

    	[Token(Token = "0x6000945")]
    	[Address(RVA = "0x525BA0", Offset = "0x5245A0", VA = "0x180525BA0")]
    	public NamedFloat(string name, float? value)
    	{
    	}
    }
}
