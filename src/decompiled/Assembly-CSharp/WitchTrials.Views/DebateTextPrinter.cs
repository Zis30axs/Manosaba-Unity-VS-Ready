using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000026")]
[UsedImplicitly]
[ActorResources(typeof(DebateTextPrinterPanel), false)]
public class DebateTextPrinter : UITextPrinter, IDebateTextPrinterActor, ITextPrinterActor, IActor
{
	[Token(Token = "0x17000009")]
	public DebateTextAnimationKind AnimationKind
	{
		[Token(Token = "0x600008C")]
		[Address(RVA = "0x313940", Offset = "0x312340", VA = "0x180313940", Slot = "86")]
		[CompilerGenerated]
		get
		{
			return default(DebateTextAnimationKind);
		}
		[Token(Token = "0x600008D")]
		[Address(RVA = "0x313950", Offset = "0x312350", VA = "0x180313950", Slot = "87")]
		[CompilerGenerated]
		set
		{
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x313960, 8 B
	// `xor r9d, r9d; jmp Naninovel.UITextPrinter..ctor` with rdx/r8 (id, meta)
	// passed through untouched: only the base-constructor call, no field
	// initializers.
	[Token(Token = "0x600008E")]
	[Address(RVA = "0x313960", Offset = "0x312360", VA = "0x180313960")]
	public DebateTextPrinter(string id, TextPrinterMetadata meta)
		: base(id, meta)
	{
	}
}
