using System;
using Il2CppDummyDll;
using LeTai.TrueShadow;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

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

	[Token(Token = "0x60003A6")]
	[Address(RVA = "0x36B2E0", Offset = "0x369CE0", VA = "0x18036B2E0")]
	private void Awake()
	{
	}

	[Token(Token = "0x60003A7")]
	[Address(RVA = "0x36B330", Offset = "0x369D30", VA = "0x18036B330")]
	private void Reset()
	{
	}

	[Token(Token = "0x60003A8")]
	[Address(RVA = "0x36B6C0", Offset = "0x36A0C0", VA = "0x18036B6C0")]
	public void Present()
	{
	}

	[Token(Token = "0x60003A9")]
	[Address(RVA = "0x36B710", Offset = "0x36A110", VA = "0x18036B710")]
	public void Dismiss()
	{
	}

	[Token(Token = "0x60003AA")]
	[Address(RVA = "0x36B760", Offset = "0x36A160", VA = "0x18036B760")]
	public IObservable<Unit> OnPointerDownAsObservable()
	{
		return null;
	}

	[Token(Token = "0x60003AB")]
	[Address(RVA = "0x36B7E0", Offset = "0x36A1E0", VA = "0x18036B7E0")]
	public IObservable<Unit> OnPointerUpAsObservable()
	{
		return null;
	}

	[Token(Token = "0x60003AC")]
	[Address(RVA = "0x36B860", Offset = "0x36A260", VA = "0x18036B860")]
	public void SetIconShadowEnabled(bool enable)
	{
	}

	[Token(Token = "0x60003AD")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SkipDebateButton()
	{
	}
}
