using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x20006A6")]
public class ClearBacklog : Command
{
	[Token(Token = "0x600204B")]
	[Address(RVA = "0x4E7D70", Offset = "0x4E6770", VA = "0x1804E7D70", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600204C")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ClearBacklog()
	{
	}
}
