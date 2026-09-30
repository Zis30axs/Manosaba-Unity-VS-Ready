using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x200055A")]
    public interface ILocalizableUI
    {
    	[Token(Token = "0x6001A8D")]
    	UniTask HandleLocalizationChanged(LocaleChangedArgs args);
    }
}
