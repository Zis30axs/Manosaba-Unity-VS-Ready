using Il2CppDummyDll;

namespace GigaCreation.NaninovelExtender.Ui;

[Token(Token = "0x200001D")]
public class LoadingIndicatorProgressBar : LoadingIndicatorBase
{
	[Token(Token = "0x600005F")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
	public override void OnShow()
	{
	}

	[Token(Token = "0x6000060")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "6")]
	public override void OnHide()
	{
	}

	[Token(Token = "0x6000061")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public LoadingIndicatorProgressBar()
	{
	}
}
