FILE = "UnityProject/Assets/Scripts/Game/GigaCreation.Essentials.UI/GigaCreation.Essentials.Ui/TextMeshProDropdownExtensions.cs"
USINGS = ["using UniRx;"]
BODIES = [
    dict(rva="0xCE1810", signature="public static IObservable<int> OnValueChangedAsObservable(this TMP_Dropdown dropdown)",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCE1810 / <>c.b__0_0 0xCE1A40 -- the TMP twin of UniRx's",
                  "  UnityUIComponentExtensions.OnValueChangedAsObservable(Dropdown): Observable.CreateWithState(dropdown, (d, observer)",
                  "  => { observer.OnNext(d.value) (m_Value +0x130, IObserver slot 0); return d.onValueChanged.AsObservable()",
                  "  .Subscribe(observer); })",
                  "Annotated disassembly: work/title-path/disas/tmpdx_0xCE1810.txt, tmpdx_0xCE1A40.txt"],
         body=["return Observable.CreateWithState(dropdown, delegate(TMP_Dropdown d, IObserver<int> observer)", "{",
               "\tobserver.OnNext(d.value);", "\treturn d.onValueChanged.AsObservable().Subscribe(observer);", "});"]),
]
