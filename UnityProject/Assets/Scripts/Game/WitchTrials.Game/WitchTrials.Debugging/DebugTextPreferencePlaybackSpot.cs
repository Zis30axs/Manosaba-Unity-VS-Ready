using GigaCreation.Tools.Debugging.TextDisplays;
using Il2CppDummyDll;
using Naninovel;

namespace WitchTrials.Debugging
{
    [Token(Token = "0x20001CA")]
    public class DebugTextPreferencePlaybackSpot : DebugTextPreferenceBase
    {
    	[Token(Token = "0x4000868")]
    	[FieldOffset(Offset = "0x50")]
    	private IScriptPlayer _naniScriptPlayer;

    	[Token(Token = "0x1700014E")]
    	private IScriptPlayer NaniScriptPlayer
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3B4880
    		// Lazy cache in +0x50 via `Engine.GetServiceOrErr<IScriptPlayer>()`.
    		[Token(Token = "0x60007C2")]
    		[Address(RVA = "0x3B4880", Offset = "0x3B3280", VA = "0x1803B4880")]
    		get
    		{
    			return _naniScriptPlayer ??= Engine.GetServiceOrErr<IScriptPlayer>();
    		}
    	}

    	[Token(Token = "0x1700014F")]
    	protected override string LabelText
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3B4940, 392 B
    		// Getter inlined, then the struct-returning interface thunk sub_8630 (`mov r8d, 0x18`) = IScriptPlayer
    		// slot 24 `get_PlaybackSpot`, then `PlaybackSpot.ToString()`.
    		[Token(Token = "0x60007C3")]
    		[Address(RVA = "0x3B4940", Offset = "0x3B3340", VA = "0x1803B4940", Slot = "4")]
    		get
    		{
    			return Engine.Initialized ? NaniScriptPlayer.PlaybackSpot.ToString() : "Naninovel 尚未初始化";
    		}
    	}

    	[Token(Token = "0x60007C4")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DebugTextPreferencePlaybackSpot()
    	{
    	}
    }
}
