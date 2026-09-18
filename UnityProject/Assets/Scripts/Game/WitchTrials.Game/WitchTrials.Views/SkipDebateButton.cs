using System;
using System.Linq;
using Il2CppDummyDll;
using LeTai.TrueShadow;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views
{
    [Token(Token = "0x20000EA")]
    public class SkipDebateButton : MonoBehaviour
    {
    	[Token(Token = "0x400047C")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x400047D")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Selectable _selectable;

    	[Token(Token = "0x400047E")]
    	[FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private Image _iconImage;

    	[Token(Token = "0x400047F")]
    	[FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private TrueShadow _iconShadow;

    	[Token(Token = "0x4000480")]
    	[FieldOffset(Offset = "0x40")]
    	private Color _storedColor;

    	[Token(Token = "0x4000481")]
    	[FieldOffset(Offset = "0x50")]
    	private float _storedShadowSize;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36B2E0, 75 B
    	// `_iconImage` (+0x30) dispatched through vtable +0x298 = slot 22, which
    	// dump.cs lists as `Graphic.get_color` (Slot: 22); the returned Color is
    	// copied to +0x40. `_iconShadow` (+0x38) is read at +0x24, TrueShadow's
    	// `size` field, i.e. the inlined 6-byte `get_Size` (RVA 0x4B8690).
    	[Token(Token = "0x60003A6")]
    	[Address(RVA = "0x36B2E0", Offset = "0x369CE0", VA = "0x18036B2E0")]
    	private void Awake()
    	{
    		_storedColor = _iconImage.color;
    		_storedShadowSize = _iconShadow.Size;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36B330, 897 B
    	// Editor-only `Reset` wiring, in byte order: GetComponent<CanvasGroup> ->
    	// +0x20, GetComponentInChildren<Selectable> -> +0x28,
    	// GetComponentsInChildren<Image>().FirstOrDefault(<>c.<Reset>b__7_0) ->
    	// +0x30, GetComponentInChildren<TrueShadow> -> +0x38. The static lambda
    	// (RVA 0x36B980) loads Image+0xE0 (`m_Sprite`, the inlined `sprite`
    	// getter) and returns false for a null reference, else
    	// `m_CachedPtr != 0` -- the inlined UnityEngine.Object null test. Whether
    	// the source wrote `!= null` or an implicit bool conversion is not
    	// distinguishable from these bytes (B for that spelling only).
    	[Token(Token = "0x60003A7")]
    	[Address(RVA = "0x36B330", Offset = "0x369D30", VA = "0x18036B330")]
    	private void Reset()
    	{
    		_canvasGroup = GetComponent<CanvasGroup>();
    		_selectable = GetComponentInChildren<Selectable>();
    		_iconImage = GetComponentsInChildren<Image>().FirstOrDefault((Image image) => image.sprite != null);
    		_iconShadow = GetComponentInChildren<TrueShadow>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36B6C0, 70 B
    	// Alpha constant at 0x2AE2820 reads `0000803f` = 1.0f.
    	[Token(Token = "0x60003A8")]
    	[Address(RVA = "0x36B6C0", Offset = "0x36A0C0", VA = "0x18036B6C0")]
    	public void Present()
    	{
    		_canvasGroup.alpha = 1f;
    		_canvasGroup.interactable = true;
    		_canvasGroup.blocksRaycasts = true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36B710, 65 B
    	[Token(Token = "0x60003A9")]
    	[Address(RVA = "0x36B710", Offset = "0x36A110", VA = "0x18036B710")]
    	public void Dismiss()
    	{
    		_canvasGroup.alpha = 0f;
    		_canvasGroup.interactable = false;
    		_canvasGroup.blocksRaycasts = false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36B760, 114 B
    	[Token(Token = "0x60003AA")]
    	[Address(RVA = "0x36B760", Offset = "0x36A160", VA = "0x18036B760")]
    	public IObservable<Unit> OnPointerDownAsObservable()
    	{
    		return _selectable.OnPointerDownAsObservable().AsUnitObservable();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36B7E0, 114 B
    	[Token(Token = "0x60003AB")]
    	[Address(RVA = "0x36B7E0", Offset = "0x36A1E0", VA = "0x18036B7E0")]
    	public IObservable<Unit> OnPointerUpAsObservable()
    	{
    		return _selectable.OnPointerUpAsObservable().AsUnitObservable();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36B860, 121 B
    	// Shadow first (`TrueShadow.set_Size`), then `_iconImage` vtable +0x2A8 =
    	// slot 23 `Graphic.set_color`. The disabled color is a 16-byte constant at
    	// 0x2AE36E0 = (1, 1, 1, 1); writing it as `Color.white` rather than
    	// `new Color(1f, 1f, 1f, 1f)` is Confidence B (value A).
    	[Token(Token = "0x60003AC")]
    	[Address(RVA = "0x36B860", Offset = "0x36A260", VA = "0x18036B860")]
    	public void SetIconShadowEnabled(bool enable)
    	{
    		_iconShadow.Size = enable ? _storedShadowSize : 0f;
    		_iconImage.color = enable ? _storedColor : Color.white;
    	}

    	[Token(Token = "0x60003AD")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public SkipDebateButton()
    	{
    	}
    }
}
