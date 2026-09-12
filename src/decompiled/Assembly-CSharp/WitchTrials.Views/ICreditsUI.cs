using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;

namespace WitchTrials.Views;

[Token(Token = "0x20000B1")]
public interface ICreditsUI : IManagedUI
{
	[Token(Token = "0x6000285")]
	UniTask PreloadResourcesAsync(int act, [Optional] AsyncToken token);

	[Token(Token = "0x6000286")]
	UniTask PlayAsync(int act, [Optional] AsyncToken token);

	[Token(Token = "0x6000287")]
	void Stop();
}
