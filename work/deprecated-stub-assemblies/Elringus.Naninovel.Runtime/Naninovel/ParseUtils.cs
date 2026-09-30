using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20002F5")]
    public static class ParseUtils
    {
    	[Token(Token = "0x6000E25")]
    	[Address(RVA = "0x54C960", Offset = "0x54B360", VA = "0x18054C960")]
    	public static bool TryInvariantInt(string str, out int result)
    	{
    		result = default;
    		result = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000E26")]
    	[Address(RVA = "0x54CA20", Offset = "0x54B420", VA = "0x18054CA20")]
    	public static bool TryInvariantFloat(string str, out float result)
    	{
    		result = default;
    		result = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000E27")]
    	[Address(RVA = "0x54CAE0", Offset = "0x54B4E0", VA = "0x18054CAE0")]
    	public static int? AsInvariantInt(this string str)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000E28")]
    	[Address(RVA = "0x54CBE0", Offset = "0x54B5E0", VA = "0x18054CBE0")]
    	public static float? AsInvariantFloat(this string str)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000E29")]
    	public static bool TryConstantParameter<TEnum>(string value, out TEnum result) where TEnum : struct
    	{
    		result = default;
    		result = default;
    		return default(bool);
    	}
    }
}
