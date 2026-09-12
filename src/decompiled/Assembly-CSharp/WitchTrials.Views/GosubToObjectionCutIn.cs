using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x2000060")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("gosubCutIn")]
public class GosubToObjectionCutIn : Command
{
	[Token(Token = "0x400013D")]
	private const string SubroutineScriptPath = "System/System_Subroutine";

	[Token(Token = "0x400013E")]
	private const string ShowObjectionCutInLabelName = "EndDebate-CutIn";

	[Token(Token = "0x400013F")]
	private const string ObjectionSpawnPathVarName = "objectionCutInSpawnPath";

	[Token(Token = "0x4000140")]
	private const string ObjectionIndexVarName = "objectionCutInIndex";

	[Token(Token = "0x4000141")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[RequiredParameter]
	public StringParameter CutInKind;

	[Token(Token = "0x4000142")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[RequiredParameter]
	public IntegerParameter Index;

	[Token(Token = "0x6000139")]
	[Address(RVA = "0x325750", Offset = "0x324150", VA = "0x180325750", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600013A")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public GosubToObjectionCutIn()
	{
	}
}
