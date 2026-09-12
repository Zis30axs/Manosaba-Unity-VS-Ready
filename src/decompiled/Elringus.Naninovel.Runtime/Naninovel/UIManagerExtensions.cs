using Il2CppDummyDll;
using Naninovel.UI;

namespace Naninovel;

[Token(Token = "0x20004D9")]
public static class UIManagerExtensions
{
	[Token(Token = "0x6001842")]
	public static bool TryGetUI<T>(this IUIManager manager, out T ui) where T : class, IManagedUI
	{
		ui = default;
		ui = default;
		return default(bool);
	}

	[Token(Token = "0x6001843")]
	[Address(RVA = "0x604A30", Offset = "0x603430", VA = "0x180604A30")]
	public static bool TryGetUI(this IUIManager manager, string name, out IManagedUI ui)
	{
		ui = default;
		ui = default;
		return default(bool);
	}

	[Token(Token = "0x6001844")]
	[Address(RVA = "0x604AF0", Offset = "0x6034F0", VA = "0x180604AF0")]
	public static bool FocusTop(this IUIManager manager)
	{
		return default(bool);
	}
}
