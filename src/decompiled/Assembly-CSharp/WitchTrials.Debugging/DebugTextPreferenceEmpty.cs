using GigaCreation.Tools.Debugging.TextDisplays;
using Il2CppDummyDll;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001C7")]
public class DebugTextPreferenceEmpty : DebugTextPreferenceBase
{
	[Token(Token = "0x1700014B")]
	protected override string LabelText
	{
		[Token(Token = "0x60007BB")]
		[Address(RVA = "0x3B4250", Offset = "0x3B2C50", VA = "0x1803B4250", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60007BC")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugTextPreferenceEmpty()
	{
	}
}
