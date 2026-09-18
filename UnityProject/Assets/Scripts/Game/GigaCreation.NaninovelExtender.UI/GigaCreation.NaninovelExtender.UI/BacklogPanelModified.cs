using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x2000006")]
    public class BacklogPanelModified : BacklogPanel
    {
    	[Token(Token = "0x400000C")]
    	[FieldOffset(Offset = "0x148")]
    	private IStateManager _naniStateManager;

    	[Token(Token = "0x400000D")]
    	[FieldOffset(Offset = "0x150")]
    	private IInputManager _naniInputManager;

    	[Token(Token = "0x17000003")]
    	private IStateManager NaniStateManager
    	{
    		[Token(Token = "0x600000E")]
    		[Address(RVA = "0xD35360", Offset = "0xD33D60", VA = "0x180D35360")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000004")]
    	private IInputManager NaniInputManager
    	{
    		[Token(Token = "0x600000F")]
    		[Address(RVA = "0xD35430", Offset = "0xD33E30", VA = "0x180D35430")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000010")]
    	[Address(RVA = "0xD35500", Offset = "0xD33F00", VA = "0x180D35500", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x6000011")]
    	[Address(RVA = "0xD35D20", Offset = "0xD34720", VA = "0x180D35D20", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x6000012")]
    	[Address(RVA = "0x61B680", Offset = "0x61A080", VA = "0x18061B680")]
    	private void HandleGameLoadStarted(GameSaveLoadArgs args)
    	{
    	}

    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0x33E9A0", Offset = "0x33D3A0", VA = "0x18033E9A0")]
    	public BacklogPanelModified()
    	{
    	}
    }
}
