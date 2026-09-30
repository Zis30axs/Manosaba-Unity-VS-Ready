using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x2000538")]
    public class ControlPanelSaveButton : ScriptableButton
    {
    	[Token(Token = "0x400141E")]
    	[FieldOffset(Offset = "0x90")]
    	private IUIManager uiManager;

    	[Token(Token = "0x60019E2")]
    	[Address(RVA = "0x620A00", Offset = "0x61F400", VA = "0x180620A00", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x60019E3")]
    	[Address(RVA = "0x620AC0", Offset = "0x61F4C0", VA = "0x180620AC0", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x60019E4")]
    	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
    	public ControlPanelSaveButton()
    	{
    	}
    }
}
