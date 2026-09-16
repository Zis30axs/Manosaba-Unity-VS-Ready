using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x2000041")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("hideCredit")]
public class HideCreditsUi : Command
{
	// RECONSTRUCTED | Confidence: A | RVA: 0x31C890, 216 B
	// 
	// `Engine.GetServiceOrErr<IUIManager>()` -> `GetUI<ICreditsUI>()` (both null-checked, NRE otherwise)
	// -> ICreditsUI slot 2 `Stop()`; then the Naninovel UniTask class-init check and a zero return.
	[Token(Token = "0x60000D8")]
	[Address(RVA = "0x31C890", Offset = "0x31B290", VA = "0x18031C890", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		Engine.GetServiceOrErr<IUIManager>().GetUI<ICreditsUI>().Stop();
		return UniTask.CompletedTask;
	}

	[Token(Token = "0x60000D9")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public HideCreditsUi()
	{
	}
}
