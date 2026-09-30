using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000246")]
    public interface ITime
    {
    	[Token(Token = "0x17000176")]
    	float Time
    	{
    		[Token(Token = "0x6000A18")]
    		get;
    	}

    	[Token(Token = "0x17000177")]
    	float DeltaTime
    	{
    		[Token(Token = "0x6000A19")]
    		get;
    	}

    	[Token(Token = "0x17000178")]
    	float UnscaledTime
    	{
    		[Token(Token = "0x6000A1A")]
    		get;
    	}

    	[Token(Token = "0x17000179")]
    	float UnscaledDeltaTime
    	{
    		[Token(Token = "0x6000A1B")]
    		get;
    	}

    	[Token(Token = "0x1700017A")]
    	float TimeScale
    	{
    		[Token(Token = "0x6000A1C")]
    		get;
    		[Token(Token = "0x6000A1D")]
    		set;
    	}

    	[Token(Token = "0x1700017B")]
    	int FrameCount
    	{
    		[Token(Token = "0x6000A1E")]
    		get;
    	}
    }
}
