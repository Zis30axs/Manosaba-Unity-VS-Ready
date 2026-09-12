using Il2CppDummyDll;

namespace GigaCreation.Tools.Debugging.TextDisplays;

[Token(Token = "0x200000B")]
public class DebugTextPreferenceResolution : DebugTextPreferenceBase
{
	[Token(Token = "0x17000005")]
	protected override string LabelText
	{
		[Token(Token = "0x6000022")]
		[Address(RVA = "0xD46A00", Offset = "0xD45400", VA = "0x180D46A00", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000023")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugTextPreferenceResolution()
	{
	}
}
