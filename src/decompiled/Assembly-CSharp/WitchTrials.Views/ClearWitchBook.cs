using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x2000068")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("clearBook")]
public class ClearWitchBook : Command
{
	[Token(Token = "0x400015E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	public StringListParameter CategoryNames;

	[Token(Token = "0x1700001D")]
	private bool IsAct2Chapter1Adv
	{
		[Token(Token = "0x600014D")]
		[Address(RVA = "0x327E70", Offset = "0x326870", VA = "0x180327E70")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x600014E")]
	[Address(RVA = "0x327FD0", Offset = "0x3269D0", VA = "0x180327FD0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600014F")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ClearWitchBook()
	{
	}
}
