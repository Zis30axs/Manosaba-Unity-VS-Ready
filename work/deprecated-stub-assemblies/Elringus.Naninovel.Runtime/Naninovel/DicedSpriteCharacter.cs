using System.Runtime.InteropServices;
using Il2CppDummyDll;
using SpriteDicing;

namespace Naninovel
{
    [Token(Token = "0x200007F")]
    [ActorResources(typeof(DicedSpriteAtlas), false)]
    public class DicedSpriteCharacter : DicedSpriteActor<CharacterMetadata>, ICharacterActor, IActor
    {
    	[Token(Token = "0x1700005F")]
    	public CharacterLookDirection LookDirection
    	{
    		[Token(Token = "0x6000235")]
    		[Address(RVA = "0x45BD50", Offset = "0x45A750", VA = "0x18045BD50", Slot = "72")]
    		get
    		{
    			return default(CharacterLookDirection);
    		}
    		[Token(Token = "0x6000236")]
    		[Address(RVA = "0x45BDD0", Offset = "0x45A7D0", VA = "0x18045BDD0", Slot = "73")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x6000237")]
    	[Address(RVA = "0x45BE50", Offset = "0x45A850", VA = "0x18045BE50")]
    	public DicedSpriteCharacter(string id = default, CharacterMetadata meta = default, EmbeddedAppearanceLoader<DicedSpriteAtlas> loader = default)
    	{
    	}

    	[Token(Token = "0x6000238")]
    	[Address(RVA = "0x45BED0", Offset = "0x45A8D0", VA = "0x18045BED0", Slot = "74")]
    	public UniTask ChangeLookDirection(CharacterLookDirection lookDirection, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}
    }
}
