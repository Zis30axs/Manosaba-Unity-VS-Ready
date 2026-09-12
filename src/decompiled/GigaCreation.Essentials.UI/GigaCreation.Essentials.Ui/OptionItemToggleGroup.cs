using System;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;

namespace GigaCreation.Essentials.Ui;

[Token(Token = "0x2000046")]
public class OptionItemToggleGroup : MonoBehaviour
{
	[Token(Token = "0x4000125")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private RebuildableToggle[] _toggles;

	[Token(Token = "0x4000126")]
	[FieldOffset(Offset = "0x28")]
	private readonly ISubject<int> _valueChangedSubject;

	[Token(Token = "0x4000127")]
	[FieldOffset(Offset = "0x30")]
	private readonly CompositeDisposable _disposable;

	[Token(Token = "0x6000156")]
	[Address(RVA = "0xCEC440", Offset = "0xCEAE40", VA = "0x180CEC440")]
	private void OnEnable()
	{
	}

	[Token(Token = "0x6000157")]
	[Address(RVA = "0xCEAF00", Offset = "0xCE9900", VA = "0x180CEAF00")]
	private void OnDisable()
	{
	}

	[Token(Token = "0x6000158")]
	[Address(RVA = "0xCEC6C0", Offset = "0xCEB0C0", VA = "0x180CEC6C0")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000159")]
	[Address(RVA = "0xCEC750", Offset = "0xCEB150", VA = "0x180CEC750")]
	public void SetValue(int index)
	{
	}

	[Token(Token = "0x600015A")]
	[Address(RVA = "0xCEC8D0", Offset = "0xCEB2D0", VA = "0x180CEC8D0")]
	public IObservable<int> OnValueChangedAsObservable()
	{
		return null;
	}

	[Token(Token = "0x600015B")]
	[Address(RVA = "0xCEC940", Offset = "0xCEB340", VA = "0x180CEC940")]
	public OptionItemToggleGroup()
	{
	}
}
