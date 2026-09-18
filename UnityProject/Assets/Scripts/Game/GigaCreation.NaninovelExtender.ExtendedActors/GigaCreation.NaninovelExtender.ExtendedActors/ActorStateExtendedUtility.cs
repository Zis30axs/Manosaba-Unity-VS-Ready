using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000004")]
    public static class ActorStateExtendedUtility
    {
    	[Token(Token = "0x6000003")]
    	[Address(RVA = "0xD10C30", Offset = "0xD0F630", VA = "0x180D10C30")]
    	public static void OverwritePivotFromActor(IActor actor, ref Vector2 pivot)
    	{
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD10CA0", Offset = "0xD0F6A0", VA = "0x180D10CA0")]
    	public static void ApplyPivotToActor(IActor actor, Vector2 pivot)
    	{
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD10D10", Offset = "0xD0F710", VA = "0x180D10D10")]
    	public static void OverwriteMaskabilityFromActor(IActor actor, ref bool masked)
    	{
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0xD10D80", Offset = "0xD0F780", VA = "0x180D10D80")]
    	public static void ApplyMaskabilityToActor(IActor actor, bool masked)
    	{
    	}
    }
}
