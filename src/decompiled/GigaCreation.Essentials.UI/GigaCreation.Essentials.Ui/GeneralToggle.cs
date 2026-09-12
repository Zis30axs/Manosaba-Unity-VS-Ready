using System;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using MPUIKIT;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui;

[Token(Token = "0x2000043")]
public class GeneralToggle : MonoBehaviour
{
	[Token(Token = "0x4000117")]
	[FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private Toggle _toggle;

	[Token(Token = "0x4000118")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private MPImage _dropShadow;

	[Token(Token = "0x4000119")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private MPImage _frame;

	[Token(Token = "0x400011A")]
	[FieldOffset(Offset = "0x38")]
	[SerializeField]
	private TextMeshProUGUI _label;

	[Token(Token = "0x400011B")]
	[FieldOffset(Offset = "0x40")]
	[Header("Parameters")]
	[SerializeField]
	private float _outlineWidthWhenOn;

	[Token(Token = "0x400011C")]
	[FieldOffset(Offset = "0x44")]
	[SerializeField]
	private Color _labelColorWhenOn;

	[Token(Token = "0x400011D")]
	[FieldOffset(Offset = "0x54")]
	[Space]
	[SerializeField]
	private Color _colorWhenOff;

	[Token(Token = "0x400011E")]
	[FieldOffset(Offset = "0x64")]
	[SerializeField]
	private Color _labelColorWhenOff;

	[Token(Token = "0x400011F")]
	[FieldOffset(Offset = "0x78")]
	private IInputManager _inputManager;

	[Token(Token = "0x1700003C")]
	private IInputManager InputManager
	{
		[Token(Token = "0x6000144")]
		[Address(RVA = "0xCEB550", Offset = "0xCE9F50", VA = "0x180CEB550")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700003D")]
	public bool IsOn
	{
		[Token(Token = "0x6000145")]
		[Address(RVA = "0xCEB610", Offset = "0xCEA010", VA = "0x180CEB610")]
		set
		{
		}
	}

	[Token(Token = "0x6000146")]
	[Address(RVA = "0xCEB640", Offset = "0xCEA040", VA = "0x180CEB640")]
	private void Start()
	{
	}

	[Token(Token = "0x6000147")]
	[Address(RVA = "0xCEB7A0", Offset = "0xCEA1A0", VA = "0x180CEB7A0")]
	private void TurnedOn()
	{
	}

	[Token(Token = "0x6000148")]
	[Address(RVA = "0xCEB960", Offset = "0xCEA360", VA = "0x180CEB960")]
	private void TurnedOff()
	{
	}

	[Token(Token = "0x6000149")]
	[Address(RVA = "0xCEBB20", Offset = "0xCEA520", VA = "0x180CEBB20")]
	public IObservable<bool> OnValueChangedAsObservable()
	{
		return null;
	}

	[Token(Token = "0x600014A")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public GeneralToggle()
	{
	}
}
