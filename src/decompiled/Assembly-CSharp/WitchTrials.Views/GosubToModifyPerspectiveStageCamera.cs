using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x2000046")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("gosubModifyCamera")]
public class GosubToModifyPerspectiveStageCamera : Command
{
	[Token(Token = "0x40000CB")]
	private const string SubroutineScriptPath = "System/System_Subroutine";

	[Token(Token = "0x40000CC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[RequiredParameter]
	public IntegerParameter Index;

	[Token(Token = "0x40000CD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("look")]
	[RequiredParameter]
	public StringParameter LookCharacterId;

	[Token(Token = "0x40000CE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("comp")]
	public StringParameter Composition;

	[Token(Token = "0x40000CF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("rollDir")]
	public StringParameter RollDirection;

	[Token(Token = "0x40000D0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("zoomLvl")]
	public IntegerParameter ZoomLevel;

	[Token(Token = "0x40000D1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("params")]
	public StringListParameter LabelNameParams;

	[Token(Token = "0x60000E8")]
	[Address(RVA = "0x31D870", Offset = "0x31C270", VA = "0x18031D870", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000E9")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public GosubToModifyPerspectiveStageCamera()
	{
	}
}
