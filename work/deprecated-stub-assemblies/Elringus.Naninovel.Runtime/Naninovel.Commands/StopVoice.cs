using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands
{
    [Token(Token = "0x200069E")]
    public class StopVoice : AudioCommand
    {
    	[Token(Token = "0x6002032")]
    	[Address(RVA = "0x4E42C0", Offset = "0x4E2CC0", VA = "0x1804E42C0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002033")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public StopVoice()
    	{
    	}
    }
}
