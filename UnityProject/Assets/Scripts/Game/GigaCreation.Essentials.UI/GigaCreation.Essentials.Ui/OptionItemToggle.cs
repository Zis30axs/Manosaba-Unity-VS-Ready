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

    	[Token(Token = "0x4000121")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly ISubject<int> _onValueChangedSubject;

    	[Token(Token = "0x4000122")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly CompositeDisposable _disposables;

    	[Token(Token = "0x600014D")]
    	[Address(RVA = "0xCEBE00", Offset = "0xCEA800", VA = "0x180CEBE00")]
    	private void OnEnable()
    	{
    	}

    	[Token(Token = "0x600014E")]
    	[Address(RVA = "0xCEAF00", Offset = "0xCE9900", VA = "0x180CEAF00")]
    	private void OnDisable()
    	{
    	}

    	[Token(Token = "0x600014F")]
    	[Address(RVA = "0xCEC0C0", Offset = "0xCEAAC0", VA = "0x180CEC0C0")]
    	public void SetValue(int index)
    	{
    	}

    	[Token(Token = "0x6000150")]
    	[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
    	public IObservable<int> OnValueChangedAsObservable()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000151")]
    	[Address(RVA = "0xCEC1C0", Offset = "0xCEABC0", VA = "0x180CEC1C0")]
    	public OptionItemToggle()
    	{
    	}
    }
}
