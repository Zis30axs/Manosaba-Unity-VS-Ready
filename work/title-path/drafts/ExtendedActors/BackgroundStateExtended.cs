using System;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Serializable]
    [Token(Token = "0x200000C")]
    public class BackgroundStateExtended : BackgroundState
    {
    	[Token(Token = "0x4000028")]
    	[FieldOffset(Offset = "0x58")]
    	[SerializeField]
    	private Vector2 _pivot;

    	[Token(Token = "0x4000029")]
    	[FieldOffset(Offset = "0x60")]
    	[SerializeField]
    	private bool _masked;

    	[Token(Token = "0x600001D")]
    	[Address(RVA = "0xD146E0", Offset = "0xD130E0", VA = "0x180D146E0", Slot = "4")]
    	public override void OverwriteFromActor(IBackgroundActor actor)
    	{
    	}

    	[Token(Token = "0x600001E")]
    	[Address(RVA = "0xD147E0", Offset = "0xD131E0", VA = "0x180D147E0", Slot = "5")]
    	public override UniTask ApplyToActor(IBackgroundActor actor)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600001F")]
    	[Address(RVA = "0x44D0C0", Offset = "0x44BAC0", VA = "0x18044D0C0")]
    	public BackgroundStateExtended()
    	{
    	}
    }
}
