using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine.Video;

namespace Naninovel
{
    [Token(Token = "0x20000D1")]
    [ActorResources(typeof(VideoClip), true)]
    public class VideoCharacter : VideoActor<CharacterMetadata>, ICharacterActor, IActor
    {
    	[Token(Token = "0x170000AC")]
    	protected override string MixerGroup
    	{
    		[Token(Token = "0x6000409")]
    		[Address(RVA = "0x476D50", Offset = "0x475750", VA = "0x180476D50", Slot = "67")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170000AD")]
    	public CharacterLookDirection LookDirection
    	{
    		[Token(Token = "0x600040A")]
    		[Address(RVA = "0x45BD50", Offset = "0x45A750", VA = "0x18045BD50", Slot = "77")]
    		get
    		{
    			return default(CharacterLookDirection);
    		}
    		[Token(Token = "0x600040B")]
    		[Address(RVA = "0x45BDD0", Offset = "0x45A7D0", VA = "0x18045BDD0", Slot = "78")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x600040C")]
    	[Address(RVA = "0x476DA0", Offset = "0x4757A0", VA = "0x180476DA0")]
    	public VideoCharacter(string id, CharacterMetadata meta, StandaloneAppearanceLoader<VideoClip> loader)
    	{
    	}

    	[Token(Token = "0x600040D")]
    	[Address(RVA = "0x45BED0", Offset = "0x45A8D0", VA = "0x18045BED0", Slot = "79")]
    	public UniTask ChangeLookDirection(CharacterLookDirection lookDirection, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}
    }
}
