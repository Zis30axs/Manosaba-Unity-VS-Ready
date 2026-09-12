using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x2000062")]
[UsedImplicitly]
[CommandAlias("timer")]
public class SetTrialTimer : Command
{
	[Token(Token = "0x4000146")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[ParameterAlias("")]
	[RequiredParameter]
	public DecimalParameter Time;

	[Token(Token = "0x600013E")]
	[Address(RVA = "0x325D70", Offset = "0x324770", VA = "0x180325D70", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600013F")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public SetTrialTimer()
	{
	}
}
