using System;
using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x200051A")]
public interface IClickThroughPanel : IManagedUI
{
	[Token(Token = "0x6001963")]
	void Show(bool hideOnClick, Action onClick, params string[] allowedSamplers);

	[Token(Token = "0x6001964")]
	void Hide();
}
