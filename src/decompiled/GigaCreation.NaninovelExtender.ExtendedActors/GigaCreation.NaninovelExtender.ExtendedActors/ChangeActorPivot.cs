using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x2000037")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("pivot")]
public class ChangeActorPivot : Command
{
	[Token(Token = "0x40000B6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[RequiredParameter]
	[ActorContext("*", -1, null)]
	public StringListParameter ActorIds;

	[Token(Token = "0x40000B7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("point")]
	[RequiredParameter]
	public DecimalListParameter PivotPoint;

	[Token(Token = "0x60000B7")]
	[Address(RVA = "0xD1FF60", Offset = "0xD1E960", VA = "0x180D1FF60", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000B8")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ChangeActorPivot()
	{
	}
}
