using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x2000567")]
    public class PauseUI : CustomUI, IPauseUI, IManagedUI
    {
    	[Token(Token = "0x6001AC6")]
    	[Address(RVA = "0x62F4D0", Offset = "0x62DED0", VA = "0x18062F4D0", Slot = "83")]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001AC7")]
    	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
    	public PauseUI()
    	{
    	}
    }
}
