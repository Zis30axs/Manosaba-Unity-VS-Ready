using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000556")]
public class GameSettingsSkipModeDropdown : ScriptableDropdown
{
	[Token(Token = "0x400147B")]
	[FieldOffset(Offset = "0x0")]
	[ManagedText("DefaultUI")]
	protected static string ReadOnly;

	[Token(Token = "0x400147C")]
	[FieldOffset(Offset = "0x8")]
	[ManagedText("DefaultUI")]
	protected static string Everything;

	[Token(Token = "0x400147D")]
	[FieldOffset(Offset = "0x90")]
	private IScriptPlayer player;

	[Token(Token = "0x6001A7B")]
	[Address(RVA = "0x62BC90", Offset = "0x62A690", VA = "0x18062BC90", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001A7C")]
	[Address(RVA = "0x62BD50", Offset = "0x62A750", VA = "0x18062BD50", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6001A7D")]
	[Address(RVA = "0x62BF10", Offset = "0x62A910", VA = "0x18062BF10", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6001A7E")]
	[Address(RVA = "0x62C0D0", Offset = "0x62AAD0", VA = "0x18062C0D0", Slot = "62")]
	protected override void OnValueChanged(int value)
	{
	}

	[Token(Token = "0x6001A7F")]
	[Address(RVA = "0x62C130", Offset = "0x62AB30", VA = "0x18062C130")]
	private void InitializeOptions()
	{
	}

	[Token(Token = "0x6001A80")]
	[Address(RVA = "0x62C300", Offset = "0x62AD00", VA = "0x18062C300")]
	private void HandleLocaleChanged(LocaleChangedArgs _)
	{
	}

	[Token(Token = "0x6001A81")]
	[Address(RVA = "0x53E9E0", Offset = "0x53D3E0", VA = "0x18053E9E0")]
	public GameSettingsSkipModeDropdown()
	{
	}
}
