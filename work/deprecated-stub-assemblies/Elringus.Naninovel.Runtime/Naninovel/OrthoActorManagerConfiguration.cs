using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000063")]
    public abstract class OrthoActorManagerConfiguration<TMeta> : ActorManagerConfiguration<TMeta> where TMeta : ActorMetadata
    {
    	[Token(Token = "0x400014C")]
    	[FieldOffset(Offset = "0x0")]
    	[Tooltip("Reference point on scene to be considered as origin for the managed actors. Doesn't affect positioning.")]
    	public Vector2 SceneOrigin;

    	[Token(Token = "0x400014D")]
    	[FieldOffset(Offset = "0x0")]
    	[Tooltip("Initial Z-axis offset (depth) from actors to the camera to set when the actors are created.")]
    	public float ZOffset;

    	[Token(Token = "0x400014E")]
    	[FieldOffset(Offset = "0x0")]
    	[Tooltip("Distance by Z-axis to set between the actors when they are created; used to prevent z-fighting issues.")]
    	public float ZStep;

    	[Token(Token = "0x60001AB")]
    	protected OrthoActorManagerConfiguration()
    	{
    	}
    }
}
