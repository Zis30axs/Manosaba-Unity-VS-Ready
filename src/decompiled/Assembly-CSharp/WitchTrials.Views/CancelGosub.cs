using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x2000034")]
[UsedImplicitly]
public class CancelGosub : Command
{
	[Token(Token = "0x60000B7")]
	[Address(RVA = "0x319490", Offset = "0x317E90", VA = "0x180319490", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000B8")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public CancelGosub()
	{
	}
}
