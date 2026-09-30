using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20002CD")]
    public interface ITweener<TTweenValue> where TTweenValue : struct, ITweenValue
    {
    	[Token(Token = "0x170001E1")]
    	TTweenValue Tween
    	{
    		[Token(Token = "0x6000D72")]
    		get;
    	}

    	[Token(Token = "0x170001E2")]
    	bool Running
    	{
    		[Token(Token = "0x6000D73")]
    		get;
    	}

    	[Token(Token = "0x6000D74")]
    	void Run(TTweenValue tween, [Optional] AsyncToken token, [Optional] Object target);

    	[Token(Token = "0x6000D75")]
    	UniTask RunAwaitable(TTweenValue tween, [Optional] AsyncToken token, [Optional] Object target);

    	[Token(Token = "0x6000D76")]
    	void Stop();

    	[Token(Token = "0x6000D77")]
    	void CompleteInstantly();
    }
}
