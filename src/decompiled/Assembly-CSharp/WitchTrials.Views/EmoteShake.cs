using System.Collections.Generic;
using GigaCreation.NaninovelExtender.ExtendedActors;
using Il2CppDummyDll;
using JetBrains.Annotations;

namespace WitchTrials.Views;

[Token(Token = "0x200002E")]
[UsedImplicitly]
public class EmoteShake : AnimateActorExtended
{
	[Token(Token = "0x400007E")]
	[FieldOffset(Offset = "0xC8")]
	private readonly float[] _scenePositionX;

	[Token(Token = "0x400007F")]
	[FieldOffset(Offset = "0xD0")]
	private readonly float[] _duration;

	[Token(Token = "0x60000A5")]
	[Address(RVA = "0x316F00", Offset = "0x315900", VA = "0x180316F00", Slot = "17")]
	protected override IReadOnlyList<string> GetParametersForActor(string actorId)
	{
		return null;
	}

	[Token(Token = "0x60000A6")]
	[Address(RVA = "0x317180", Offset = "0x315B80", VA = "0x180317180")]
	public EmoteShake()
	{
	}
}
