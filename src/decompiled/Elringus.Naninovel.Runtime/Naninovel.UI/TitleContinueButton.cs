using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x20005C6")]
public class TitleContinueButton : ScriptableButton
{
	[Token(Token = "0x4001620")]
	[FieldOffset(Offset = "0x90")]
	private IStateManager stateManager;

	[Token(Token = "0x4001621")]
	[FieldOffset(Offset = "0x98")]
	private IUIManager uiManager;

	[Token(Token = "0x6001CEA")]
	[Address(RVA = "0x4A92B0", Offset = "0x4A7CB0", VA = "0x1804A92B0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001CEB")]
	[Address(RVA = "0x4A9400", Offset = "0x4A7E00", VA = "0x1804A9400", Slot = "6")]
	protected override void Start()
	{
	}

	[Token(Token = "0x6001CEC")]
	[Address(RVA = "0x4A9490", Offset = "0x4A7E90", VA = "0x1804A9490", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6001CED")]
	[Address(RVA = "0x4A9610", Offset = "0x4A8010", VA = "0x1804A9610", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6001CEE")]
	[Address(RVA = "0x4A9790", Offset = "0x4A8190", VA = "0x1804A9790", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x6001CEF")]
	[Address(RVA = "0x4A9400", Offset = "0x4A7E00", VA = "0x1804A9400")]
	private void ControlInteractability(string _)
	{
	}

	[Token(Token = "0x6001CF0")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public TitleContinueButton()
	{
	}
}
