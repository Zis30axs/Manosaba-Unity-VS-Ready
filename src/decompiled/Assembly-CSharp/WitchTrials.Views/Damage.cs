using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x200005F")]
[UsedImplicitly]
public class Damage : Command
{
	// RECONSTRUCTED | Confidence: A (behavior) | RVA: 0x325700, 65 B
	// The whole body is a class-init check on `Naninovel.UniTask` followed by
	// `xor eax, eax; ret`: the command does nothing and completes
	// synchronously, ignoring `token`. The class-init check points to a static
	// member access, so `UniTask.CompletedTask` rather than `default` is the
	// likely spelling (Confidence B for that spelling only).
	[Token(Token = "0x6000137")]
	[Address(RVA = "0x325700", Offset = "0x324100", VA = "0x180325700", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return UniTask.CompletedTask;
	}

	[Token(Token = "0x6000138")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public Damage()
	{
	}
}
