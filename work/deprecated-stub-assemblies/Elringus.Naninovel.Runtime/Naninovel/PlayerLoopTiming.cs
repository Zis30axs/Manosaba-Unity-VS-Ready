using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000132")]
    public enum PlayerLoopTiming
    {
    	[Token(Token = "0x40004BB")]
    	Initialization,
    	[Token(Token = "0x40004BC")]
    	LastInitialization,
    	[Token(Token = "0x40004BD")]
    	EarlyUpdate,
    	[Token(Token = "0x40004BE")]
    	LastEarlyUpdate,
    	[Token(Token = "0x40004BF")]
    	FixedUpdate,
    	[Token(Token = "0x40004C0")]
    	LastFixedUpdate,
    	[Token(Token = "0x40004C1")]
    	PreUpdate,
    	[Token(Token = "0x40004C2")]
    	LastPreUpdate,
    	[Token(Token = "0x40004C3")]
    	Update,
    	[Token(Token = "0x40004C4")]
    	LastUpdate,
    	[Token(Token = "0x40004C5")]
    	PreLateUpdate,
    	[Token(Token = "0x40004C6")]
    	LastPreLateUpdate,
    	[Token(Token = "0x40004C7")]
    	PostLateUpdate,
    	[Token(Token = "0x40004C8")]
    	LastPostLateUpdate,
    	[Token(Token = "0x40004C9")]
    	TimeUpdate,
    	[Token(Token = "0x40004CA")]
    	LastTimeUpdate
    }
}
