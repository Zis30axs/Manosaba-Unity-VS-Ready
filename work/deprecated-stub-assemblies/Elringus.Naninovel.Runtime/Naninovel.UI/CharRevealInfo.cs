using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x20005AD")]
    public readonly struct CharRevealInfo
    {
    	[Token(Token = "0x1700041F")]
    	public float StartTime
    	{
    		[Token(Token = "0x6001C48")]
    		[Address(RVA = "0x49EDC0", Offset = "0x49D7C0", VA = "0x18049EDC0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x17000420")]
    	public float Duration
    	{
    		[Token(Token = "0x6001C49")]
    		[Address(RVA = "0x49EDD0", Offset = "0x49D7D0", VA = "0x18049EDD0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x17000421")]
    	public AsyncToken Token
    	{
    		[Token(Token = "0x6001C4A")]
    		[Address(RVA = "0x456320", Offset = "0x454D20", VA = "0x180456320")]
    		[CompilerGenerated]
    		get
    		{
    			return default(AsyncToken);
    		}
    	}

    	[Token(Token = "0x6001C4B")]
    	[Address(RVA = "0x49EDE0", Offset = "0x49D7E0", VA = "0x18049EDE0")]
    	public CharRevealInfo(float startTime, float duration, AsyncToken token)
    	{ this = default; }
    }
}
