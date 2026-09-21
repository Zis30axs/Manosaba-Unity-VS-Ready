using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000013")]
    public class TweeningButton : ButtonExtendedBase
    {
    	[Token(Token = "0x4000039")]
    	[FieldOffset(Offset = "0x118")]
    	[Header("References")]
    	[SerializeField]
    	private Transform _transform;

    	[Token(Token = "0x400003A")]
    	[FieldOffset(Offset = "0x120")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x400003B")]
    	[FieldOffset(Offset = "0x128")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _scaleUpWhenHighlighted;

    	[Token(Token = "0x400003C")]
    	[FieldOffset(Offset = "0x130")]
    	[SerializeField]
    	private ScaleUtility _scaleUtil;

    	[Token(Token = "0x400003D")]
    	[FieldOffset(Offset = "0x138")]
    	[SerializeField]
    	private bool _fadeOutWhenDisabled;

    	[Token(Token = "0x400003E")]
    	[FieldOffset(Offset = "0x140")]
    	[SerializeField]
    	private FadeUtility _fadeUtil;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDB530 -- flag +0x128; tail call ScaleUtility.Scale(+0x130, _transform +0x118)
    	[Token(Token = "0x600004A")]
    	[Address(RVA = "0xCDB530", Offset = "0xCD9F30", VA = "0x180CDB530", Slot = "44")]
    	protected override void OnHighlightingChanged(bool isHighlighted)
    	{
    		if (_scaleUpWhenHighlighted)
    		{
    			_scaleUtil.Scale(_transform, isHighlighted);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDB570 -- flag +0x138; tail call FadeUtility.Fade(+0x140, _canvasGroup +0x120)
    	[Token(Token = "0x600004B")]
    	[Address(RVA = "0xCDB570", Offset = "0xCD9F70", VA = "0x180CDB570", Slot = "45")]
    	protected override void OnEnablingChanged(bool isEnabled)
    	{
    		if (_fadeOutWhenDisabled)
    		{
    			_fadeUtil.Fade(_canvasGroup, isEnabled);
    		}
    	}

    	[Token(Token = "0x600004C")]
    	[Address(RVA = "0x30CA10", Offset = "0x30B410", VA = "0x18030CA10")]
    	public TweeningButton()
    	{
    	}
    }
}
