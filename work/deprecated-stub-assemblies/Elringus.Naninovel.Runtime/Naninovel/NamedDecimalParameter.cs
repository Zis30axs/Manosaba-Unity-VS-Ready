using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x20003F9")]
    public class NamedDecimalParameter : NamedParameter<NamedFloat, NullableFloat>
    {
    	[Token(Token = "0x6001327")]
    	[Address(RVA = "0x5A95B0", Offset = "0x5A7FB0", VA = "0x1805A95B0")]
    	public static implicit operator NamedDecimalParameter(NamedFloat value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001328")]
    	[Address(RVA = "0x5A9650", Offset = "0x5A8050", VA = "0x1805A9650")]
    	public static implicit operator NamedFloat(NamedDecimalParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001329")]
    	[Address(RVA = "0x5A96E0", Offset = "0x5A80E0", VA = "0x1805A96E0", Slot = "23")]
    	protected override NamedFloat ParseRaw(RawValue raw, out string errors)
    	{
    		errors = default;
    		errors = default;
    		return null;
    	}

    	[Token(Token = "0x600132A")]
    	[Address(RVA = "0x5A9840", Offset = "0x5A8240", VA = "0x1805A9840")]
    	public NamedDecimalParameter()
    	{
    	}
    }
}
