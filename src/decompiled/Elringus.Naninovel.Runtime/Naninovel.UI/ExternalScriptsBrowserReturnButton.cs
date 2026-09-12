using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000542")]
public class ExternalScriptsBrowserReturnButton : ScriptableButton
{
	[Token(Token = "0x4001445")]
	[FieldOffset(Offset = "0x90")]
	private ExternalScriptsBrowserPanel externalScriptsBrowser;

	[Token(Token = "0x6001A15")]
	[Address(RVA = "0x625760", Offset = "0x624160", VA = "0x180625760", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001A16")]
	[Address(RVA = "0x60DBC0", Offset = "0x60C5C0", VA = "0x18060DBC0", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x6001A17")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public ExternalScriptsBrowserReturnButton()
	{
	}
}
