using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x200053B")]
    public class ControlPanelTipsButton : ScriptableLabeledButton
    {
    	[Token(Token = "0x4001422")]
    	[FieldOffset(Offset = "0x90")]
    	private IUIManager uiManager;

    	[Token(Token = "0x60019EE")]
    	[Address(RVA = "0x621130", Offset = "0x61FB30", VA = "0x180621130", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x60019EF")]
    	[Address(RVA = "0x621280", Offset = "0x61FC80", VA = "0x180621280", Slot = "8")]
    	protected override void OnDestroy()
    	{
    	}

    	[Token(Token = "0x60019F0")]
    	[Address(RVA = "0x621320", Offset = "0x61FD20", VA = "0x180621320", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x60019F1")]
    	[Address(RVA = "0x621460", Offset = "0x61FE60", VA = "0x180621460", Slot = "63")]
    	protected virtual void DisableIfNoTips()
    	{
    	}

    	[Token(Token = "0x60019F2")]
    	[Address(RVA = "0x53F870", Offset = "0x53E270", VA = "0x18053F870")]
    	public ControlPanelTipsButton()
    	{
    	}
    }
}
