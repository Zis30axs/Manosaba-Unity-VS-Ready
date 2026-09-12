using System.Collections.Generic;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Localization;

[Token(Token = "0x2000004")]
public interface IDefaultLocaleSelector : IService
{
	[Token(Token = "0x17000001")]
	LocaleKind DefaultLocale
	{
		[Token(Token = "0x6000003")]
		get;
	}

	[Token(Token = "0x17000002")]
	IList<string> AvailableTextLanguages
	{
		[Token(Token = "0x6000004")]
		get;
	}
}
