using UniRx.Triggers;
using UnityEngine.EventSystems;
using GigaCreation.Tools.Service;
using System.Globalization;
using System;
using System.Runtime.CompilerServices;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000042")]
    public class LabeledSlider : MonoBehaviour
    {
    	[Token(Token = "0x4000112")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Slider _slider;

    	[Token(Token = "0x4000113")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private TextMeshProUGUI _label;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCEB270: new CompositeDisposable() -> +0x30
    	[Token(Token = "0x4000114")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly CompositeDisposable _disposables = new CompositeDisposable();

    	[Token(Token = "0x4000115")]
    	[FieldOffset(Offset = "0x38")]
    	private IInputManager _inputManager;

    	[Token(Token = "0x17000039")]
    	private Func<float, string> BuildLabelTextFunc
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCEAC70 -- OverriddenBuildLabelTextFunc (+0x40) when set (0x180CEACB2), else a new
    		//   Func<float, string>(BuildValueText)
    		// Annotated disassembly: work/title-path/disas/ls_0xCEAC70.txt
    		[Token(Token = "0x6000135")]
    		[Address(RVA = "0xCEAC70", Offset = "0xCE9670", VA = "0x180CEAC70")]
    		get
    		{
    			// 0x180CEACB2: the shipped code prefers the overriding function when one is set
    			if (OverriddenBuildLabelTextFunc != null)
    			{
    				return OverriddenBuildLabelTextFunc;
    			}
    			return BuildValueText;
    		}
    	}

    	[Token(Token = "0x1700003A")]
    	public Func<float, string> OverriddenBuildLabelTextFunc
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x44CE10 (shared +0x40 getter) / 0x5405E0 (set): auto-property accessors over
    		//   <OverriddenBuildLabelTextFunc>k__BackingField (+0x40)
    		[Token(Token = "0x6000136")]
    		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10")]
    		[CompilerGenerated]
    		private get;
    		[Token(Token = "0x6000137")]
    		[Address(RVA = "0x5405E0", Offset = "0x53EFE0", VA = "0x1805405E0")]
    		[CompilerGenerated]
    		set;
    	}

    	[Token(Token = "0x1700003B")]
    	private IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCEACF0 -- lazy: _inputManager (+0x38), else ServiceLocator.Get<IInputManager>(true)
    		// Annotated disassembly: work/title-path/disas/ls_0xCEACF0.txt
    		[Token(Token = "0x6000138")]
    		[Address(RVA = "0xCEACF0", Offset = "0xCE96F0", VA = "0x180CEACF0")]
    		get
    		{
    			if (_inputManager == null)
    			{
    				_inputManager = ServiceLocator.Get<IInputManager>(true);
    			}
    			return _inputManager;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEADB0, 334 B / b__12_0 0xCEB350, b__12_1 0xCEB420 -- each AddTo(_disposables
    	//   (+0x30), this): _slider (+0x20).OnValueChangedAsObservable() (UniRx; not Skip(1)) -> _label (+0x28)
    	//   .SetText(BuildLabelTextFunc(value)) (getter inlined); OnConfirmAsObservable() -> InputManager.PlayResponseSfx(Submit)
    	// Annotated disassembly: work/title-path/disas/ls_0xCEADB0.txt, ls_0xCEB350.txt, ls_0xCEB420.txt
    	[Token(Token = "0x6000139")]
    	[Address(RVA = "0xCEADB0", Offset = "0xCE97B0", VA = "0x180CEADB0")]
    	private void OnEnable()
    	{
    		_slider.OnValueChangedAsObservable().Subscribe(delegate(float value)
    		{
    			_label.SetText(BuildLabelTextFunc(value));
    		}).AddTo(_disposables, this);
    		OnConfirmAsObservable().Subscribe(delegate
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    		}).AddTo(_disposables, this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEAF00 (shared 30 B body) -- tail jump _disposables (+0x30).Clear()
    	// Annotated disassembly: work/title-path/disas/ls_0xCEAF00.txt
    	[Token(Token = "0x600013A")]
    	[Address(RVA = "0xCEAF00", Offset = "0xCE9900", VA = "0x180CEAF00")]
    	public void OnDisable()
    	{
    		_disposables.Clear();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEAF20, 278 B -- with _slider.wholeNumbers (+0x11C) the value is
    	//   Mathf.RoundToInt-ed (0x355C90); then value.ToString(CultureInfo.InvariantCulture) (static +0x0
    	//   invariant_culture_info; tail jump Number.FormatSingle(value, null, NumberFormatInfo.GetInstance))
    	// Annotated disassembly: work/title-path/disas/ls_0xCEAF20.txt
    	[Token(Token = "0x600013B")]
    	[Address(RVA = "0xCEAF20", Offset = "0xCE9920", VA = "0x180CEAF20")]
    	private string BuildValueText(float value)
    	{
    		if (_slider.wholeNumbers)
    		{
    			value = Mathf.RoundToInt(value);
    		}
    		return value.ToString(CultureInfo.InvariantCulture);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEB040 -- _slider.maxValue = value (setter inlined: SetStruct(m_MaxValue),
    	//   Set(m_Value), UpdateVisuals)
    	// Annotated disassembly: work/title-path/disas/ls_0xCEB040.txt
    	[Token(Token = "0x600013C")]
    	[Address(RVA = "0xCEB040", Offset = "0xCE9A40", VA = "0x180CEB040")]
    	public void SetMaxValue(float value)
    	{
    		_slider.maxValue = value;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEB0E0 -- _slider.value = value (Slider slot 47 set_value, vtable +0x428)
    	// Annotated disassembly: work/title-path/disas/ls_0xCEB0E0.txt
    	[Token(Token = "0x600013D")]
    	[Address(RVA = "0xCEB0E0", Offset = "0xCE9AE0", VA = "0x180CEB0E0")]
    	public void SetValue(int value)
    	{
    		_slider.value = value;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEB120 -- _slider.OnValueChangedAsObservable().Skip(1) (edx = 1)
    	// Annotated disassembly: work/title-path/disas/ls_0xCEB120.txt
    	[Token(Token = "0x600013E")]
    	[Address(RVA = "0xCEB120", Offset = "0xCE9B20", VA = "0x180CEB120")]
    	public IObservable<float> OnSlideAsObservable()
    	{
    		return _slider.OnValueChangedAsObservable().Skip(1);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEB1A0 / b__18_0 0xCEB520 -- _slider.OnPointerUpAsObservable().Select(_ =>
    	//   _slider.value)
    	// Annotated disassembly: work/title-path/disas/ls_0xCEB1A0.txt, ls_0xCEB520.txt
    	[Token(Token = "0x600013F")]
    	[Address(RVA = "0xCEB1A0", Offset = "0xCE9BA0", VA = "0x180CEB1A0")]
    	public IObservable<float> OnConfirmAsObservable()
    	{
    		return _slider.OnPointerUpAsObservable().Select((PointerEventData _) => _slider.value);
    	}

    	[Token(Token = "0x6000140")]
    	[Address(RVA = "0xCEB270", Offset = "0xCE9C70", VA = "0x180CEB270")]
    	public LabeledSlider()
    	{
    	}
    }
}
