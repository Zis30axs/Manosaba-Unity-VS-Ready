using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x20005C2")]
    public class TipsReturnButton : ScriptableButton
    {
    	[Token(Token = "0x400161F")]
    	[FieldOffset(Offset = "0x90")]
    	private ITipsUI tipsUI;

    	[Token(Token = "0x6001CE0")]
    	[Address(RVA = "0x4A8CE0", Offset = "0x4A76E0", VA = "0x1804A8CE0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001CE1")]
    	[Address(RVA = "0x4A8D90", Offset = "0x4A7790", VA = "0x1804A8D90", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x6001CE2")]
    	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
    	public TipsReturnButton()
    	{
    	}
    }
}
