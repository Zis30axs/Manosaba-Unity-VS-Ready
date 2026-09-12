using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x20005CA")]
public class TitleExternalScriptsButton : ScriptableButton
{
	[Token(Token = "0x4001630")]
	[FieldOffset(Offset = "0x90")]
	private IUIManager uiManager;

	[Token(Token = "0x6001CFA")]
	[Address(RVA = "0x4AAEF0", Offset = "0x4A98F0", VA = "0x1804AAEF0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001CFB")]
	[Address(RVA = "0x4AAFB0", Offset = "0x4A99B0", VA = "0x1804AAFB0", Slot = "6")]
	protected override void Start()
	{
	}

	[Token(Token = "0x6001CFC")]
	[Address(RVA = "0x4AB050", Offset = "0x4A9A50", VA = "0x1804AB050", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x6001CFD")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public TitleExternalScriptsButton()
	{
	}
}
