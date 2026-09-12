using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x200052C")]
public interface IConfirmationUI : IManagedUI
{
	[Token(Token = "0x60019B8")]
	UniTask<bool> Confirm(string message);

	[Token(Token = "0x60019B9")]
	UniTask Notify(string message);
}
