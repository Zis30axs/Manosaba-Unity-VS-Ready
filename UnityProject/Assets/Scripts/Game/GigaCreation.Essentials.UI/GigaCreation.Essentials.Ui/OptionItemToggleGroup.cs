using System;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000046")]
    public class OptionItemToggleGroup : MonoBehaviour
    {
    	[Token(Token = "0x4000125")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private RebuildableToggle[] _toggles;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCEC940: new Subject<int>() -> +0x28
    	[Token(Token = "0x4000126")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly ISubject<int> _valueChangedSubject = new Subject<int>();

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCEC940: new CompositeDisposable() -> +0x30
    	[Token(Token = "0x4000127")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly CompositeDisposable _disposable = new CompositeDisposable();

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEC440, 624 B / <>c.b__3_0 0x39B030 (identity), b__3_1 0xCECAC0 -- for each
    	//   index i of _toggles (+0x20): toggle.OnValueChangedAsObservable() (inlined: its _uiToggle's UniRx
    	//   OnValueChangedAsObservable) .Where(turnedOn => turnedOn).SubscribeWithState(i, (_, index) =>
    	//   _valueChangedSubject (+0x28).OnNext(index)).AddTo(_disposable (+0x30), this)
    	// Annotated disassembly: work/title-path/disas/oitg_0xCEC440.txt, oitg_0x39B030.txt, oitg_0xCECAC0.txt
    	[Token(Token = "0x6000156")]
    	[Address(RVA = "0xCEC440", Offset = "0xCEAE40", VA = "0x180CEC440")]
    	private void OnEnable()
    	{
    		for (int i = 0; i < _toggles.Length; i++)
    		{
    			_toggles[i].OnValueChangedAsObservable().Where((bool turnedOn) => turnedOn).SubscribeWithState(i, delegate(bool _, int index)
    			{
    				_valueChangedSubject.OnNext(index);
    			}).AddTo(_disposable, this);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEAF00 (shared 30 B body) -- tail jump _disposable (+0x30).Clear()
    	// Annotated disassembly: work/title-path/disas/oitg_0xCEAF00.txt
    	[Token(Token = "0x6000157")]
    	[Address(RVA = "0xCEAF00", Offset = "0xCE9900", VA = "0x180CEAF00")]
    	private void OnDisable()
    	{
    		_disposable.Clear();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEC6C0 -- GetComponentsInChildren<RebuildableToggle>() -> _toggles (+0x20)
    	// Annotated disassembly: work/title-path/disas/oitg_0xCEC6C0.txt
    	[Token(Token = "0x6000158")]
    	[Address(RVA = "0xCEC6C0", Offset = "0xCEB0C0", VA = "0x180CEC6C0")]
    	private void Reset()
    	{
    		_toggles = GetComponentsInChildren<RebuildableToggle>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEC750, 373 B -- an index outside [0, Length) (js / jge at 0x180CEC7A0 /
    	//   0x180CEC7B2) only logs Debug.LogError(string.Format("The specified toggle index is out of range: {0}", index));
    	//   otherwise every toggle's IsOn (setter inlined: Toggle.Set(value, true)) = (i == index)
    	// Annotated disassembly: work/title-path/disas/oitg_0xCEC750.txt
    	[Token(Token = "0x6000159")]
    	[Address(RVA = "0xCEC750", Offset = "0xCEB150", VA = "0x180CEC750")]
    	public void SetValue(int index)
    	{
    		if (index < 0 || index >= _toggles.Length)
    		{
    			Debug.LogError(string.Format("The specified toggle index is out of range: {0}", index));
    			return;
    		}
    		for (int i = 0; i < _toggles.Length; i++)
    		{
    			_toggles[i].IsOn = i == index;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEC8D0 -- tail jump Observable.AsObservable(_valueChangedSubject (+0x28))
    	// Annotated disassembly: work/title-path/disas/oitg_0xCEC8D0.txt
    	[Token(Token = "0x600015A")]
    	[Address(RVA = "0xCEC8D0", Offset = "0xCEB2D0", VA = "0x180CEC8D0")]
    	public IObservable<int> OnValueChangedAsObservable()
    	{
    		return _valueChangedSubject.AsObservable();
    	}

    	[Token(Token = "0x600015B")]
    	[Address(RVA = "0xCEC940", Offset = "0xCEB340", VA = "0x180CEC940")]
    	public OptionItemToggleGroup()
    	{
    	}
    }
}
