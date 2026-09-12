using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x200057C")]
public class SaveLoadMenuReturnButton : ScriptableButton
{
	[Token(Token = "0x4001502")]
	[FieldOffset(Offset = "0x90")]
	private SaveLoadMenu saveLoadMenu;

	[Token(Token = "0x6001B33")]
	[Address(RVA = "0x637F50", Offset = "0x636950", VA = "0x180637F50", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001B34")]
	[Address(RVA = "0x60DBC0", Offset = "0x60C5C0", VA = "0x18060DBC0", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x6001B35")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public SaveLoadMenuReturnButton()
	{
	}
}
