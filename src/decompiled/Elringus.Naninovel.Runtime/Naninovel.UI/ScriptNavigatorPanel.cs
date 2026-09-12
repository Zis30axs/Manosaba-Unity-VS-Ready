using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000587")]
public class ScriptNavigatorPanel : NavigatorPanel, IScriptNavigatorUI, IManagedUI
{
	[Token(Token = "0x6001B5E")]
	[Address(RVA = "0x63B290", Offset = "0x639C90", VA = "0x18063B290", Slot = "99")]
	protected override UniTask<IReadOnlyCollection<string>> LocateAllScriptPaths()
	{
		return default(UniTask<IReadOnlyCollection<string>>);
	}

	[Token(Token = "0x6001B5F")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public ScriptNavigatorPanel()
	{
	}
}
