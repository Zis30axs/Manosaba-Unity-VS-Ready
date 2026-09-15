using System.Collections.Generic;
using GigaCreation.NaninovelExtender.ExtendedActors;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using System.Linq;

namespace WitchTrials.Views;

[Token(Token = "0x200002D")]
[UsedImplicitly]
public class EmoteNod : AnimateActorExtended
{
	[Token(Token = "0x400007A")]
	[FieldOffset(Offset = "0xC8")]
	private readonly float[] _scenePositionY = new float[2] { -5f, 0f };

	[Token(Token = "0x400007B")]
	[FieldOffset(Offset = "0xD0")]
	private readonly float[] _duration = new float[2] { 0.3f, 0.3f };

	[Token(Token = "0x400007C")]
	[FieldOffset(Offset = "0xD8")]
	private readonly EasingType[] _easing = new EasingType[2]
	{
		EasingType.EaseOutQuad,
		EasingType.EaseInQuad
	};

	[Token(Token = "0x400007D")]
	[FieldOffset(Offset = "0xE0")]
	[UsedImplicitly]
	[ParameterDefaultValue("1")]
	public DecimalParameter Speed = 1f;

	// RECONSTRUCTED | Confidence: A | RVA: 0x3167F0, 973 B; lambda <GetParametersForActor>b__4_0 0x316E30 (instance: reads Speed)
	// Each `string.Join<T>(char, IEnumerable<T>)` uses AnimateActor.KeyDelimiters[0] (static char[], element 0) and goes through the StringParameter implicit conversion into the AnimateActor field: ScenePositionX +0x58, ScenePositionY +0x60, Rotation +0x70, EasingTypeName +0x88, Duration +0x90; AnimateActorExtended.Relative (+0xB0) = true. The tail jump is AnimateActorExtended.GetParametersForActor (0xD1E940).
	// Durations are divided by the Speed parameter: `x / speed`: Command.Assigned(Speed) inlined (null test + ICommandParameter slot 0 `HasValue`), Naninovel Nullable<float>.Value, otherwise 1.0 (data RVA 0x2AE2820).
	[Token(Token = "0x60000A2")]
	[Address(RVA = "0x3167F0", Offset = "0x3151F0", VA = "0x1803167F0", Slot = "17")]
	protected override IReadOnlyList<string> GetParametersForActor(string actorId)
	{
		ScenePositionY = string.Join(KeyDelimiters[0], _scenePositionY);
		EasingTypeName = string.Join(KeyDelimiters[0], _easing);
		Duration = string.Join(KeyDelimiters[0], _duration.Select((float x) => x / (Assigned(Speed) ? Speed.Value : 1f)));
		Relative = true;
		return base.GetParametersForActor(actorId);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x316BC0 (field initializers only, then AnimateActorExtended..ctor)
	// All arrays are written element by element; note the easing is joined before the duration here.
	// Speed = 1f via DecimalParameter's implicit conversion.
	[Token(Token = "0x60000A3")]
	[Address(RVA = "0x316BC0", Offset = "0x3155C0", VA = "0x180316BC0")]
	public EmoteNod()
	{
	}
}
