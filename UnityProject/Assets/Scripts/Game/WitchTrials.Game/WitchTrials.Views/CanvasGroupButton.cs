using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000009")]
    public sealed class CanvasGroupButton : Button
    {
    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0x108")]
    	[Header("References")]
    	[SerializeField]
    	private CanvasGroup _normal;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0x110")]
    	[SerializeField]
    	private CanvasGroup _highlighted;

    	[Token(Token = "0x400000A")]
    	[FieldOffset(Offset = "0x118")]
    	[SerializeField]
    	private CanvasGroup _disabled;

    	[Token(Token = "0x400000B")]
    	[FieldOffset(Offset = "0x120")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _ignoreParentDisabling;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30C610
    	// Selectable.DoStateTransition (real call), then tail jump to OnStateChanged(state).
    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0x30C610", Offset = "0x30B010", VA = "0x18030C610", Slot = "26")]
    	protected override void DoStateTransition(SelectionState state, bool instant)
    	{
    		base.DoStateTransition(state, instant);
    		OnStateChanged(state);
    	}

    	// RECONSTRUCTED | Confidence: A (`|` spelling: B) | RVA: 0x30C640
    	// - isHighlighted: state == 1 (Highlighted) or == 2 (Pressed).
    	// - isDisabled: `_ignoreParentDisabling` (+0x120) ? m_Interactable (+0xD8, the inlined `interactable` getter, not
    	//   IsInteractable()) == false : state == 4 (Disabled).
    	// - Each group behind an inlined UnityEngine.Object liveness test: `_normal` (+0x108) alpha = (isDisabled | isHighlighted)
    	//   ? 0 : 1; `_highlighted` (+0x110) alpha = (!isHighlighted | isDisabled) ? 0 : 1; `_disabled` (+0x118) alpha =
    	//   isDisabled ? 1 : 0.
    	[Token(Token = "0x600000E")]
    	[Address(RVA = "0x30C640", Offset = "0x30B040", VA = "0x18030C640")]
    	private void OnStateChanged(SelectionState state)
    	{
    		bool isHighlighted = state == SelectionState.Highlighted || state == SelectionState.Pressed;
    		bool isDisabled = _ignoreParentDisabling ? !interactable : state == SelectionState.Disabled;
    		if (_normal != null)
    		{
    			_normal.alpha = (isDisabled | isHighlighted) ? 0f : 1f;
    		}
    		if (_highlighted != null)
    		{
    			_highlighted.alpha = (!isHighlighted | isDisabled) ? 0f : 1f;
    		}
    		if (_disabled != null)
    		{
    			_disabled.alpha = isDisabled ? 1f : 0f;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x30C900, 7 B
    	// Tail jump to Button..ctor; no field initializers.
    	[Token(Token = "0x600000F")]
    	[Address(RVA = "0x30C900", Offset = "0x30B300", VA = "0x18030C900")]
    	public CanvasGroupButton()
    	{
    	}
    }
}
