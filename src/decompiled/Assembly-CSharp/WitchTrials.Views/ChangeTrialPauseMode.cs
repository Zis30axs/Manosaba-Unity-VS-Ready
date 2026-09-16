using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Models;

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

	// RECONSTRUCTED | Confidence: A | RVA: 0x325570, 273 B
	// ServiceLocator.Get<IWitchTrialsNaninovelManager>(cl = 0); Enable (+0x30) and ProcessTimer (+0x38) through the
	// Nullable<bool> implicit operator; slot 7 ChangeTrialPauseMode(enable, processTimer) via sub_3820. Returns default.
	[Token(Token = "0x6000135")]
	[Address(RVA = "0x325570", Offset = "0x323F70", VA = "0x180325570", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		IWitchTrialsNaninovelManager manager = GigaCreation.Tools.Service.ServiceLocator.Get<IWitchTrialsNaninovelManager>(false);
		manager.ChangeTrialPauseMode(Enable, ProcessTimer);
		return UniTask.CompletedTask;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x325690, 112 B
	// Folded with ArrangeCharacters / ClearChoiceHandler / ChangeActorMaskability ctors: BooleanParameter.op_Implicit(true)
	// stored at +0x38 (here `ProcessTimer`), then Command..ctor.
	[Token(Token = "0x6000136")]
	[Address(RVA = "0x325690", Offset = "0x324090", VA = "0x180325690")]
	public ChangeTrialPauseMode()
	{
		ProcessTimer = true;
	}
}
