using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x200053A")]
    public class ControlPanelSkipButton : ScriptableLabeledButton
    {
    	[Token(Token = "0x4001420")]
    	[FieldOffset(Offset = "0x90")]
    	[SerializeField]
    	private Color activeColorMultiplier;

    	[Token(Token = "0x4001421")]
    	[FieldOffset(Offset = "0xA0")]
    	private IScriptPlayer player;

    	[Token(Token = "0x60019E8")]
    	[Address(RVA = "0x620E30", Offset = "0x61F830", VA = "0x180620E30", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x60019E9")]
    	[Address(RVA = "0x620EF0", Offset = "0x61F8F0", VA = "0x180620EF0", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x60019EA")]
    	[Address(RVA = "0x620FD0", Offset = "0x61F9D0", VA = "0x180620FD0", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x60019EB")]
    	[Address(RVA = "0x6210B0", Offset = "0x61FAB0", VA = "0x1806210B0", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x60019EC")]
    	[Address(RVA = "0x61EF90", Offset = "0x61D990", VA = "0x18061EF90")]
    	private void HandleSkipModeChange(bool enabled)
    	{
    	}

    	[Token(Token = "0x60019ED")]
    	[Address(RVA = "0x61F000", Offset = "0x61DA00", VA = "0x18061F000")]
    	public ControlPanelSkipButton()
    	{
    	}
    }
}
