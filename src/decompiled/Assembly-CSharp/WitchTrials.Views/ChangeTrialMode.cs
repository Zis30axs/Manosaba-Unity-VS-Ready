using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x200005D")]
[UsedImplicitly]
[CommandAlias("trialMode")]
public class ChangeTrialMode : Command
{
	[Token(Token = "0x400013A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[ParameterAlias("")]
	[RequiredParameter]
	public BooleanParameter Enable;

	[Token(Token = "0x6000133")]
	[Address(RVA = "0x325460", Offset = "0x323E60", VA = "0x180325460", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000134")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ChangeTrialMode()
	{
	}
}
