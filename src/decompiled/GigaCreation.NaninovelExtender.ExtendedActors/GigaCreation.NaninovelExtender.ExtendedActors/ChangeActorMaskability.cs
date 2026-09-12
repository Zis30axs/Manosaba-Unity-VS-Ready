using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x2000035")]
[UsedImplicitly]
[CommandAlias("mask")]
public class ChangeActorMaskability : Command
{
	[Token(Token = "0x40000B2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[ParameterAlias("")]
	[RequiredParameter]
	[ActorContext("*", -1, null)]
	public StringListParameter ActorIds;

	[Token(Token = "0x40000B3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly]
	[ParameterDefaultValue("true")]
	public BooleanParameter Enable;

	[Token(Token = "0x60000B2")]
	[Address(RVA = "0xD1FA30", Offset = "0xD1E430", VA = "0x180D1FA30", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000B3")]
	[Address(RVA = "0x325690", Offset = "0x324090", VA = "0x180325690")]
	public ChangeActorMaskability()
	{
	}
}
