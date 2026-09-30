using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000043")]
    public class CharacterState : ActorState<ICharacterActor>
    {
    	[Token(Token = "0x40000EE")]
    	[FieldOffset(Offset = "0x58")]
    	[SerializeField]
    	private CharacterLookDirection lookDirection;

    	[Token(Token = "0x1700001C")]
    	public CharacterLookDirection LookDirection
    	{
    		[Token(Token = "0x60000FA")]
    		[Address(RVA = "0x4537D0", Offset = "0x4521D0", VA = "0x1804537D0")]
    		get
    		{
    			return default(CharacterLookDirection);
    		}
    	}

    	[Token(Token = "0x60000FB")]
    	[Address(RVA = "0x4537E0", Offset = "0x4521E0", VA = "0x1804537E0", Slot = "4")]
    	public override void OverwriteFromActor(ICharacterActor actor)
    	{
    	}

    	[Token(Token = "0x60000FC")]
    	[Address(RVA = "0x453860", Offset = "0x452260", VA = "0x180453860", Slot = "5")]
    	public override UniTask ApplyToActor(ICharacterActor actor)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60000FD")]
    	[Address(RVA = "0x453910", Offset = "0x452310", VA = "0x180453910")]
    	public CharacterState()
    	{
    	}
    }
}
