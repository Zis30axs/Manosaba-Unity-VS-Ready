using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000028")]
    public class BackgroundMetadata : OrthoActorMetadata
    {
    	[Serializable]
    	[Token(Token = "0x2000029")]
    	public class Map : ActorMetadataMap<BackgroundMetadata>
    	{
    		[Token(Token = "0x60000B1")]
    		[Address(RVA = "0x44CDA0", Offset = "0x44B7A0", VA = "0x18044CDA0")]
    		public Map()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x200002A")]
    	public class Pose : ActorPose<BackgroundState>
    	{
    		[Token(Token = "0x60000B2")]
    		[Address(RVA = "0x44CDE0", Offset = "0x44B7E0", VA = "0x18044CDE0")]
    		public Pose()
    		{
    		}
    	}

    	[Token(Token = "0x400009A")]
    	[FieldOffset(Offset = "0x70")]
    	[Tooltip("Controls the mode in which background actor is matched to the screen when aspect ratios are different:\n • Crop — Crop the background to ensure no `black bars` are visible.\n • Fit — Fit the background to the screen. The whole background will always be visible, but `black bars` will appear.\n • Custom — Match either width or height with a custom ratio. The ratio is controlled with `Custom Match Ratio` property.\n • Disable — Don't perform any matching.")]
    	public AspectMatchMode MatchMode;

    	[Token(Token = "0x400009B")]
    	[FieldOffset(Offset = "0x74")]
    	[Tooltip("When `Match Mode` is set to `Custom`, controls the match ratio. Minimum (0) value will match width and ignore height, maximum (1) — vice-versa.")]
    	[Range(0f, 1f)]
    	public float CustomMatchRatio;

    	[Token(Token = "0x400009C")]
    	[FieldOffset(Offset = "0x78")]
    	[Tooltip("Named states (poses) of the background; pose name can be used as appearance in `@back` commands to set enabled properties of the associated state.")]
    	public List<Pose> Poses;

    	[Token(Token = "0x400009D")]
    	[FieldOffset(Offset = "0x80")]
    	[Tooltip("Directory of the scene background assets under which Naninovel will attempt to resolve the appearances. Only applicable to scene backgrounds.")]
    	public string ScenePathRoot;

    	[Token(Token = "0x60000AF")]
    	[Address(RVA = "0x44CA90", Offset = "0x44B490", VA = "0x18044CA90")]
    	public BackgroundMetadata()
    	{
    	}

    	[Token(Token = "0x60000B0")]
    	public override ActorPose<TState> GetPose<TState>(string poseName)
    	{
    		return null;
    	}
    }
}
