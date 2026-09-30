using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000210")]
    public class NamedInteger : Named<NullableInteger>
    {
    	[Token(Token = "0x6000942")]
    	[Address(RVA = "0x525990", Offset = "0x524390", VA = "0x180525990")]
    	public NamedInteger()
    	{
    	}

    	[Token(Token = "0x6000943")]
    	[Address(RVA = "0x5259C0", Offset = "0x5243C0", VA = "0x1805259C0")]
    	public NamedInteger(string name, int? value)
    	{
    	}
    }
}
