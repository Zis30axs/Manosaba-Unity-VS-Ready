using Il2CppDummyDll;

namespace GigaCreation.Tools.Debugging.TextDisplays;

[Token(Token = "0x200000C")]
public class DebugTextPreferenceSelectedGameObject : DebugTextPreferenceBase
{
	[Token(Token = "0x17000006")]
	protected override string LabelText
	{
		[Token(Token = "0x6000024")]
		[Address(RVA = "0xD46C00", Offset = "0xD45600", VA = "0x180D46C00", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000025")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugTextPreferenceSelectedGameObject()
	{
	}
}
