using GigaCreation.Tools.Debugging.TextDisplays;
using Il2CppDummyDll;
using Naninovel;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001CA")]
public class DebugTextPreferencePlaybackSpot : DebugTextPreferenceBase
{
	[Token(Token = "0x4000868")]
	[FieldOffset(Offset = "0x50")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x1700014E")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x60007C2")]
		[Address(RVA = "0x3B4880", Offset = "0x3B3280", VA = "0x1803B4880")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700014F")]
	protected override string LabelText
	{
		[Token(Token = "0x60007C3")]
		[Address(RVA = "0x3B4940", Offset = "0x3B3340", VA = "0x1803B4940", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60007C4")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugTextPreferencePlaybackSpot()
	{
	}
}
