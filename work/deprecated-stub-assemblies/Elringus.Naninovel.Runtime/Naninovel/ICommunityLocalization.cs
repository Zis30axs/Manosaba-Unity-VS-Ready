using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200037C")]
    public interface ICommunityLocalization : IEngineService
    {
    	[Token(Token = "0x17000245")]
    	bool Active
    	{
    		[Token(Token = "0x60010DF")]
    		get;
    	}

    	[Token(Token = "0x17000246")]
    	string Author
    	{
    		[Token(Token = "0x60010E0")]
    		get;
    	}

    	[Token(Token = "0x17000247")]
    	IResourceLoader<TextAsset> TextLoader
    	{
    		[Token(Token = "0x60010E1")]
    		get;
    	}

    	[Token(Token = "0x60010E2")]
    	UniTask<TMP_FontAsset> LoadFont();
    }
}
