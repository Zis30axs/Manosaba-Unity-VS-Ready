using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x200037D")]
public interface ILocalizationManager : IEngineService<LocalizationConfiguration>, IEngineService
{
	[Token(Token = "0x17000248")]
	string SelectedLocale
	{
		[Token(Token = "0x60010E5")]
		get;
	}

	[Token(Token = "0x17000249")]
	IReadOnlyCollection<string> AvailableLocales
	{
		[Token(Token = "0x60010E6")]
		get;
	}

	[Token(Token = "0x1700024A")]
	IReadOnlyList<IResourceProvider> Providers
	{
		[Token(Token = "0x60010E7")]
		get;
	}

	[Token(Token = "0x1400005B")]
	event Action<LocaleChangedArgs> OnLocaleChanged;

	[Token(Token = "0x60010E8")]
	bool LocaleAvailable(string locale);

	[Token(Token = "0x60010E9")]
	UniTask SelectLocale(string locale);

	[Token(Token = "0x60010EA")]
	void AddChangeLocaleTask(Func<LocaleChangedArgs, UniTask> handler, int priority = 0);

	[Token(Token = "0x60010EB")]
	void RemoveChangeLocaleTask(Func<LocaleChangedArgs, UniTask> handler);
}
