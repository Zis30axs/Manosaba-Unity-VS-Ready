using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x200005E")]
[UsedImplicitly]
[CommandAlias("pauseTrial")]
public class ChangeTrialPauseMode : Command
{
	[Token(Token = "0x400013B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[ParameterAlias("")]
	[RequiredParameter]
	public BooleanParameter Enable;

	[Token(Token = "0x400013C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly]
	[ParameterAlias("timer")]
	[ParameterDefaultValue("true")]
	public BooleanParameter ProcessTimer;

	[Token(Token = "0x6000135")]
	[Address(RVA = "0x325570", Offset = "0x323F70", VA = "0x180325570", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000136")]
	[Address(RVA = "0x325690", Offset = "0x324090", VA = "0x180325690")]
	public ChangeTrialPauseMode()
	{
	}
}
