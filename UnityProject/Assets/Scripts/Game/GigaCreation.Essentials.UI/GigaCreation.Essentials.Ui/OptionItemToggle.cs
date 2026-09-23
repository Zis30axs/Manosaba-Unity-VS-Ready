using System;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000044")]
    public class OptionItemToggle : MonoBehaviour
    {
    	[Token(Token = "0x4000120")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private GeneralToggle[] _toggles;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCEC1C0: new Subject<int>() -> +0x28
    	[Token(Token = "0x4000121")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly ISubject<int> _onValueChangedSubject = new Subject<int>();

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCEC1C0: new CompositeDisposable() -> +0x30
    	[Token(Token = "0x4000122")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly CompositeDisposable _disposables = new CompositeDisposable();

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEBE00, 693 B / <>c.b__3_0 0x39B030 (identity), b__3_1 0xCEC340 -- for each
    	//   index i: _toggles[i].OnValueChangedAsObservable() (inlined) .Skip(1).Where(turnedOn => turnedOn)
    	//   .SubscribeWithState(i, (_, index) => _onValueChangedSubject (+0x28).OnNext(index)).AddTo(_disposables, this)
    	// Annotated disassembly: work/title-path/disas/oit_0xCEBE00.txt, oit_0xCEC340.txt
    	[Token(Token = "0x600014D")]
    	[Address(RVA = "0xCEBE00", Offset = "0xCEA800", VA = "0x180CEBE00")]
    	private void OnEnable()
    	{
    		for (int i = 0; i < _toggles.Length; i++)
    		{
    			_toggles[i].OnValueChangedAsObservable().Skip(1).Where((bool turnedOn) => turnedOn).SubscribeWithState(i, delegate(bool _, int index)
    			{
    				_onValueChangedSubject.OnNext(index);
    			}).AddTo(_disposables, this);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEAF00 (shared 30 B body) -- tail jump _disposables (+0x30).Clear()
    	[Token(Token = "0x600014E")]
    	[Address(RVA = "0xCEAF00", Offset = "0xCE9900", VA = "0x180CEAF00")]
    	private void OnDisable()
    	{
    		_disposables.Clear();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEC0C0, 250 B -- an index outside [0, Length) only logs
    	//   Debug.LogError("指定されたトグルのインデックスが不正です。"); otherwise every toggle's IsOn (inlined) = (i == index)
    	// Annotated disassembly: work/title-path/disas/oit_0xCEC0C0.txt
    	[Token(Token = "0x600014F")]
    	[Address(RVA = "0xCEC0C0", Offset = "0xCEAAC0", VA = "0x180CEC0C0")]
    	public void SetValue(int index)
    	{
    		if (index < 0 || index >= _toggles.Length)
    		{
    			Debug.LogError("指定されたトグルのインデックスが不正です。");
    			return;
    		}
    		for (int i = 0; i < _toggles.Length; i++)
    		{
    			_toggles[i].IsOn = i == index;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38F060 (shared +0x28 getter) -- returns _onValueChangedSubject
    	[Token(Token = "0x6000150")]
    	[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
    	public IObservable<int> OnValueChangedAsObservable()
    	{
    		return _onValueChangedSubject;
    	}

    	[Token(Token = "0x6000151")]
    	[Address(RVA = "0xCEC1C0", Offset = "0xCEABC0", VA = "0x180CEC1C0")]
    	public OptionItemToggle()
    	{
    	}
    }
}
