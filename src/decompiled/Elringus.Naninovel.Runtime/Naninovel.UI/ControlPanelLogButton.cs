using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000533")]
public class ControlPanelLogButton : ScriptableLabeledButton
{
	[Token(Token = "0x4001411")]
	[FieldOffset(Offset = "0x90")]
	private IUIManager uiManager;

	[Token(Token = "0x60019CF")]
	[Address(RVA = "0x61F3A0", Offset = "0x61DDA0", VA = "0x18061F3A0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x60019D0")]
	[Address(RVA = "0x61F460", Offset = "0x61DE60", VA = "0x18061F460", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x60019D1")]
	[Address(RVA = "0x53F870", Offset = "0x53E270", VA = "0x18053F870")]
	public ControlPanelLogButton()
	{
	}
}
