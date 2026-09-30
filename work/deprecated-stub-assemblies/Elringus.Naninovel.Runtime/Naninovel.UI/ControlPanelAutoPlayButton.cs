using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x2000530")]
    public class ControlPanelAutoPlayButton : ScriptableLabeledButton
    {
    	[Token(Token = "0x400140D")]
    	[FieldOffset(Offset = "0x90")]
    	[SerializeField]
    	private Color activeColorMultiplier;

    	[Token(Token = "0x400140E")]
    	[FieldOffset(Offset = "0xA0")]
    	private IScriptPlayer player;

    	[Token(Token = "0x60019C3")]
    	[Address(RVA = "0x61EC90", Offset = "0x61D690", VA = "0x18061EC90", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x60019C4")]
    	[Address(RVA = "0x61ED50", Offset = "0x61D750", VA = "0x18061ED50", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x60019C5")]
    	[Address(RVA = "0x61EE30", Offset = "0x61D830", VA = "0x18061EE30", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x60019C6")]
    	[Address(RVA = "0x61EF10", Offset = "0x61D910", VA = "0x18061EF10", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x60019C7")]
    	[Address(RVA = "0x61EF90", Offset = "0x61D990", VA = "0x18061EF90")]
    	private void HandleAutoModeChange(bool enabled)
    	{
    	}

    	[Token(Token = "0x60019C8")]
    	[Address(RVA = "0x61F000", Offset = "0x61DA00", VA = "0x18061F000")]
    	public ControlPanelAutoPlayButton()
    	{
    	}
    }
}
