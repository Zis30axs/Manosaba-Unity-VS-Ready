using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x2000532")]
    public class ControlPanelLoadButton : ScriptableButton
    {
    	[Token(Token = "0x4001410")]
    	[FieldOffset(Offset = "0x90")]
    	private IUIManager uiManager;

    	[Token(Token = "0x60019CC")]
    	[Address(RVA = "0x61F170", Offset = "0x61DB70", VA = "0x18061F170", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x60019CD")]
    	[Address(RVA = "0x61F230", Offset = "0x61DC30", VA = "0x18061F230", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x60019CE")]
    	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
    	public ControlPanelLoadButton()
    	{
    	}
    }
}
