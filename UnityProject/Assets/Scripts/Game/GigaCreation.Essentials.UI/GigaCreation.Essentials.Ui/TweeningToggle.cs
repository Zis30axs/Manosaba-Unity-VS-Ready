using System;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000050")]
    public class TweeningToggle : Toggle
    {
    	[Token(Token = "0x400014B")]
    	[FieldOffset(Offset = "0x128")]
    	[Header("References")]
    	[SerializeField]
    	private Transform _transform;

    	[Token(Token = "0x400014C")]
    	[FieldOffset(Offset = "0x130")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x400014D")]
    	[FieldOffset(Offset = "0x138")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _scaleUpWhenHighlighted;

    	[Token(Token = "0x400014E")]
    	[FieldOffset(Offset = "0x140")]
    	[SerializeField]
    	private ScaleUtility _scaleUtil;

    	[Token(Token = "0x400014F")]
    	[FieldOffset(Offset = "0x148")]
    	[SerializeField]
    	private bool _fadeOutWhenOff;

    	[Token(Token = "0x4000150")]
    	[FieldOffset(Offset = "0x149")]
    	[SerializeField]
    	private bool _fadeOutWhenDisabled;

    	[Token(Token = "0x4000151")]
    	[FieldOffset(Offset = "0x150")]
    	[SerializeField]
    	private FadeUtility _fadeUtil;

    	[Token(Token = "0x4000152")]
    	[FieldOffset(Offset = "0x158")]
    	[Space]
    	[SerializeField]
    	private bool _disableRaycastBlockingWhenOff;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCF1290: new CompositeDisposable() -> +0x160
    	[Token(Token = "0x4000153")]
    	[FieldOffset(Offset = "0x160")]
    	private readonly CompositeDisposable _disposables = new CompositeDisposable();

    	[Token(Token = "0x4000154")]
    	[FieldOffset(Offset = "0x168")]
    	private SelectableUtility _selectableUtil;

    	[Token(Token = "0x17000045")]
    	private SelectableUtility SelectableUtil
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF0BF0 -- lazy: [this+0x168], else new SelectableUtility(gameObject)
    		// Annotated disassembly: work/title-path/disas/tt_01_0xCF0BF0.txt
    		[Token(Token = "0x600019D")]
    		[Address(RVA = "0xCF0BF0", Offset = "0xCEF5F0", VA = "0x180CF0BF0")]
    		get
    		{
    			if (_selectableUtil == null)
    			{
    				_selectableUtil = new SelectableUtility(base.gameObject);
    			}
    			return _selectableUtil;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF0CC0 -- Toggle.OnEnable inlined (Selectable.OnEnable, SetToggleGroup(m_Group,
    	//   false), PlayEffect(true)), then when SelectableUtil.IsActive a tail call to Initialize
    	// Annotated disassembly: work/title-path/disas/tt_02_0xCF0CC0.txt
    	[Token(Token = "0x600019E")]
    	[Address(RVA = "0xCF0CC0", Offset = "0xCEF6C0", VA = "0x180CF0CC0", Slot = "5")]
    	protected override void OnEnable()
    	{
    		base.OnEnable();
    		if (SelectableUtil.IsActive)
    		{
    			Initialize();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF0D30 -- Toggle.OnDisable inlined (SetToggleGroup(null, false),
    	//   Selectable.OnDisable), then _disposables (+0x160) Clear
    	// Annotated disassembly: work/title-path/disas/tt_03_0xCF0D30.txt
    	[Token(Token = "0x600019F")]
    	[Address(RVA = "0xCF0D30", Offset = "0xCEF730", VA = "0x180CF0D30", Slot = "7")]
    	protected override void OnDisable()
    	{
    		base.OnDisable();
    		_disposables.Clear();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF0D70, 541 B -- SelectableUtility's highlighting (+0x10) and enabling (+0x18)
    	//   observables and UnityUIComponentExtensions.OnValueChangedAsObservable(this) subscribed with OnHighlightingChanged /
    	//   OnEnablingChanged / OnValueChanged, each AddTo(_disposables +0x160, this); then onValueChanged (+0x118) is invoked
    	//   with m_IsOn (+0x120) -- behind the binary's own null test of the event (0x180CF0F57: test rcx,rcx / je ret).
    	// Annotated disassembly: work/title-path/disas/tt_04_0xCF0D70.txt
    	[Token(Token = "0x60001A0")]
    	[Address(RVA = "0xCF0D70", Offset = "0xCEF770", VA = "0x180CF0D70")]
    	private void Initialize()
    	{
    		SelectableUtil.OnHighlightingChangedAsObservable().Subscribe(OnHighlightingChanged).AddTo(_disposables, this);
    		SelectableUtil.OnEnablingChangedAsObservable().Subscribe(OnEnablingChanged).AddTo(_disposables, this);
    		this.OnValueChangedAsObservable().Subscribe(OnValueChanged).AddTo(_disposables, this);
    		// 0x180CF0F57: the shipped code tests onValueChanged for null before invoking it
    		if (onValueChanged != null)
    		{
    			onValueChanged.Invoke(isOn);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF0F90 -- _scaleUpWhenHighlighted (+0x138) -> ScaleUtility.Scale(_transform,
    	//   isHighlighted); _fadeOutWhenOff (+0x148) and !m_IsOn (+0x120) -> FadeUtility.Fade(_canvasGroup, isHighlighted)
    	// Annotated disassembly: work/title-path/disas/tt_05_0xCF0F90.txt
    	[Token(Token = "0x60001A1")]
    	[Address(RVA = "0xCF0F90", Offset = "0xCEF990", VA = "0x180CF0F90")]
    	private void OnHighlightingChanged(bool isHighlighted)
    	{
    		if (_scaleUpWhenHighlighted)
    		{
    			_scaleUtil.Scale(_transform, isHighlighted);
    		}
    		if (_fadeOutWhenOff && !isOn)
    		{
    			_fadeUtil.Fade(_canvasGroup, isHighlighted);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF1010 -- only with _fadeOutWhenDisabled (+0x149): fade to
    	//   isEnabled ? (_fadeOutWhenOff (+0x148) ? m_IsOn : true) : false (tail call FadeUtility.Fade)
    	// Annotated disassembly: work/title-path/disas/tt_06_0xCF1010.txt
    	[Token(Token = "0x60001A2")]
    	[Address(RVA = "0xCF1010", Offset = "0xCEFA10", VA = "0x180CF1010")]
    	private void OnEnablingChanged(bool isEnabled)
    	{
    		if (_fadeOutWhenDisabled)
    		{
    			_fadeUtil.Fade(_canvasGroup, isEnabled && (!_fadeOutWhenOff || isOn));
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF1070 -- _fadeOutWhenOff (+0x148) -> Fade(_canvasGroup, turnedOn);
    	//   _disableRaycastBlockingWhenOff (+0x158) -> _canvasGroup.blocksRaycasts = !turnedOn (xor dil, 1 at 0x180CF10BD)
    	// Annotated disassembly: work/title-path/disas/tt_07_0xCF1070.txt
    	[Token(Token = "0x60001A3")]
    	[Address(RVA = "0xCF1070", Offset = "0xCEFA70", VA = "0x180CF1070")]
    	private void OnValueChanged(bool turnedOn)
    	{
    		if (_fadeOutWhenOff)
    		{
    			_fadeUtil.Fade(_canvasGroup, turnedOn);
    		}
    		if (_disableRaycastBlockingWhenOff)
    		{
    			_canvasGroup.blocksRaycasts = !turnedOn;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF10E0, 385 B -- as ButtonExtendedBase.DoStateTransition: nothing while
    	//   !IsActive; states 0..4 to the matching SelectableUtility transition; anything else
    	//   ArgumentOutOfRangeException("state", boxed state, null).  Selectable's own transition is not called.
    	// Annotated disassembly: work/title-path/disas/tt_08_0xCF10E0.txt
    	[Token(Token = "0x60001A4")]
    	[Address(RVA = "0xCF10E0", Offset = "0xCEFAE0", VA = "0x180CF10E0", Slot = "26")]
    	protected override void DoStateTransition(SelectionState state, bool instant)
    	{
    		if (!SelectableUtil.IsActive)
    		{
    			return;
    		}
    		switch (state)
    		{
    		case SelectionState.Normal:
    			SelectableUtil.OnStateTransitionToNormal();
    			break;
    		case SelectionState.Highlighted:
    			SelectableUtil.OnStateTransitionToHighlighted();
    			break;
    		case SelectionState.Pressed:
    			SelectableUtil.OnStateTransitionToPressed();
    			break;
    		case SelectionState.Selected:
    			SelectableUtil.OnStateTransitionToSelected();
    			break;
    		case SelectionState.Disabled:
    			SelectableUtil.OnStateTransitionToDisabled();
    			break;
    		default:
    			throw new ArgumentOutOfRangeException("state", state, null);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF1270, 19 B -- _fadeOutWhenOff (+0x148) || _fadeOutWhenDisabled (+0x149)
    	// Annotated disassembly: work/title-path/disas/tt_09_0xCF1270.txt
    	[Token(Token = "0x60001A5")]
    	[Address(RVA = "0xCF1270", Offset = "0xCEFC70", VA = "0x180CF1270")]
    	private bool IsFadeEnabled()
    	{
    		return _fadeOutWhenOff || _fadeOutWhenDisabled;
    	}

    	[Token(Token = "0x60001A6")]
    	[Address(RVA = "0xCF1290", Offset = "0xCEFC90", VA = "0x180CF1290")]
    	public TweeningToggle()
    	{
    	}
    }
}
