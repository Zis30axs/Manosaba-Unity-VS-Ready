using System;
using Il2CppDummyDll;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Serializable]
    [Token(Token = "0x200001A")]
    public class ScaleUtility
    {
    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCDD130: mov [rcx+0x10], 0x3F8CCCCD
    	[Token(Token = "0x4000050")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private float _scaleOnHighlighted = 1.1f;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCDD130: mov [rcx+0x14], 0x3DCCCCCD
    	[Token(Token = "0x4000051")]
    	[FieldOffset(Offset = "0x14")]
    	[SerializeField]
    	private float _tweenDuration = 0.1f;

    	[Token(Token = "0x4000052")]
    	[FieldOffset(Offset = "0x18")]
    	private MotionHandle _motion;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDCDE0 -- cancel a running motion (+0x18); target Vector3.one (static +0xC)
    	//   times _scaleOnHighlighted (+0x10) or Vector3.one; LMotion from localScale over _tweenDuration (+0x14), Ease 5
    	//   (OutQuad), MotionScheduler static +0x60 (UpdateIgnoreTimeScale), BindToLocalScale, AddTo(Component) 0xDA5150
    	// Annotated disassembly: work/title-path/disas/scu_01_Scale.txt
    	[Token(Token = "0x600005F")]
    	[Address(RVA = "0xCDCDE0", Offset = "0xCDB7E0", VA = "0x180CDCDE0")]
    	public void Scale(Transform transform, bool scaleUp)
    	{
    		if (_motion.IsActive())
    		{
    			_motion.Cancel();
    		}
    		Vector3 endValue = (scaleUp ? (Vector3.one * _scaleOnHighlighted) : Vector3.one);
    		_motion = LMotion.Create(transform.localScale, endValue, _tweenDuration).WithEase(Ease.OutQuad)
    			.WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
    			.BindToLocalScale(transform)
    			.AddTo(transform);
    	}

    	[Token(Token = "0x6000060")]
    	[Address(RVA = "0xCDD130", Offset = "0xCDBB30", VA = "0x180CDD130")]
    	public ScaleUtility()
    	{
    	}
    }
}
