using System.Collections.Generic;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Localization
{
    [Token(Token = "0x2000005")]
    public interface ILocalizationManagerExtended : Naninovel.ILocalizationManager, IEngineService<LocalizationConfiguration>, IEngineService
    {
    	[Token(Token = "0x17000002")]
    	IReadOnlyCollection<string> ValidLocales
    	{
    		[Token(Token = "0x6000006")]
    		get;
    	}

    	[Token(Token = "0x6000007")]
    	UniTask SelectLocale(LocaleKind locale);
    }
}
