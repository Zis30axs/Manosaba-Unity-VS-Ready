using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000546")]
public class GameSettingsFontDropdown : ScriptableDropdown
{
	[Token(Token = "0x4001449")]
	[FieldOffset(Offset = "0x0")]
	[ManagedText("DefaultUI")]
	protected static string DefaultFontName;

	[Token(Token = "0x400144A")]
	[FieldOffset(Offset = "0x90")]
	private IUIManager uiManager;

	[Token(Token = "0x400144B")]
	[FieldOffset(Offset = "0x98")]
	private ICommunityLocalization communityL10n;

	[Token(Token = "0x6001A21")]
	[Address(RVA = "0x625BF0", Offset = "0x6245F0", VA = "0x180625BF0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001A22")]
	[Address(RVA = "0x625D40", Offset = "0x624740", VA = "0x180625D40", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6001A23")]
	[Address(RVA = "0x625F00", Offset = "0x624900", VA = "0x180625F00", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6001A24")]
	[Address(RVA = "0x6260C0", Offset = "0x624AC0", VA = "0x1806260C0", Slot = "62")]
	protected override void OnValueChanged(int value)
	{
	}

	[Token(Token = "0x6001A25")]
	[Address(RVA = "0x6261A0", Offset = "0x624BA0", VA = "0x1806261A0")]
	private void InitializeOptions()
	{
	}

	[Token(Token = "0x6001A26")]
	[Address(RVA = "0x6266B0", Offset = "0x6250B0", VA = "0x1806266B0")]
	private int GetCurrentIndex()
	{
		return default(int);
	}

	[Token(Token = "0x6001A27")]
	[Address(RVA = "0x626840", Offset = "0x625240", VA = "0x180626840")]
	private void HandleLocaleChanged(LocaleChangedArgs _)
	{
	}

	[Token(Token = "0x6001A28")]
	[Address(RVA = "0x53E9E0", Offset = "0x53D3E0", VA = "0x18053E9E0")]
	public GameSettingsFontDropdown()
	{
	}
}
