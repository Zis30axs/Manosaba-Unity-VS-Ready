using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006C3")]
    public class EndIf : Command
    {
    	[Token(Token = "0x6002096")]
    	[Address(RVA = "0x4EFB70", Offset = "0x4EE570", VA = "0x1804EFB70", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002097")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public EndIf()
    	{
    	}
    }
}
