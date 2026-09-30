using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000044")]
    public interface ICharacterActor : IActor
    {
    	[Token(Token = "0x1700001D")]
    	CharacterLookDirection LookDirection
    	{
    		[Token(Token = "0x60000FE")]
    		get;
    		[Token(Token = "0x60000FF")]
    		set;
    	}

    	[Token(Token = "0x6000100")]
    	UniTask ChangeLookDirection(CharacterLookDirection lookDirection, Tween tween, [Optional] AsyncToken token);
    }
}
