using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x2000047")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("gosubSplitStageScreen")]
public class GosubToSplitPerspectiveStageScreens : Command
{
	[Token(Token = "0x40000D2")]
	private const string SubroutineScriptPath = "System/System_Subroutine";

	[Token(Token = "0x40000D3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[RequiredParameter]
	public IntegerParameter Index;

	[Token(Token = "0x40000D4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("look")]
	[RequiredParameter]
	public StringParameter LookCharacterId;

	[Token(Token = "0x40000D5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("comp")]
	public StringParameter Composition;

	[Token(Token = "0x40000D6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("rollDir")]
	public StringParameter RollDirection;

	[Token(Token = "0x40000D7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("params")]
	public StringListParameter LabelNameParams;

	[Token(Token = "0x60000EA")]
	[Address(RVA = "0x31E250", Offset = "0x31CC50", VA = "0x18031E250", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000EB")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public GosubToSplitPerspectiveStageScreens()
	{
	}
}
