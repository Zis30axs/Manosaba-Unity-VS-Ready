using System;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Dialogs.Models;

[Token(Token = "0x2000018")]
public interface IDialogManager : IService, IDialogProvider
{
	[Token(Token = "0x600004F")]
	IObservable<AddDialogData> OnRequestToAddDialogAsObservable();
}
