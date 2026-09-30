using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x2000548")]
    public class GameSettingsFontSizeDropdown : ScriptableDropdown
    {
    	[Token(Token = "0x400144E")]
    	[FieldOffset(Offset = "0x0")]
    	[ManagedText("DefaultUI")]
    	protected static string Small;

    	[Token(Token = "0x400144F")]
    	[FieldOffset(Offset = "0x8")]
    	[ManagedText("DefaultUI")]
    	protected static string Default;

    	[Token(Token = "0x4001450")]
    	[FieldOffset(Offset = "0x10")]
    	[ManagedText("DefaultUI")]
    	protected static string Large;

    	[Token(Token = "0x4001451")]
    	[FieldOffset(Offset = "0x18")]
    	[ManagedText("DefaultUI")]
    	protected static string ExtraLarge;

    	[Token(Token = "0x4001452")]
    	[FieldOffset(Offset = "0x90")]
    	[Tooltip("Index of the dropdown list associated with default font size.")]
    	[SerializeField]
    	private int defaultSizeIndex;

    	[Token(Token = "0x4001453")]
    	[FieldOffset(Offset = "0x98")]
    	private IUIManager uiManager;

    	[Token(Token = "0x6001A2E")]
    	[Address(RVA = "0x626A50", Offset = "0x625450", VA = "0x180626A50", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001A2F")]
    	[Address(RVA = "0x626B10", Offset = "0x625510", VA = "0x180626B10", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x6001A30")]
    	[Address(RVA = "0x626CE0", Offset = "0x6256E0", VA = "0x180626CE0", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x6001A31")]
    	[Address(RVA = "0x626EA0", Offset = "0x6258A0", VA = "0x180626EA0", Slot = "62")]
    	protected override void OnValueChanged(int index)
    	{
    	}

    	[Token(Token = "0x6001A32")]
    	[Address(RVA = "0x626F10", Offset = "0x625910", VA = "0x180626F10", Slot = "63")]
    	protected virtual void InitializeOptions()
    	{
    	}

    	[Token(Token = "0x6001A33")]
    	[Address(RVA = "0x627230", Offset = "0x625C30", VA = "0x180627230")]
    	private void HandleLocaleChanged(LocaleChangedArgs _)
    	{
    	}

    	[Token(Token = "0x6001A34")]
    	[Address(RVA = "0x627250", Offset = "0x625C50", VA = "0x180627250")]
    	public GameSettingsFontSizeDropdown()
    	{
    	}
    }
}
