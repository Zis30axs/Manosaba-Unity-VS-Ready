using Il2CppDummyDll;

namespace CustomInspector
{
    [Token(Token = "0x2000028")]
    public enum MessageBoxType
    {
    	[Token(Token = "0x40000EA")]
    	None,
    	[Token(Token = "0x40000EB")]
    	Info,
    	[Token(Token = "0x40000EC")]
    	Warning,
    	[Token(Token = "0x40000ED")]
    	Error
    }
}
