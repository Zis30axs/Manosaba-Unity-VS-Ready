using System.Collections.Generic;
using GigaCreation.NaninovelExtender.ExtendedActors;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x200002B")]
[UsedImplicitly]
public class EmoteJump : AnimateActorExtended
{
	[Token(Token = "0x4000072")]
	[FieldOffset(Offset = "0xC8")]
	private readonly float[] _scenePositionY;

	[Token(Token = "0x4000073")]
	[FieldOffset(Offset = "0xD0")]
	private readonly float[] _duration;

	[Token(Token = "0x4000074")]
	[FieldOffset(Offset = "0xD8")]
	private readonly EasingType[] _easing;

	[Token(Token = "0x4000075")]
	[FieldOffset(Offset = "0xE0")]
	[UsedImplicitly]
	[ParameterDefaultValue("1")]
	public DecimalParameter Speed;

	[Token(Token = "0x600009C")]
	[Address(RVA = "0x3159B0", Offset = "0x3143B0", VA = "0x1803159B0", Slot = "17")]
	protected override IReadOnlyList<string> GetParametersForActor(string actorId)
	{
		return null;
	}

	[Token(Token = "0x600009D")]
	[Address(RVA = "0x315D80", Offset = "0x314780", VA = "0x180315D80")]
	public EmoteJump()
	{
	}
}
