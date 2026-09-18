using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Views
{
    [Token(Token = "0x200000A")]
    public class MapAreaFocusButton : ButtonExtendedBase
    {
    	[Token(Token = "0x400000C")]
    	[FieldOffset(Offset = "0x118")]
    	[Header("References")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30C910
    	// 
    	// Direct call to `Selectable.Awake` (ButtonExtendedBase adds nothing there),
    	// then `_canvasGroup.alpha = 0f`.
    	[Token(Token = "0x6000010")]
    	[Address(RVA = "0x30C910", Offset = "0x30B310", VA = "0x18030C910", Slot = "4")]
    	protected override void Awake()
    	{
    		base.Awake();
    		_canvasGroup.alpha = 0f;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30C950
    	[Token(Token = "0x6000011")]
    	[Address(RVA = "0x30C950", Offset = "0x30B350", VA = "0x18030C950")]
    	public void Enable()
    	{
    		_canvasGroup.interactable = true;
    		_canvasGroup.blocksRaycasts = true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30C990
    	[Token(Token = "0x6000012")]
    	[Address(RVA = "0x30C990", Offset = "0x30B390", VA = "0x18030C990")]
    	public void Disable()
    	{
    		_canvasGroup.interactable = false;
    		_canvasGroup.blocksRaycasts = false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30C9D0 (identical-code-folded with ModifyInEditor)
    	// 
    	// `_canvasGroup.alpha = isHighlighted ? 1f : 0f`. Written as a call to
    	// ModifyInEditor because the linker folded both methods into one body; either
    	// spelling produces the same code.
    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0x30C9D0", Offset = "0x30B3D0", VA = "0x18030C9D0", Slot = "44")]
    	protected override void OnHighlightingChanged(bool isHighlighted)
    	{
    		ModifyInEditor(isHighlighted);
    	}

    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "45")]
    	protected override void OnEnablingChanged(bool isEnabled)
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30C9D0
    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0x30C9D0", Offset = "0x30B3D0", VA = "0x18030C9D0")]
    	public void ModifyInEditor(bool show)
    	{
    		_canvasGroup.alpha = show ? 1f : 0f;
    	}

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0x30CA10", Offset = "0x30B410", VA = "0x18030CA10")]
    	public MapAreaFocusButton()
    	{
    	}
    }
}
