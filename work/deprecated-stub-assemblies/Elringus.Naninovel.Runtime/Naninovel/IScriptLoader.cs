using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200042D")]
    public interface IScriptLoader : IEngineService
    {
    	[Token(Token = "0x14000064")]
    	event Action<float> OnLoadProgress;

    	[Token(Token = "0x6001469")]
    	UniTask Load(string scriptPath, int startIndex = 0);
    }
}
