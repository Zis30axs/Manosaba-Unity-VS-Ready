using Il2CppDummyDll;

namespace CustomInspector
{
    [Token(Token = "0x200003A")]
    public enum StaticConditions
    {
    	[Token(Token = "0x4000114")]
    	IsPlaying,
    	[Token(Token = "0x4000115")]
    	IsNotPlaying,
    	[Token(Token = "0x4000116")]
    	True,
    	[Token(Token = "0x4000117")]
    	False,
    	[Token(Token = "0x4000118")]
    	IsEnabled,
    	[Token(Token = "0x4000119")]
    	IsActiveAndEnabled
    }
}
