using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000541")]
public class ExternalScriptsBrowserPanel : NavigatorPanel, IExternalScriptsUI, IManagedUI
{
	[Token(Token = "0x6001A13")]
	[Address(RVA = "0x625640", Offset = "0x624040", VA = "0x180625640", Slot = "99")]
	protected override UniTask<IReadOnlyCollection<string>> LocateAllScriptPaths()
	{
		return default(UniTask<IReadOnlyCollection<string>>);
	}

	[Token(Token = "0x6001A14")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public ExternalScriptsBrowserPanel()
	{
	}
}
