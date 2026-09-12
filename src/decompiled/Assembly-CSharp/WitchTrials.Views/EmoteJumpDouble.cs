using System.Collections.Generic;
using GigaCreation.NaninovelExtender.ExtendedActors;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x200002C")]
[UsedImplicitly]
public class EmoteJumpDouble : AnimateActorExtended
{
	[Token(Token = "0x4000076")]
	[FieldOffset(Offset = "0xC8")]
	private readonly float[] _scenePositionY;

	[Token(Token = "0x4000077")]
	[FieldOffset(Offset = "0xD0")]
	private readonly float[] _duration;

	[Token(Token = "0x4000078")]
	[FieldOffset(Offset = "0xD8")]
	private readonly EasingType[] _easing;

	[Token(Token = "0x4000079")]
	[FieldOffset(Offset = "0xE0")]
	[UsedImplicitly]
	[ParameterDefaultValue("1")]
	public DecimalParameter Speed;

	[Token(Token = "0x600009F")]
	[Address(RVA = "0x3160D0", Offset = "0x314AD0", VA = "0x1803160D0", Slot = "17")]
	protected override IReadOnlyList<string> GetParametersForActor(string actorId)
	{
		return null;
	}

	[Token(Token = "0x60000A0")]
	[Address(RVA = "0x3164A0", Offset = "0x314EA0", VA = "0x1803164A0")]
	public EmoteJumpDouble()
	{
	}
}
