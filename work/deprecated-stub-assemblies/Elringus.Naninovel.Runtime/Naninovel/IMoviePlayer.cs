using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20003B3")]
    public interface IMoviePlayer : IEngineService<MoviesConfiguration>, IEngineService
    {
    	[Token(Token = "0x1700026C")]
    	bool Playing
    	{
    		[Token(Token = "0x60011E6")]
    		get;
    	}

    	[Token(Token = "0x1400005D")]
    	event Action OnMoviePlay;

    	[Token(Token = "0x1400005E")]
    	event Action OnMovieStop;

    	[Token(Token = "0x60011E7")]
    	UniTask<Texture> Play(string movieName, [Optional] AsyncToken token);

    	[Token(Token = "0x60011E8")]
    	void Stop();

    	[Token(Token = "0x60011E9")]
    	UniTask HoldResources(string movieName, object holder);

    	[Token(Token = "0x60011EA")]
    	void ReleaseResources(string movieName, object holder);
    }
}
