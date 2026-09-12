using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20000BA")]
[ActorResources(typeof(Texture2D), true)]
public class SpriteCharacter : SpriteActor<CharacterMetadata>, ICharacterActor, IActor
{
	[Token(Token = "0x17000093")]
	public CharacterLookDirection LookDirection
	{
		[Token(Token = "0x600037E")]
		[Address(RVA = "0x461B60", Offset = "0x460560", VA = "0x180461B60", Slot = "73")]
		get
		{
			return default(CharacterLookDirection);
		}
		[Token(Token = "0x600037F")]
		[Address(RVA = "0x461BE0", Offset = "0x4605E0", VA = "0x180461BE0", Slot = "74")]
		set
		{
		}
	}

	[Token(Token = "0x6000380")]
	[Address(RVA = "0x46D380", Offset = "0x46BD80", VA = "0x18046D380")]
	public SpriteCharacter(string id, CharacterMetadata meta, StandaloneAppearanceLoader<Texture2D> loader)
	{
	}

	[Token(Token = "0x6000381")]
	[Address(RVA = "0x461E00", Offset = "0x460800", VA = "0x180461E00", Slot = "75")]
	public UniTask ChangeLookDirection(CharacterLookDirection lookDirection, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}
}
