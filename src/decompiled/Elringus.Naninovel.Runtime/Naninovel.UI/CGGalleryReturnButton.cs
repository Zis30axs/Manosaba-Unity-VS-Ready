using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000506")]
public class CGGalleryReturnButton : ScriptableButton
{
	[Token(Token = "0x4001390")]
	[FieldOffset(Offset = "0x90")]
	private ICGGalleryUI cgGalleryUI;

	[Token(Token = "0x600190E")]
	[Address(RVA = "0x614390", Offset = "0x612D90", VA = "0x180614390", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x600190F")]
	[Address(RVA = "0x4A8D90", Offset = "0x4A7790", VA = "0x1804A8D90", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x6001910")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public CGGalleryReturnButton()
	{
	}
}
