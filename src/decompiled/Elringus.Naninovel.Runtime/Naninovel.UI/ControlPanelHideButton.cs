using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000531")]
public class ControlPanelHideButton : ScriptableLabeledButton
{
	[Token(Token = "0x400140F")]
	[FieldOffset(Offset = "0x90")]
	private IUIManager uiManager;

	[Token(Token = "0x60019C9")]
	[Address(RVA = "0x61F050", Offset = "0x61DA50", VA = "0x18061F050", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x60019CA")]
	[Address(RVA = "0x61F110", Offset = "0x61DB10", VA = "0x18061F110", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x60019CB")]
	[Address(RVA = "0x53F870", Offset = "0x53E270", VA = "0x18053F870")]
	public ControlPanelHideButton()
	{
	}
}
