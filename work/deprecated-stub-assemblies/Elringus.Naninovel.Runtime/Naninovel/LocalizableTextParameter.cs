using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x20003F5")]
    public class LocalizableTextParameter : CommandParameter<LocalizableText>
    {
    	[Token(Token = "0x6001315")]
    	[Address(RVA = "0x5A81F0", Offset = "0x5A6BF0", VA = "0x1805A81F0")]
    	public static implicit operator LocalizableTextParameter(string value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001316")]
    	[Address(RVA = "0x5A82D0", Offset = "0x5A6CD0", VA = "0x1805A82D0")]
    	public static implicit operator LocalizableTextParameter(LocalizableText value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001317")]
    	[Address(RVA = "0x5A8380", Offset = "0x5A6D80", VA = "0x1805A8380")]
    	public static implicit operator LocalizableText?(LocalizableTextParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001318")]
    	[Address(RVA = "0x5A8470", Offset = "0x5A6E70", VA = "0x1805A8470")]
    	public static implicit operator string(LocalizableTextParameter value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001319")]
    	[Address(RVA = "0x5A84F0", Offset = "0x5A6EF0", VA = "0x1805A84F0", Slot = "23")]
    	protected override LocalizableText ParseRaw(RawValue raw, out string errors)
    	{
    		errors = default;
    		errors = default;
    		return default(LocalizableText);
    	}

    	[Token(Token = "0x600131A")]
    	[Address(RVA = "0x5A8DE0", Offset = "0x5A77E0", VA = "0x1805A8DE0")]
    	public LocalizableTextParameter()
    	{
    	}
    }
}
