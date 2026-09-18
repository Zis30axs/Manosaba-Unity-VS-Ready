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

    	[Token(Token = "0x4000054")]
    	[FieldOffset(Offset = "0x14")]
    	[SerializeField]
    	private float _tweenDuration;

    	[Token(Token = "0x4000055")]
    	[FieldOffset(Offset = "0x18")]
    	private MotionHandle _motion;

    	[Token(Token = "0x4000056")]
    	[FieldOffset(Offset = "0x24")]
    	private float _defaultAnchorPositionX;

    	[Token(Token = "0x6000061")]
    	[Address(RVA = "0xCDD140", Offset = "0xCDBB40", VA = "0x180CDD140")]
    	public void Initialize(RectTransform rectTransform)
    	{
    	}

    	[Token(Token = "0x6000062")]
    	[Address(RVA = "0xCDD180", Offset = "0xCDBB80", VA = "0x180CDD180")]
    	public void ForceSlide(RectTransform rectTransform, bool isDefault)
    	{
    	}

    	[Token(Token = "0x6000063")]
    	[Address(RVA = "0xCDD250", Offset = "0xCDBC50", VA = "0x180CDD250")]
    	public void MotorSlide(RectTransform rectTransform)
    	{
    	}

    	[Token(Token = "0x6000064")]
    	[Address(RVA = "0xCDD4A0", Offset = "0xCDBEA0", VA = "0x180CDD4A0")]
    	public SlideUtility()
    	{
    	}
    }
}
