using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x2000034")]
[UsedImplicitly]
public class CancelGosub : Command
{
	// RECONSTRUCTED | Confidence: A (behavior), B (`UniTask.CompletedTask` spelling) | RVA: 0x319490, 179 B
	// `Engine.GetServiceOrErr<IWitchTrialsScriptPlayer>()`, then `sub_22E0`
	// with ecx = 1 = IWitchTrialsScriptPlayer slot 1 `CancelGosub()`. Returns
	// an all-zero Naninovel.UniTask after a class-init check, i.e. it completes
	// synchronously and ignores `token`.
	[Token(Token = "0x60000B7")]
	[Address(RVA = "0x319490", Offset = "0x317E90", VA = "0x180319490", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		Engine.GetServiceOrErr<WitchTrials.Models.IWitchTrialsScriptPlayer>().CancelGosub();
		return UniTask.CompletedTask;
	}

	[Token(Token = "0x60000B8")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public CancelGosub()
	{
	}
}
