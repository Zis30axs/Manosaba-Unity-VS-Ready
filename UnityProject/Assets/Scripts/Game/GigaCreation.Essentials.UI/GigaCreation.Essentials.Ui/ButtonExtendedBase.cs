using System;
using Il2CppDummyDll;
using UniRx;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000006")]
    public abstract class ButtonExtendedBase : Button
    {
    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCD6AA0: new CompositeDisposable() -> +0x108
    	[Token(Token = "0x400000A")]
    	[FieldOffset(Offset = "0x108")]
    	private readonly CompositeDisposable _disposables = new CompositeDisposable();

    	[Token(Token = "0x400000B")]
    	[FieldOffset(Offset = "0x110")]
    	private SelectableUtility _selectableUtil;

    	[Token(Token = "0x17000005")]
    	private SelectableUtility SelectableUtil
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCD6650 -- lazy: [this+0x110], else new SelectableUtility(gameObject)
    		[Token(Token = "0x6000009")]
    		[Address(RVA = "0xCD6650", Offset = "0xCD5050", VA = "0x180CD6650")]
    		get
    		{
    			if (_selectableUtil == null)
    			{
    				_selectableUtil = new SelectableUtility(base.gameObject);
    			}
    			return _selectableUtil;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD6720 -- Selectable.OnEnable, then when IsActive a tail call through
    	//   vtable +0x3E8 = slot 43 Initialize
    	[Token(Token = "0x600000A")]
    	[Address(RVA = "0xCD6720", Offset = "0xCD5120", VA = "0x180CD6720", Slot = "5")]
    	protected override void OnEnable()
    	{
    		base.OnEnable();
    		if (SelectableUtil.IsActive)
    		{
    			Initialize();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD6780 -- Selectable.OnDisable, then _disposables (+0x108) Clear
    	[Token(Token = "0x600000B")]
    	[Address(RVA = "0xCD6780", Offset = "0xCD5180", VA = "0x180CD6780", Slot = "7")]
    	protected override void OnDisable()
    	{
    		base.OnDisable();
    		_disposables.Clear();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD67B0 -- the two observables (inlined +0x10 / +0x18 reads) subscribed
    	//   with the virtual OnHighlightingChanged (vtable +0x400, slot 44) and OnEnablingChanged (+0x410, slot 45),
    	//   AddTo(_disposables, this) (0x67FCB0)
    	// Annotated disassembly: work/title-path/disas/beb_04_Initialize.txt
    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0xCD67B0", Offset = "0xCD51B0", VA = "0x180CD67B0", Slot = "43")]
    	protected virtual void Initialize()
    	{
    		SelectableUtil.OnHighlightingChangedAsObservable().Subscribe(OnHighlightingChanged).AddTo(_disposables, this);
    		SelectableUtil.OnEnablingChangedAsObservable().Subscribe(OnEnablingChanged).AddTo(_disposables, this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD6910 -- nothing while !IsActive; states 0..4 to the matching
    	//   SelectableUtility transition; anything else ArgumentOutOfRangeException("state", boxed state, null).
    	//   Selectable.DoStateTransition (tint / sprite swap) is not called: the buttons animate themselves.
    	// Annotated disassembly: work/title-path/disas/beb_05_DoStateTransition.txt
    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0xCD6910", Offset = "0xCD5310", VA = "0x180CD6910", Slot = "26")]
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

    	[Token(Token = "0x600000E")]
    	protected abstract void OnHighlightingChanged(bool isHighlighted);

    	[Token(Token = "0x600000F")]
    	protected abstract void OnEnablingChanged(bool isEnabled);

    	[Token(Token = "0x6000010")]
    	[Address(RVA = "0xCD6AA0", Offset = "0xCD54A0", VA = "0x180CD6AA0")]
    	protected ButtonExtendedBase()
    	{
    	}
    }
}
