using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;

namespace WitchTrials.Views
{
    [Token(Token = "0x200005B")]
    [UsedImplicitly]
    [CommandAlias("print")]
    public class PrintTextModified : PrintText
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0x3251A0, 446 B
    	// 
    	// The base PrintText.ShouldPlayAutoVoice (0x4FE180) is byte-for-byte the same except for the middle test:
    	// auto voicing enabled (AudioConfiguration +0x40 `EnableAutoVoicing`), a valid PlaybackSpot (Command +0x18,
    	// inlined as script path non-null and non-empty), and IScriptPlayer interface slot 16 `SkipActive` negated.
    	// The modification inserts the `wait` escape hatch: when the command explicitly passes `wait:false`,
    	// the voice plays even while skipping. `Nullable<bool>.op_Implicit` is the conversion, not `.Value`.
    	[Token(Token = "0x600012F")]
    	[Address(RVA = "0x3251A0", Offset = "0x323BA0", VA = "0x1803251A0", Slot = "33")]
    	protected override bool ShouldPlayAutoVoice()
    	{
    		if (!Engine.GetServiceOrErr<IAudioManager>().Configuration.EnableAutoVoicing)
    		{
    			return false;
    		}
    		if (!PlaybackSpot.Valid)
    		{
    			return false;
    		}
    		if (Assigned(WaitForInput) && !WaitForInput)
    		{
    			return true;
    		}
    		return !ScriptPlayer.SkipActive;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x323690, 7 B
    	// FOLDED BODY shared with `PrintDebateText..ctor`: `xor edx, edx; jmp
    	// Naninovel.Commands.PrintText..ctor`. The constructor does nothing but
    	// chain to the base, so there are no instance field initializers.
    	// Not counted as progress: `ranked.json` lists this 7-byte shared body
    	// under `aliased` (alias 2, <= 0x40), outside every subsystem bucket.
    	[Token(Token = "0x6000130")]
    	[Address(RVA = "0x323690", Offset = "0x322090", VA = "0x180323690")]
    	public PrintTextModified()
    	{
    	}
    }
}
