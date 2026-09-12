using Il2CppDummyDll;
using Naninovel;

namespace WitchTrials.Models;

[Token(Token = "0x200017B")]
public interface IWitchTrialsScriptPlayer : IScriptPlayer, IEngineService<ScriptPlayerConfiguration>, IEngineService
{
	[Token(Token = "0x6000679")]
	UniTask ProcessAutoObjectionAsync(string[] objectionIds);

	[Token(Token = "0x600067A")]
	void CancelGosub();
}
