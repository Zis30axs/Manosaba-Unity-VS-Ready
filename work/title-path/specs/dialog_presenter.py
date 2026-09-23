FILE = "UnityProject/Assets/Scripts/Game/GigaCreation.Essentials.Dialogs/GigaCreation.Essentials.Dialogs.Presenters/DialogPresenter.cs"
USINGS = ["using System;", "using GigaCreation.Tools.Service;", "using UniRx;"]
EV = "work/title-path/disas/dp_"
BODIES = [
    dict(rva="0xCCE130", signature="private void Awake()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCCE130, 442 B / b__4_0 0xCCE360 -- _dialogManager (+0x38) = new",
                  "  DialogManager(_baseSortingOrder (+0x30)); its OnRequestToAddDialogAsObservable() (IDialogManager slot 0)",
                  "  .Subscribe(data => data.ProvidableDialog = Object.Instantiate(_providableDialogPrefab (+0x20), _dialogParent",
                  "  (+0x28))).AddTo(this); tail jump ServiceLocator.Register<IDialogManager>(_dialogManager)",
                  "Annotated disassembly: " + EV + "0xCCE130.txt, dp_0xCCE360.txt"],
         body=["_dialogManager = new DialogManager(_baseSortingOrder);",
               "_dialogManager.OnRequestToAddDialogAsObservable().Subscribe(delegate(AddDialogData data)", "{",
               "\tdata.ProvidableDialog = UnityEngine.Object.Instantiate(_providableDialogPrefab, _dialogParent);", "}).AddTo(this);",
               "ServiceLocator.Register(_dialogManager);"]),
    dict(rva="0xCCE2F0", signature="private void OnDestroy()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCCE2F0, 104 B -- tail jump ServiceLocator.Unregister<IDialogManager>(_dialogManager)",
                  "Annotated disassembly: " + EV + "0xCCE2F0.txt"],
         body=["ServiceLocator.Unregister(_dialogManager);"]),
]
