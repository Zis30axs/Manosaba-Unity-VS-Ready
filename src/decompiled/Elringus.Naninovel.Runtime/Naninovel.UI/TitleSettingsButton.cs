using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x20005D4")]
public class TitleSettingsButton : ScriptableButton
{
	[Token(Token = "0x400165E")]
	[FieldOffset(Offset = "0x90")]
	private IUIManager uiManager;

	[Token(Token = "0x6001D1B")]
	[Address(RVA = "0x4AEA20", Offset = "0x4AD420", VA = "0x1804AEA20", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001D1C")]
	[Address(RVA = "0x4AEAE0", Offset = "0x4AD4E0", VA = "0x1804AEAE0", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x6001D1D")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public TitleSettingsButton()
	{
	}
}
