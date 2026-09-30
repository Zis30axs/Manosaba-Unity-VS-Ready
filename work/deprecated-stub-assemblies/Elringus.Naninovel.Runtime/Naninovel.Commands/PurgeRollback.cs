using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006FB")]
    public class PurgeRollback : Command
    {
    	[Token(Token = "0x600215C")]
    	[Address(RVA = "0x504A80", Offset = "0x503480", VA = "0x180504A80", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600215D")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public PurgeRollback()
    	{
    	}
    }
}
