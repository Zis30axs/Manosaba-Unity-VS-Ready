using System.Collections.Generic;
using GigaCreation.NaninovelExtender.ExtendedActors;
using Il2CppDummyDll;
using JetBrains.Annotations;

namespace WitchTrials.Views
{
    [Token(Token = "0x200002E")]
    [UsedImplicitly]
    public class EmoteShake : AnimateActorExtended
    {
    	[Token(Token = "0x400007E")]
    	[FieldOffset(Offset = "0xC8")]
    	private readonly float[] _scenePositionX = new float[7] { -0.6f, 0.6f, -0.6f, 0.6f, -0.4f, 0.4f, 0f };

    	[Token(Token = "0x400007F")]
    	[FieldOffset(Offset = "0xD0")]
    	private readonly float[] _duration = new float[7] { 0.05f, 0.05f, 0.05f, 0.05f, 0.05f, 0.05f, 0.05f };

    	// RECONSTRUCTED | Confidence: A | RVA: 0x316F00, 631 B
    	// Each `string.Join<T>(char, IEnumerable<T>)` uses AnimateActor.KeyDelimiters[0] (static char[], element 0) and goes through the StringParameter implicit conversion into the AnimateActor field: ScenePositionX +0x58, ScenePositionY +0x60, Rotation +0x70, EasingTypeName +0x88, Duration +0x90; AnimateActorExtended.Relative (+0xB0) = true. The tail jump is AnimateActorExtended.GetParametersForActor (0xD1E940).
    	[Token(Token = "0x60000A5")]
    	[Address(RVA = "0x316F00", Offset = "0x315900", VA = "0x180316F00", Slot = "17")]
    	protected override IReadOnlyList<string> GetParametersForActor(string actorId)
    	{
    		ScenePositionX = string.Join(KeyDelimiters[0], _scenePositionX);
    		Duration = string.Join(KeyDelimiters[0], _duration);
    		Relative = true;
    		return base.GetParametersForActor(actorId);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x317180, 370 B (field initializers only, then AnimateActorExtended..ctor)
    	// InitializeArray blobs (global-metadata.dat v31): X 0x8549C0, duration 0x854A00.
    	[Token(Token = "0x60000A6")]
    	[Address(RVA = "0x317180", Offset = "0x315B80", VA = "0x180317180")]
    	public EmoteShake()
    	{
    	}
    }
}
