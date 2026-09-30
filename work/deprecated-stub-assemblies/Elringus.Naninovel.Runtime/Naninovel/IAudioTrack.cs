using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000202")]
    public interface IAudioTrack
    {
    	[Token(Token = "0x17000140")]
    	bool Playing
    	{
    		[Token(Token = "0x60008F5")]
    		get;
    	}

    	[Token(Token = "0x17000141")]
    	bool Loop
    	{
    		[Token(Token = "0x60008F6")]
    		get;
    		[Token(Token = "0x60008F7")]
    		set;
    	}

    	[Token(Token = "0x17000142")]
    	bool Mute
    	{
    		[Token(Token = "0x60008F8")]
    		get;
    		[Token(Token = "0x60008F9")]
    		set;
    	}

    	[Token(Token = "0x17000143")]
    	float Volume
    	{
    		[Token(Token = "0x60008FA")]
    		get;
    		[Token(Token = "0x60008FB")]
    		set;
    	}

    	[Token(Token = "0x14000012")]
    	event Action OnPlay;

    	[Token(Token = "0x14000013")]
    	event Action OnStop;

    	[Token(Token = "0x60008FC")]
    	void Play();

    	[Token(Token = "0x60008FD")]
    	void Stop();

    	[Token(Token = "0x60008FE")]
    	UniTask Fade(float volume, float fadeTime, [Optional] AsyncToken token);
    }
}
