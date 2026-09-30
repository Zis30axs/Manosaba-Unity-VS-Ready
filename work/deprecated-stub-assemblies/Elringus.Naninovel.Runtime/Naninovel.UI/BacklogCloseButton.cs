using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x20004F6")]
    public class BacklogCloseButton : ScriptableLabeledButton
    {
    	[Token(Token = "0x400134E")]
    	[FieldOffset(Offset = "0x90")]
    	private BacklogPanel backlogPanel;

    	[Token(Token = "0x60018BA")]
    	[Address(RVA = "0x60DB10", Offset = "0x60C510", VA = "0x18060DB10", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x60018BB")]
    	[Address(RVA = "0x60DBC0", Offset = "0x60C5C0", VA = "0x18060DBC0", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x60018BC")]
    	[Address(RVA = "0x53F870", Offset = "0x53E270", VA = "0x18053F870")]
    	public BacklogCloseButton()
    	{
    	}
    }
}
