using GigaCreation.Tools.Debugging.TextDisplays;
using Il2CppDummyDll;
using Naninovel;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001CB")]
public class DebugTextPreferenceScriptPlayMode : DebugTextPreferenceBase
{
	[Token(Token = "0x4000869")]
	[FieldOffset(Offset = "0x50")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x17000150")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x60007C5")]
		[Address(RVA = "0x3B4AD0", Offset = "0x3B34D0", VA = "0x1803B4AD0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000151")]
	protected override string LabelText
	{
		[Token(Token = "0x60007C6")]
		[Address(RVA = "0x3B4B90", Offset = "0x3B3590", VA = "0x1803B4B90", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60007C7")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugTextPreferenceScriptPlayMode()
	{
	}
}
