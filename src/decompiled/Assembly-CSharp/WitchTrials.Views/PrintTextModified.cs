using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;

namespace WitchTrials.Views;

[Token(Token = "0x200005B")]
[UsedImplicitly]
[CommandAlias("print")]
public class PrintTextModified : PrintText
{
	[Token(Token = "0x600012F")]
	[Address(RVA = "0x3251A0", Offset = "0x323BA0", VA = "0x1803251A0", Slot = "33")]
	protected override bool ShouldPlayAutoVoice()
	{
		return default(bool);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x323690, 7 B
	// FOLDED BODY shared with `PrintDebateText..ctor`: `xor edx, edx; jmp
	// Naninovel.Commands.PrintText..ctor`. The constructor does nothing but
	// chain to the base, so there are no instance field initializers.
	// Counted once (RECONSTRUCTION.md folded-body convention).
	[Token(Token = "0x6000130")]
	[Address(RVA = "0x323690", Offset = "0x322090", VA = "0x180323690")]
	public PrintTextModified()
	{
	}
}
