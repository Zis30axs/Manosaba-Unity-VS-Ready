using System.Collections.Generic;
using GigaCreation.NaninovelExtender.ExtendedActors;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x200002D")]
[UsedImplicitly]
public class EmoteNod : AnimateActorExtended
{
	[Token(Token = "0x400007A")]
	[FieldOffset(Offset = "0xC8")]
	private readonly float[] _scenePositionY;

	[Token(Token = "0x400007B")]
	[FieldOffset(Offset = "0xD0")]
	private readonly float[] _duration;

	[Token(Token = "0x400007C")]
	[FieldOffset(Offset = "0xD8")]
	private readonly EasingType[] _easing;

	[Token(Token = "0x400007D")]
	[FieldOffset(Offset = "0xE0")]
	[UsedImplicitly]
	[ParameterDefaultValue("1")]
	public DecimalParameter Speed;

	[Token(Token = "0x60000A2")]
	[Address(RVA = "0x3167F0", Offset = "0x3151F0", VA = "0x1803167F0", Slot = "17")]
	protected override IReadOnlyList<string> GetParametersForActor(string actorId)
	{
		return null;
	}

	[Token(Token = "0x60000A3")]
	[Address(RVA = "0x316BC0", Offset = "0x3155C0", VA = "0x180316BC0")]
	public EmoteNod()
	{
	}
}
