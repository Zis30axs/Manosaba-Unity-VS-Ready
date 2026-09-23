using UniRx;
using GigaCreation.Tools.Service;
using System;
using GigaCreation.Essentials.Dialogs.Models;
using GigaCreation.Essentials.Dialogs.Views;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Dialogs.Presenters
{
    [Token(Token = "0x2000014")]
    public class DialogPresenter : MonoBehaviour
    {
    	[Token(Token = "0x4000050")]
    	[FieldOffset(Offset = "0x20")]
    	[Header("Assets")]
    	[SerializeField]
    	private ProvidableDialog _providableDialogPrefab;

    	[Token(Token = "0x4000051")]
    	[FieldOffset(Offset = "0x28")]
    	[Header("References")]
    	[SerializeField]
    	private Transform _dialogParent;

    	[Token(Token = "0x4000052")]
    	[FieldOffset(Offset = "0x30")]
    	[Header("Parameters")]
    	[SerializeField]
    	private int _baseSortingOrder;

    	[Token(Token = "0x4000053")]
    	[FieldOffset(Offset = "0x38")]
    	private IDialogManager _dialogManager;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCCE130, 442 B / b__4_0 0xCCE360 -- _dialogManager (+0x38) = new
    	//   DialogManager(_baseSortingOrder (+0x30)); its OnRequestToAddDialogAsObservable() (IDialogManager slot 0)
    	//   .Subscribe(data => data.ProvidableDialog = Object.Instantiate(_providableDialogPrefab (+0x20), _dialogParent
    	//   (+0x28))).AddTo(this); tail jump ServiceLocator.Register<IDialogManager>(_dialogManager)
    	// Annotated disassembly: work/title-path/disas/dp_0xCCE130.txt, dp_0xCCE360.txt
    	[Token(Token = "0x6000040")]
    	[Address(RVA = "0xCCE130", Offset = "0xCCCB30", VA = "0x180CCE130")]
    	private void Awake()
    	{
    		_dialogManager = new DialogManager(_baseSortingOrder);
    		_dialogManager.OnRequestToAddDialogAsObservable().Subscribe(delegate(AddDialogData data)
    		{
    			data.ProvidableDialog = UnityEngine.Object.Instantiate(_providableDialogPrefab, _dialogParent);
    		}).AddTo(this);
    		ServiceLocator.Register(_dialogManager);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCCE2F0, 104 B -- tail jump ServiceLocator.Unregister<IDialogManager>(_dialogManager)
    	// Annotated disassembly: work/title-path/disas/dp_0xCCE2F0.txt
    	[Token(Token = "0x6000041")]
    	[Address(RVA = "0xCCE2F0", Offset = "0xCCCCF0", VA = "0x180CCE2F0")]
    	private void OnDestroy()
    	{
    		ServiceLocator.Unregister(_dialogManager);
    	}

    	[Token(Token = "0x6000042")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DialogPresenter()
    	{
    	}
    }
}
