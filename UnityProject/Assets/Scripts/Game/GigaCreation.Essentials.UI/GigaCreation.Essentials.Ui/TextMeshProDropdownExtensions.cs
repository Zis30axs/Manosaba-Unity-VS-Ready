using UniRx;
using System;
using Il2CppDummyDll;
using TMPro;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000025")]
    public static class TextMeshProDropdownExtensions
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE1810 / <>c.b__0_0 0xCE1A40 -- the TMP twin of UniRx's
    	//   UnityUIComponentExtensions.OnValueChangedAsObservable(Dropdown): Observable.CreateWithState(dropdown, (d, observer)
    	//   => { observer.OnNext(d.value) (m_Value +0x130, IObserver slot 0); return d.onValueChanged.AsObservable()
    	//   .Subscribe(observer); })
    	// Annotated disassembly: work/title-path/disas/tmpdx_0xCE1810.txt, tmpdx_0xCE1A40.txt
    	[Token(Token = "0x600009C")]
    	[Address(RVA = "0xCE1810", Offset = "0xCE0210", VA = "0x180CE1810")]
    	public static IObservable<int> OnValueChangedAsObservable(this TMP_Dropdown dropdown)
    	{
    		return Observable.CreateWithState(dropdown, delegate(TMP_Dropdown d, IObserver<int> observer)
    		{
    			observer.OnNext(d.value);
    			return d.onValueChanged.AsObservable().Subscribe(observer);
    		});
    	}
    }
}
