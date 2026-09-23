using System;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x200004E")]
    public class SlideToggle : Toggle
    {
    	[Token(Token = "0x4000144")]
    	[FieldOffset(Offset = "0x128")]
    	[Header("References")]
    	[SerializeField]
    	private RectTransform _rectTransform;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCF0AA0: new SlideUtility() (its ctor inlined: _tweenDuration 0.1) -> +0x130
    	[Token(Token = "0x4000145")]
    	[FieldOffset(Offset = "0x130")]
    	[Header("Parameters")]
    	[SerializeField]
    	private SlideUtility _slideUtil = new SlideUtility();

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCF0AA0: new CompositeDisposable() -> +0x138
    	[Token(Token = "0x4000146")]
    	[FieldOffset(Offset = "0x138")]
    	private readonly CompositeDisposable _disposables = new CompositeDisposable();

    	[Token(Token = "0x4000147")]
    	[FieldOffset(Offset = "0x140")]
    	private SelectableUtility _selectableUtil;

    	[Token(Token = "0x17000040")]
    	private SelectableUtility SelectableUtil
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCEFFE0 -- lazy: _selectableUtil (+0x140), else new SelectableUtility(gameObject)
    		// Annotated disassembly: work/title-path/disas/st_01_0xCEFFE0.txt
    		[Token(Token = "0x600018D")]
    		[Address(RVA = "0xCEFFE0", Offset = "0xCEE9E0", VA = "0x180CEFFE0")]
    		get
    		{
    			if (_selectableUtil == null)
    			{
    				_selectableUtil = new SelectableUtility(base.gameObject);
    			}
    			return _selectableUtil;
    		}
    	}

    	[Token(Token = "0x17000041")]
    	protected bool IsActiveSelectableUtil
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF00B0 -- the field itself (+0x140, not the lazy getter): tail jump
    		//   SelectableUtility.get_IsActive
    		// Annotated disassembly: work/title-path/disas/st_02_0xCF00B0.txt
    		[Token(Token = "0x600018E")]
    		[Address(RVA = "0xCF00B0", Offset = "0xCEEAB0", VA = "0x180CF00B0")]
    		get
    		{
    			return _selectableUtil.IsActive;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF00E0, 84 B -- Selectable.Awake, then _slideUtil (+0x130).Initialize(
    	//   _rectTransform (+0x128)) (inlined: its _defaultAnchorPositionX = anchoredPosition.x)
    	// Annotated disassembly: work/title-path/disas/st_03_0xCF00E0.txt
    	[Token(Token = "0x600018F")]
    	[Address(RVA = "0xCF00E0", Offset = "0xCEEAE0", VA = "0x180CF00E0", Slot = "4")]
    	protected override void Awake()
    	{
    		base.Awake();
    		_slideUtil.Initialize(_rectTransform);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF0140, 290 B -- Toggle.OnEnable inlined (Selectable.OnEnable,
    	//   SetToggleGroup(m_Group, false), PlayEffect(true)); then, when SelectableUtil.IsActive, Initialize() (inlined)
    	// Annotated disassembly: work/title-path/disas/st_04_0xCF0140.txt
    	[Token(Token = "0x6000190")]
    	[Address(RVA = "0xCF0140", Offset = "0xCEEB40", VA = "0x180CF0140", Slot = "5")]
    	protected override void OnEnable()
    	{
    		base.OnEnable();
    		if (SelectableUtil.IsActive)
    		{
    			Initialize();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF0350, 62 B -- Toggle.OnDisable inlined (SetToggleGroup(null, false),
    	//   Selectable.OnDisable), then tail jump _disposables (+0x138).Clear()
    	// Annotated disassembly: work/title-path/disas/st_05_0xCF0350.txt
    	[Token(Token = "0x6000191")]
    	[Address(RVA = "0xCF0350", Offset = "0xCEED50", VA = "0x180CF0350", Slot = "7")]
    	protected override void OnDisable()
    	{
    		base.OnDisable();
    		_disposables.Clear();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF0390, 430 B -- _slideUtil.Initialize(_rectTransform) (inlined);
    	//   OnHighlightingChangedAsObservable() (inlined: SelectableUtil +0x10) Subscribe(OnHighlightingChanged) and
    	//   OnValueChangedAsObservable() (inlined: onValueChanged (+0x118).AsObservable()) Subscribe(OnValueChanged), each
    	//   AddTo(_disposables, this)
    	// Annotated disassembly: work/title-path/disas/st_06_0xCF0390.txt
    	[Token(Token = "0x6000192")]
    	[Address(RVA = "0xCF0390", Offset = "0xCEED90", VA = "0x180CF0390")]
    	private void Initialize()
    	{
    		_slideUtil.Initialize(_rectTransform);
    		OnHighlightingChangedAsObservable().Subscribe(OnHighlightingChanged).AddTo(_disposables, this);
    		OnValueChangedAsObservable().Subscribe(OnValueChanged).AddTo(_disposables, this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF0550, 769 B -- nothing while m_IsOn (+0x120); highlighted ->
    	//   _slideUtil.MotorSlide(_rectTransform), otherwise _slideUtil.ForceSlide(_rectTransform, true) (both inlined)
    	// Annotated disassembly: work/title-path/disas/st_07_0xCF0550.txt
    	[Token(Token = "0x6000193")]
    	[Address(RVA = "0xCF0550", Offset = "0xCEEF50", VA = "0x180CF0550")]
    	private void OnHighlightingChanged(bool isHighlighted)
    	{
    		if (!base.isOn)
    		{
    			if (isHighlighted)
    			{
    				_slideUtil.MotorSlide(_rectTransform);
    			}
    			else
    			{
    				_slideUtil.ForceSlide(_rectTransform, true);
    			}
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF0860, 50 B -- tail jump _slideUtil.ForceSlide(_rectTransform, !value)
    	//   (xor dl, 1)
    	// Annotated disassembly: work/title-path/disas/st_08_0xCF0860.txt
    	[Token(Token = "0x6000194")]
    	[Address(RVA = "0xCF0860", Offset = "0xCEF260", VA = "0x180CF0860")]
    	private void OnValueChanged(bool value)
    	{
    		_slideUtil.ForceSlide(_rectTransform, !value);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF08A0, 400 B -- as TweeningToggle.DoStateTransition: nothing while
    	//   !SelectableUtil.IsActive; states 0..4 to the matching SelectableUtility transition; anything else
    	//   ArgumentOutOfRangeException("state", boxed state, null)
    	// Annotated disassembly: work/title-path/disas/st_09_0xCF08A0.txt
    	[Token(Token = "0x6000195")]
    	[Address(RVA = "0xCF08A0", Offset = "0xCEF2A0", VA = "0x180CF08A0", Slot = "26")]
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF0A30, 66 B -- tail jump UnityEventExtensions.AsObservable<bool>(
    	//   onValueChanged (+0x118))
    	// Annotated disassembly: work/title-path/disas/st_10_0xCF0A30.txt
    	[Token(Token = "0x6000196")]
    	[Address(RVA = "0xCF0A30", Offset = "0xCEF430", VA = "0x180CF0A30")]
    	protected IObservable<bool> OnValueChangedAsObservable()
    	{
    		return onValueChanged.AsObservable();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF0A80, 30 B -- SelectableUtil.OnHighlightingChangedAsObservable() (inlined:
    	//   its +0x10)
    	// Annotated disassembly: work/title-path/disas/st_11_0xCF0A80.txt
    	[Token(Token = "0x6000197")]
    	[Address(RVA = "0xCF0A80", Offset = "0xCEF480", VA = "0x180CF0A80")]
    	protected IObservable<bool> OnHighlightingChangedAsObservable()
    	{
    		return SelectableUtil.OnHighlightingChangedAsObservable();
    	}

    	[Token(Token = "0x6000198")]
    	[Address(RVA = "0xCF0AA0", Offset = "0xCEF4A0", VA = "0x180CF0AA0")]
    	public SlideToggle()
    	{
    	}
    }
}
