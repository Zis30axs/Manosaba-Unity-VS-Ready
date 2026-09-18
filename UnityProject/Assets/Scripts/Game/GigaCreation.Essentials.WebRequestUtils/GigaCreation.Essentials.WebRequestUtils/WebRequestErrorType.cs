using Il2CppDummyDll;

namespace GigaCreation.Essentials.WebRequestUtils
{
    [Token(Token = "0x2000006")]
    public enum WebRequestErrorType
    {
    	[Token(Token = "0x400000B")]
    	Timeout,
    	[Token(Token = "0x400000C")]
    	CancellationRequested,
    	[Token(Token = "0x400000D")]
    	ConnectionError,
    	[Token(Token = "0x400000E")]
    	AccessDenied,
    	[Token(Token = "0x400000F")]
    	NotFound
    }
}
