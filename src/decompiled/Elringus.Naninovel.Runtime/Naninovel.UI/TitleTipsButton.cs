using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x20005D5")]
public class TitleTipsButton : ScriptableButton
{
	[Token(Token = "0x6001D1E")]
	[Address(RVA = "0x4AEB90", Offset = "0x4AD590", VA = "0x1804AEB90", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001D1F")]
	[Address(RVA = "0x4AEC80", Offset = "0x4AD680", VA = "0x1804AEC80", Slot = "8")]
	protected override void OnDestroy()
	{
	}

	[Token(Token = "0x6001D20")]
	[Address(RVA = "0x4AED20", Offset = "0x4AD720", VA = "0x1804AED20", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x6001D21")]
	[Address(RVA = "0x4AEE10", Offset = "0x4AD810", VA = "0x1804AEE10", Slot = "63")]
	protected virtual void DisableIfNoTips()
	{
	}

	[Token(Token = "0x6001D22")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public TitleTipsButton()
	{
	}
}
