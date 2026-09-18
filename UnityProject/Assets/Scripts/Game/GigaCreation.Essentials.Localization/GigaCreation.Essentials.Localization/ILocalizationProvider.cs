using System;
using Il2CppDummyDll;
using TMPro;

namespace GigaCreation.Essentials.Localization
{
    [Token(Token = "0x2000007")]
    public interface ILocalizationProvider
    {
    	[Token(Token = "0x17000003")]
    	LocaleKind SourceLocale
    	{
    		[Token(Token = "0x6000008")]
    		get;
    	}

    	[Token(Token = "0x17000004")]
    	LocaleKind CurrentLocale
    	{
    		[Token(Token = "0x6000009")]
    		get;
    	}

    	[Token(Token = "0x17000005")]
    	TMP_FontAsset CurrentFontAsset
    	{
    		[Token(Token = "0x600000A")]
    		get;
    	}

    	[Token(Token = "0x600000B")]
    	IObservable<LocaleKind> OnLocaleChangedAsObservable();
    }
}
