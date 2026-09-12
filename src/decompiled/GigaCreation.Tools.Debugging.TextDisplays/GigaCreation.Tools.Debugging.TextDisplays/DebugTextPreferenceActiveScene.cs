using Il2CppDummyDll;

namespace GigaCreation.Tools.Debugging.TextDisplays;

[Token(Token = "0x2000006")]
public class DebugTextPreferenceActiveScene : DebugTextPreferenceBase
{
	[Token(Token = "0x17000001")]
	protected override string LabelText
	{
		[Token(Token = "0x600000B")]
		[Address(RVA = "0xD457E0", Offset = "0xD441E0", VA = "0x180D457E0", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600000C")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugTextPreferenceActiveScene()
	{
	}
}
