using System;
using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x20005B4")]
    public interface IRevealInfo
    {
    	[Token(Token = "0x17000427")]
    	int LastRevealedCharIndex
    	{
    		[Token(Token = "0x6001C74")]
    		get;
    	}

    	[Token(Token = "0x17000428")]
    	int LastAppendIndex
    	{
    		[Token(Token = "0x6001C75")]
    		get;
    	}

    	[Token(Token = "0x1400008F")]
    	event Action OnChange;

    	[Token(Token = "0x6001C76")]
    	float GetRevealRatio(int charIndex, float modifier = 1f);

    	[Token(Token = "0x6001C77")]
    	float GetAppendTime(int charIndex);
    }
}
