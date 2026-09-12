using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x20005D6")]
public interface IToastUI : IManagedUI
{
	[Token(Token = "0x6001D23")]
	void Show(LocalizableText text, [Optional] string appearance, [Optional] float? duration);
}
