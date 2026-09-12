using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000553")]
public class GameSettingsReturnButton : ScriptableButton
{
	[Token(Token = "0x4001472")]
	[FieldOffset(Offset = "0x90")]
	private GameSettingsMenu menu;

	[Token(Token = "0x6001A6A")]
	[Address(RVA = "0x62AC00", Offset = "0x629600", VA = "0x18062AC00", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001A6B")]
	[Address(RVA = "0x62ACB0", Offset = "0x6296B0", VA = "0x18062ACB0", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x6001A6C")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public GameSettingsReturnButton()
	{
	}
}
