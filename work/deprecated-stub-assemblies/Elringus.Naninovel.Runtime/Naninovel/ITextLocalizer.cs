using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200037E")]
    public interface ITextLocalizer : IEngineService
    {
    	[Token(Token = "0x60010EC")]
    	UniTask Load(LocalizableText text, [Optional] object holder);

    	[Token(Token = "0x60010ED")]
    	void Hold(LocalizableText text, object holder);

    	[Token(Token = "0x60010EE")]
    	void Release(LocalizableText text, object holder);

    	[Token(Token = "0x60010EF")]
    	string Resolve(LocalizableText text);
    }
}
