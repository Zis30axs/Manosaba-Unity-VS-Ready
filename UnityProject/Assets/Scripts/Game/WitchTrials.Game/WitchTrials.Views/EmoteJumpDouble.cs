using System.Collections.Generic;
using GigaCreation.NaninovelExtender.ExtendedActors;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using System.Linq;

namespace WitchTrials.Views
{
    [Token(Token = "0x200002C")]
    [UsedImplicitly]
    public class EmoteJumpDouble : AnimateActorExtended
    {
    	[Token(Token = "0x4000076")]
    	[FieldOffset(Offset = "0xC8")]
    	private readonly float[] _scenePositionY = new float[5] { 6f, -2f, 6f, -2f, 0f };

    	[Token(Token = "0x4000077")]
    	[FieldOffset(Offset = "0xD0")]
    	private readonly float[] _duration = new float[5] { 0.15f, 0.15f, 0.15f, 0.15f, 0.1f };

    	[Token(Token = "0x4000078")]
    	[FieldOffset(Offset = "0xD8")]
    	private readonly EasingType[] _easing = new EasingType[5]
    	{
    		EasingType.EaseOutQuad,
    		EasingType.EaseInQuad,
    		EasingType.EaseOutQuad,
    		EasingType.EaseInQuad,
    		EasingType.EaseOutQuad
    	};

    	[Token(Token = "0x4000079")]
    	[FieldOffset(Offset = "0xE0")]
    	[UsedImplicitly]
    	[ParameterDefaultValue("1")]
    	public DecimalParameter Speed = 1f;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3160D0, 973 B; lambda <GetParametersForActor>b__4_0 0x316720 (instance: reads Speed)
    	// Each `string.Join<T>(char, IEnumerable<T>)` uses AnimateActor.KeyDelimiters[0] (static char[], element 0) and goes through the StringParameter implicit conversion into the AnimateActor field: ScenePositionX +0x58, ScenePositionY +0x60, Rotation +0x70, EasingTypeName +0x88, Duration +0x90; AnimateActorExtended.Relative (+0xB0) = true. The tail jump is AnimateActorExtended.GetParametersForActor (0xD1E940).
    	// Durations are divided by the Speed parameter: `x / speed`: Command.Assigned(Speed) inlined (null test + ICommandParameter slot 0 `HasValue`), Naninovel Nullable<float>.Value, otherwise 1.0 (data RVA 0x2AE2820).
    	[Token(Token = "0x600009F")]
    	[Address(RVA = "0x3160D0", Offset = "0x314AD0", VA = "0x1803160D0", Slot = "17")]
    	protected override IReadOnlyList<string> GetParametersForActor(string actorId)
    	{
    		ScenePositionY = string.Join(KeyDelimiters[0], _scenePositionY);
    		Duration = string.Join(KeyDelimiters[0], _duration.Select((float x) => x / (Assigned(Speed) ? Speed.Value : 1f)));
    		EasingTypeName = string.Join(KeyDelimiters[0], _easing);
    		Relative = true;
    		return base.GetParametersForActor(actorId);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3164A0 (field initializers only, then AnimateActorExtended..ctor)
    	// All three arrays come from InitializeArray blobs (global-metadata.dat v31): Y 0x854A38, duration 0x854A68, easing 0x85B190.
    	// Speed = 1f via DecimalParameter's implicit conversion.
    	[Token(Token = "0x60000A0")]
    	[Address(RVA = "0x3164A0", Offset = "0x314EA0", VA = "0x1803164A0")]
    	public EmoteJumpDouble()
    	{
    	}
    }
}
