using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000212")]
    public class NamedBoolean : Named<NullableBoolean>
    {
    	[Token(Token = "0x6000946")]
    	[Address(RVA = "0x525D50", Offset = "0x524750", VA = "0x180525D50")]
    	public NamedBoolean()
    	{
    	}

    	[Token(Token = "0x6000947")]
    	[Address(RVA = "0x525D80", Offset = "0x524780", VA = "0x180525D80")]
    	public NamedBoolean(string name, bool? value)
    	{
    	}
    }
}
