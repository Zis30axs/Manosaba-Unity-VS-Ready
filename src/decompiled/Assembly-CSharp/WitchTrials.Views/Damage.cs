using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x200005F")]
[UsedImplicitly]
public class Damage : Command
{
	[Token(Token = "0x6000137")]
	[Address(RVA = "0x325700", Offset = "0x324100", VA = "0x180325700", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000138")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public Damage()
	{
	}
}
