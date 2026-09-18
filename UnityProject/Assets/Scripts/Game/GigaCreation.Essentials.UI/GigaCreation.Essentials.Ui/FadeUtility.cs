using System;
using Il2CppDummyDll;
using LitMotion;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Serializable]
    [Token(Token = "0x2000016")]
    public class FadeUtility
    {
    	[Token(Token = "0x4000042")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private float _alphaOnDisabled;

    	[Token(Token = "0x4000043")]
    	[FieldOffset(Offset = "0x14")]
    	[SerializeField]
    	private float _tweenDuration;

    	[Token(Token = "0x4000044")]
    	[FieldOffset(Offset = "0x18")]
    	private MotionHandle _motion;

    	[Token(Token = "0x6000053")]
    	[Address(RVA = "0xCDBCC0", Offset = "0xCDA6C0", VA = "0x180CDBCC0")]
    	public void Fade(CanvasGroup canvasGroup, bool fadeIn)
    	{
    	}

    	[Token(Token = "0x6000054")]
    	[Address(RVA = "0xCDBF50", Offset = "0xCDA950", VA = "0x180CDBF50")]
    	public FadeUtility()
    	{
    	}
    }
}
