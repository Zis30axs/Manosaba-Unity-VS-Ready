#nullable enable annotations
﻿using GigaCreation.Essentials.Ui.Runtime.Extensions;
using System;
using Il2CppDummyDll;
using LitMotion;
using TMPro;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Serializable]
    [Token(Token = "0x2000018")]
    public class LabelGlowUtility
    {
    	[Token(Token = "0x4000045")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private Color _glowColorOnDisabled;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCDC750: mov [rcx+0x20], 0x3F000000
    	[Token(Token = "0x4000046")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private float _glowDuration = 0.5f;

    	[Token(Token = "0x4000047")]
    	[FieldOffset(Offset = "0x24")]
    	[SerializeField]
    	private Ease _glowEase;

    	[Token(Token = "0x4000048")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Ease _disableGlowEase;

    	[Token(Token = "0x4000049")]
    	[FieldOffset(Offset = "0x2C")]
    	private MotionHandle _motion;

    	[Token(Token = "0x400004A")]
    	[FieldOffset(Offset = "0x38")]
    	private Color? _initialGlowColor;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDC0A0
    	// - A running motion (+0x2C) is cancelled.
    	// - Disable: from the label's glow colour (TextMeshProExtensions.GetGlowColor inlined) to _glowColorOnDisabled
    	//   (+0x10) over _glowDuration (+0x20), _disableGlowEase (+0x28).
    	// - Enable: the label's glow colour is captured once into _initialGlowColor (+0x38; the compiler's `??=` shape:
    	//   GetValueOrDefault, HasValue), then _glowColorOnDisabled -> it, _glowEase (+0x24), WithLoops(-1, 3 = Yoyo).
    	// - Both: UpdateIgnoreTimeScale, Bind(label.SetGlowColor), AddTo(label) (the MonoBehaviour overload 0xDA53D0).
    	// Annotated disassembly: work/title-path/disas/lgu_01_Glow.txt
    	[Token(Token = "0x6000057")]
    	[Address(RVA = "0xCDC0A0", Offset = "0xCDAAA0", VA = "0x180CDC0A0")]
    	public void Glow(TextMeshProUGUI label, bool enable)
    	{
    		if (_motion.IsActive())
    		{
    			_motion.Cancel();
    		}
    		if (!enable)
    		{
    			_motion = LMotion.Create(label.GetGlowColor(), _glowColorOnDisabled, _glowDuration).WithEase(_disableGlowEase)
    				.WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
    				.Bind(label.SetGlowColor)
    				.AddTo(label);
    			return;
    		}
    		if (!_initialGlowColor.HasValue)
    		{
    			_initialGlowColor = label.GetGlowColor();
    		}
    		_motion = LMotion.Create(_glowColorOnDisabled, _initialGlowColor.Value, _glowDuration).WithEase(_glowEase)
    			.WithLoops(-1, LoopType.Yoyo)
    			.WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
    			.Bind(label.SetGlowColor)
    			.AddTo(label);
    	}

    	[Token(Token = "0x6000058")]
    	[Address(RVA = "0xCDC750", Offset = "0xCDB150", VA = "0x180CDC750")]
    	public LabelGlowUtility()
    	{
    	}
    }
}
