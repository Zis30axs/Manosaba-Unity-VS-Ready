using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x2000539")]
    public class ControlPanelSettingsButton : ScriptableButton
    {
    	[Token(Token = "0x400141F")]
    	[FieldOffset(Offset = "0x90")]
    	private IUIManager uiManager;

    	[Token(Token = "0x60019E5")]
    	[Address(RVA = "0x620C30", Offset = "0x61F630", VA = "0x180620C30", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x60019E6")]
    	[Address(RVA = "0x620CF0", Offset = "0x61F6F0", VA = "0x180620CF0", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x60019E7")]
    	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
    	public ControlPanelSettingsButton()
    	{
    	}
    }
}
