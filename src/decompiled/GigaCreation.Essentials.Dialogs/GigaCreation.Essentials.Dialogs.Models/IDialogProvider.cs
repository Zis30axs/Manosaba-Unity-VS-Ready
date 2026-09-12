using GigaCreation.Essentials.Dialogs.Views;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Dialogs.Models;

[Token(Token = "0x2000019")]
public interface IDialogProvider
{
	[Token(Token = "0x6000050")]
	ProvidableDialog GetOrAddDialog();
}
