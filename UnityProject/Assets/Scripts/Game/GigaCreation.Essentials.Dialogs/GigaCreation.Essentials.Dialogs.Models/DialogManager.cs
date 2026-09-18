using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Dialogs.Views;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UniRx;

namespace GigaCreation.Essentials.Dialogs.Models
{
    [Token(Token = "0x2000016")]
    public class DialogManager : IDialogManager, IService, IDialogProvider
    {
    	[Token(Token = "0x4000055")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly ISubject<AddDialogData> _addDialogSubject;

    	[Token(Token = "0x4000056")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly AddDialogData _addDialogData;

    	[Token(Token = "0x4000057")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly IList<ProvidableDialog> _dialogs;

    	[Token(Token = "0x4000058")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly int _baseSortingOrder;

    	[Token(Token = "0x6000046")]
    	[Address(RVA = "0xCCE440", Offset = "0xCCCE40", VA = "0x180CCE440")]
    	public DialogManager(int baseSortingOrder)
    	{
    	}

    	[Token(Token = "0x6000047")]
    	[Address(RVA = "0xCCE6C0", Offset = "0xCCD0C0", VA = "0x180CCE6C0", Slot = "5")]
    	public ProvidableDialog GetOrAddDialog()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000048")]
    	[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
    	public IObservable<AddDialogData> OnRequestToAddDialogAsObservable()
    	{
    		return null;
    	}
    }
}
