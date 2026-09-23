using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using GigaCreation.Tools.Service;
using System;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using MPUIKIT;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCEB550 -- lazy: _inputManager (+0x78), else ServiceLocator.Get<IInputManager>(true)
    		// Annotated disassembly: work/title-path/disas/gt_0xCEB550.txt
    		[Token(Token = "0x6000144")]
    		[Address(RVA = "0xCEB550", Offset = "0xCE9F50", VA = "0x180CEB550")]
    		get
    		{
    			if (_inputManager == null)
    			{
    				_inputManager = ServiceLocator.Get<IInputManager>(true);
    			}
    			return _inputManager;
    		}
    	}

    	[Token(Token = "0x1700003D")]
    	public bool IsOn
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCEB610 -- _toggle (+0x20).isOn = value (tail jump Toggle.Set(value, true))
    		// Annotated disassembly: work/title-path/disas/gt_0xCEB610.txt
    		[Token(Token = "0x6000145")]
    		[Address(RVA = "0xCEB610", Offset = "0xCEA010", VA = "0x180CEB610")]
    		set
    		{
    			_toggle.isOn = value;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEB640, 350 B / b__13_0 0xCEBB30, b__13_1 0xCEBD00 -- each AddTo(this):
    	//   _toggle.OnValueChangedAsObservable() -> TurnedOn() / TurnedOff() (both inlined); _toggle.OnPointerClickAsObservable()
    	//   -> InputManager.PlayResponseSfx(Submit)
    	// Annotated disassembly: work/title-path/disas/gt_0xCEB640.txt, gt_0xCEBB30.txt, gt_0xCEBD00.txt
    	[Token(Token = "0x6000146")]
    	[Address(RVA = "0xCEB640", Offset = "0xCEA040", VA = "0x180CEB640")]
    	private void Start()
    	{
    		_toggle.OnValueChangedAsObservable().Subscribe(delegate(bool x)
    		{
    			if (x)
    			{
    				TurnedOn();
    			}
    			else
    			{
    				TurnedOff();
    			}
    		}).AddTo(this);
    		_toggle.OnPointerClickAsObservable().Subscribe(delegate
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    		}).AddTo(this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEB7A0, 443 B -- _dropShadow (+0x28) enabled; _frame (+0x30).color = white (data
    	//   0x2AE36E0; Graphic slot 23), OutlineWidth = _outlineWidthWhenOn (+0x40), its GradientEffect copied, Enabled = true
    	//   and stored back (setter inlined: m_GradientEffect +0x2A0, SetMaterialDirty slot 29); _label.color = _labelColorWhenOn
    	// Annotated disassembly: work/title-path/disas/gt_0xCEB7A0.txt
    	[Token(Token = "0x6000147")]
    	[Address(RVA = "0xCEB7A0", Offset = "0xCEA1A0", VA = "0x180CEB7A0")]
    	private void TurnedOn()
    	{
    		_dropShadow.enabled = true;
    		_frame.color = Color.white;
    		_frame.OutlineWidth = _outlineWidthWhenOn;
    		GradientEffect gradientEffect = _frame.GradientEffect;
    		gradientEffect.Enabled = true;
    		_frame.GradientEffect = gradientEffect;
    		_label.color = _labelColorWhenOn;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEB960, 443 B -- TurnedOn with the drop shadow disabled, _colorWhenOff (+0x54),
    	//   outline width 0, the gradient disabled and _labelColorWhenOff (+0x64)
    	// Annotated disassembly: work/title-path/disas/gt_0xCEB960.txt
    	[Token(Token = "0x6000148")]
    	[Address(RVA = "0xCEB960", Offset = "0xCEA360", VA = "0x180CEB960")]
    	private void TurnedOff()
    	{
    		_dropShadow.enabled = false;
    		_frame.color = _colorWhenOff;
    		_frame.OutlineWidth = 0f;
    		GradientEffect gradientEffect = _frame.GradientEffect;
    		gradientEffect.Enabled = false;
    		_frame.GradientEffect = gradientEffect;
    		_label.color = _labelColorWhenOff;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEBB20 -- tail jump UniRx OnValueChangedAsObservable(_toggle)
    	// Annotated disassembly: work/title-path/disas/gt_0xCEBB20.txt
    	[Token(Token = "0x6000149")]
    	[Address(RVA = "0xCEBB20", Offset = "0xCEA520", VA = "0x180CEBB20")]
    	public IObservable<bool> OnValueChangedAsObservable()
    	{
    		return _toggle.OnValueChangedAsObservable();
    	}

    	[Token(Token = "0x600014A")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public GeneralToggle()
    	{
    	}
    }
}
