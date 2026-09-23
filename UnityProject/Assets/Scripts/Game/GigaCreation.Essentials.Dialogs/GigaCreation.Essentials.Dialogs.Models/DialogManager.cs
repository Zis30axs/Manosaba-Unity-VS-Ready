using System.Linq;
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCCE440 -- new Subject<AddDialogData>() -> +0x10, new AddDialogData() -> +0x18,
    	//   new List<ProvidableDialog>(8) (List ctor inlined: _items = new T[8]) -> +0x20, baseSortingOrder -> +0x28
    	// Annotated disassembly: work/title-path/disas/dm_01_ctor.txt
    	[Token(Token = "0x6000046")]
    	[Address(RVA = "0xCCE440", Offset = "0xCCCE40", VA = "0x180CCE440")]
    	public DialogManager(int baseSortingOrder)
    	{
    		_addDialogSubject = new Subject<AddDialogData>();
    		_addDialogData = new AddDialogData();
    		_dialogs = new List<ProvidableDialog>(8);
    		_baseSortingOrder = baseSortingOrder;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCCE6C0, 2166 B -- _dialogs.GroupBy(<>c.b__5_0: InUse).ToDictionary(b__5_1: Key,
    	//   b__5_2: ToArray) held as IReadOnlyDictionary<bool, ProvidableDialog[]>; TryGetValue(false) (slot 1) -> First()
    	//   of the free dialogs; otherwise _addDialogData.Clear() (inlined: +0x10 = null), _addDialogSubject.OnNext(
    	//   _addDialogData) (IObserver slot 0) and _dialogs.Add(the ProvidableDialog it now holds) (ICollection slot 2);
    	//   the sorting order is one above Select(b__5_3: SortingOrder).Max() of the dialogs in use (TryGetValue(true)),
    	//   else _baseSortingOrder (+0x28); SortingOrder set (inlined onto the canvas), InUse = true (0x180CCEF1C)
    	// Annotated disassembly: work/title-path/disas/dm_02_GetOrAddDialog.txt
    	[Token(Token = "0x6000047")]
    	[Address(RVA = "0xCCE6C0", Offset = "0xCCD0C0", VA = "0x180CCE6C0", Slot = "5")]
    	public ProvidableDialog GetOrAddDialog()
    	{
    		IReadOnlyDictionary<bool, ProvidableDialog[]> readOnlyDictionary = _dialogs.GroupBy((ProvidableDialog dialog) => dialog.InUse)
    			.ToDictionary((IGrouping<bool, ProvidableDialog> group) => group.Key, (IGrouping<bool, ProvidableDialog> group) => group.ToArray());
    		ProvidableDialog providableDialog;
    		if (readOnlyDictionary.TryGetValue(false, out var value))
    		{
    			providableDialog = value.First();
    		}
    		else
    		{
    			_addDialogData.Clear();
    			_addDialogSubject.OnNext(_addDialogData);
    			_dialogs.Add(_addDialogData.ProvidableDialog);
    			providableDialog = _addDialogData.ProvidableDialog;
    		}
    		int sortingOrder = ((!readOnlyDictionary.TryGetValue(true, out var value2)) ? _baseSortingOrder : (value2.Select((ProvidableDialog dialog) => dialog.SortingOrder).Max() + 1));
    		providableDialog.SortingOrder = sortingOrder;
    		providableDialog.InUse = true;
    		return providableDialog;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (shared +0x10 getter) -- returns _addDialogSubject
    	[Token(Token = "0x6000048")]
    	[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
    	public IObservable<AddDialogData> OnRequestToAddDialogAsObservable()
    	{
    		return _addDialogSubject;
    	}
    }
}
