using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000218")]
    public interface INullable<TValue> : INullableValue
    {
    	[Token(Token = "0x17000154")]
    	TValue Value
    	{
    		[Token(Token = "0x6000951")]
    		get;
    		[Token(Token = "0x6000952")]
    		set;
    	}
    }
}
