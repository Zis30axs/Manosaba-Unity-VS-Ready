using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x20006D1")]
public class Stop : Command
{
	[Token(Token = "0x60020C9")]
	[Address(RVA = "0x4F4C50", Offset = "0x4F3650", VA = "0x1804F4C50", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020CA")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public Stop()
	{
	}
}
