using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x200069F")]
[CommandAlias("save")]
public class AutoSave : Command
{
	[Token(Token = "0x6002034")]
	[Address(RVA = "0x4E4390", Offset = "0x4E2D90", VA = "0x1804E4390", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002035")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public AutoSave()
	{
	}
}
