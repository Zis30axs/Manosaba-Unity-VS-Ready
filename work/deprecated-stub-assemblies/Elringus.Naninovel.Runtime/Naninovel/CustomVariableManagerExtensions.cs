using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000327")]
    public static class CustomVariableManagerExtensions
    {
    	[Token(Token = "0x6000F20")]
    	[Address(RVA = "0x562670", Offset = "0x561070", VA = "0x180562670")]
    	public static bool TryGetVariableValue(this ICustomVariableManager manager, string name, out CustomVariableValue value)
    	{
    		value = default;
    		value = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000F21")]
    	public static bool TryGetVariableValue<TValue>(this ICustomVariableManager manager, string name, out TValue value)
    	{
    		value = default;
    		value = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000F22")]
    	public static bool TrySetVariableValue<TValue>(this ICustomVariableManager manager, string name, TValue value)
    	{
    		return default(bool);
    	}
    }
}
