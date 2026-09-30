using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x20005B3")]
    public interface IRevealableText
    {
    	[Token(Token = "0x17000424")]
    	string Text
    	{
    		[Token(Token = "0x6001C69")]
    		get;
    		[Token(Token = "0x6001C6A")]
    		set;
    	}

    	[Token(Token = "0x17000425")]
    	Color TextColor
    	{
    		[Token(Token = "0x6001C6B")]
    		get;
    		[Token(Token = "0x6001C6C")]
    		set;
    	}

    	[Token(Token = "0x17000426")]
    	float RevealProgress
    	{
    		[Token(Token = "0x6001C6D")]
    		get;
    		[Token(Token = "0x6001C6E")]
    		set;
    	}

    	[Token(Token = "0x6001C6F")]
    	bool RevealNextChars(int count, float duration, AsyncToken token);

    	[Token(Token = "0x6001C70")]
    	Vector2 GetLastRevealedCharPosition();

    	[Token(Token = "0x6001C71")]
    	char GetLastRevealedChar();
    }
}
