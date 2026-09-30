using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000406")]
    public class StringParameter : CommandParameter<string>
    {
    	[Token(Token = "0x6001374")]
    	[Address(RVA = "0x5ABE50", Offset = "0x5AA850", VA = "0x1805ABE50")]
    	public static implicit operator StringParameter(string value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001375")]
    	[Address(RVA = "0x5ABEF0", Offset = "0x5AA8F0", VA = "0x1805ABEF0")]
    	public static implicit operator string(StringParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001376")]
    	[Address(RVA = "0x5ABF80", Offset = "0x5AA980", VA = "0x1805ABF80")]
    	public static implicit operator StringParameter(NullableString value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001377")]
    	[Address(RVA = "0x5AC090", Offset = "0x5AAA90", VA = "0x1805AC090")]
    	public static implicit operator NullableString(StringParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001378")]
    	[Address(RVA = "0x5AC100", Offset = "0x5AAB00", VA = "0x1805AC100", Slot = "23")]
    	protected override string ParseRaw(RawValue raw, out string errors)
    	{
    		errors = default;
    		errors = default;
    		return null;
    	}

    	[Token(Token = "0x6001379")]
    	[Address(RVA = "0x5AC1C0", Offset = "0x5AABC0", VA = "0x1805AC1C0")]
    	public StringParameter()
    	{
    	}
    }
}
