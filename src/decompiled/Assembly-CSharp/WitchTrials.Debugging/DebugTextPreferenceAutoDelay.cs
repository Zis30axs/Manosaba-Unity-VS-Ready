using GigaCreation.Tools.Debugging.TextDisplays;
using Il2CppDummyDll;
using Naninovel;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001C5")]
public class DebugTextPreferenceAutoDelay : DebugTextPreferenceBase
{
	[Token(Token = "0x4000861")]
	[FieldOffset(Offset = "0x50")]
	private TextPrintersConfiguration _tpConf;

	[Token(Token = "0x4000862")]
	[FieldOffset(Offset = "0x58")]
	private ScriptPlayerConfiguration _spConf;

	[Token(Token = "0x17000146")]
	protected override string LabelText
	{
		[Token(Token = "0x60007B4")]
		[Address(RVA = "0x3B3B50", Offset = "0x3B2550", VA = "0x1803B3B50", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000147")]
	private TextPrintersConfiguration TpConf
	{
		[Token(Token = "0x60007B5")]
		[Address(RVA = "0x3B3E00", Offset = "0x3B2800", VA = "0x1803B3E00")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000148")]
	private ScriptPlayerConfiguration SpConf
	{
		[Token(Token = "0x60007B6")]
		[Address(RVA = "0x3B3EC0", Offset = "0x3B28C0", VA = "0x1803B3EC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60007B7")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugTextPreferenceAutoDelay()
	{
	}
}
