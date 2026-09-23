using LitMotion.Extensions;
using System;
using Il2CppDummyDll;
using LitMotion;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Serializable]
    [Token(Token = "0x200001B")]
    public class SlideUtility
    {
    	[Token(Token = "0x4000053")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private float _moveXOnHighlighted;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCDD4A0: mov [rcx+0x14], 0x3DCCCCCD
    	[Token(Token = "0x4000054")]
    	[FieldOffset(Offset = "0x14")]
    	[SerializeField]
    	private float _tweenDuration = 0.1f;

    	[Token(Token = "0x4000055")]
    	[FieldOffset(Offset = "0x18")]
    	private MotionHandle _motion;

    	[Token(Token = "0x4000056")]
    	[FieldOffset(Offset = "0x24")]
    	private float _defaultAnchorPositionX;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDD140, 54 B -- _defaultAnchorPositionX (+0x24) = rectTransform.anchoredPosition.x
    	// Annotated disassembly: work/title-path/disas/sldu_0xCDD140.txt
    	[Token(Token = "0x6000061")]
    	[Address(RVA = "0xCDD140", Offset = "0xCDBB40", VA = "0x180CDD140")]
    	public void Initialize(RectTransform rectTransform)
    	{
    		_defaultAnchorPositionX = rectTransform.anchoredPosition.x;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDD180, 206 B -- an active _motion (+0x18) is cancelled (MotionManager.IsActive
    	//   0xCD57C0 / Cancel 0xCD58F0, IMotionStorage slot 4); the target x is _defaultAnchorPositionX, plus
    	//   _moveXOnHighlighted (+0x10) unless isDefault; anchoredPosition is set only when its x is not already
    	//   Mathf.Approximately the target (0x331670)
    	// Annotated disassembly: work/title-path/disas/sldu_0xCDD180.txt
    	[Token(Token = "0x6000062")]
    	[Address(RVA = "0xCDD180", Offset = "0xCDBB80", VA = "0x180CDD180")]
    	public void ForceSlide(RectTransform rectTransform, bool isDefault)
    	{
    		if (_motion.IsActive())
    		{
    			_motion.Cancel();
    		}
    		Vector2 anchoredPosition = rectTransform.anchoredPosition;
    		float num = (isDefault ? _defaultAnchorPositionX : (_defaultAnchorPositionX + _moveXOnHighlighted));
    		if (!Mathf.Approximately(anchoredPosition.x, num))
    		{
    			rectTransform.anchoredPosition = new Vector2(num, anchoredPosition.y);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDD250, 589 B -- an active _motion is cancelled; _motion =
    	//   LMotion.Create((_defaultAnchorPositionX, y), (_defaultAnchorPositionX + _moveXOnHighlighted, y), _tweenDuration
    	//   (+0x14)).WithEase(Ease 5 = OutQuad).WithScheduler(MotionScheduler.UpdateIgnoreTimeScale (static +0x60))
    	//   .BindToAnchoredPosition(rectTransform).AddTo(rectTransform) (y read from anchoredPosition each time)
    	// Annotated disassembly: work/title-path/disas/sldu_0xCDD250.txt
    	[Token(Token = "0x6000063")]
    	[Address(RVA = "0xCDD250", Offset = "0xCDBC50", VA = "0x180CDD250")]
    	public void MotorSlide(RectTransform rectTransform)
    	{
    		if (_motion.IsActive())
    		{
    			_motion.Cancel();
    		}
    		_motion = LMotion.Create(new Vector2(_defaultAnchorPositionX, rectTransform.anchoredPosition.y), new Vector2(_defaultAnchorPositionX + _moveXOnHighlighted, rectTransform.anchoredPosition.y), _tweenDuration).WithEase(Ease.OutQuad)
    			.WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
    			.BindToAnchoredPosition(rectTransform)
    			.AddTo(rectTransform);
    	}

    	[Token(Token = "0x6000064")]
    	[Address(RVA = "0xCDD4A0", Offset = "0xCDBEA0", VA = "0x180CDD4A0")]
    	public SlideUtility()
    	{
    	}
    }
}
