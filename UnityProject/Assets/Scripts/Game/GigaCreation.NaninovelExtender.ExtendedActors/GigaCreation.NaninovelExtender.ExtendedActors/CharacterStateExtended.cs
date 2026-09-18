using System;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Serializable]
    [Token(Token = "0x2000018")]
    public class CharacterStateExtended : CharacterState
    {
    	[Token(Token = "0x4000055")]
    	[FieldOffset(Offset = "0x60")]
    	[SerializeField]
    	private Vector2 _pivot;

    	[Token(Token = "0x4000056")]
    	[FieldOffset(Offset = "0x68")]
    	[SerializeField]
    	private bool _masked;

    	[Token(Token = "0x600003F")]
    	[Address(RVA = "0xD18890", Offset = "0xD17290", VA = "0x180D18890", Slot = "4")]
    	public override void OverwriteFromActor(ICharacterActor actor)
    	{
    	}

    	[Token(Token = "0x6000040")]
    	[Address(RVA = "0xD189C0", Offset = "0xD173C0", VA = "0x180D189C0", Slot = "5")]
    	public override UniTask ApplyToActor(ICharacterActor actor)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000041")]
    	[Address(RVA = "0x453910", Offset = "0x452310", VA = "0x180453910")]
    	public CharacterStateExtended()
    	{
    	}
    }
}
