using Il2CppDummyDll;

namespace CustomInspector
{
    [Token(Token = "0x200001D")]
    public enum ExecutionTarget
    {
    	[Token(Token = "0x40000D1")]
    	Always,
    	[Token(Token = "0x40000D2")]
    	IsPlaying,
    	[Token(Token = "0x40000D3")]
    	IsNotPlaying
    }
}
