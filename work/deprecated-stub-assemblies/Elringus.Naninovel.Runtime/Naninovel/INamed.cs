using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200020D")]
    public interface INamed<TValue> : INamedValue
    {
    	[Token(Token = "0x17000150")]
    	TValue Value
    	{
    		[Token(Token = "0x6000937")]
    		get;
    		[Token(Token = "0x6000938")]
    		set;
    	}
    }
}
