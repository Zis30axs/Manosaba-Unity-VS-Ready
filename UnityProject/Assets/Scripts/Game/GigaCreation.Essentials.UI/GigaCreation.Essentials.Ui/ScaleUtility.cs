using System;
using Il2CppDummyDll;
using LitMotion;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Serializable]
    [Token(Token = "0x200001A")]
    public class ScaleUtility
    {
    	[Token(Token = "0x4000050")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private float _scaleOnHighlighted;

    	[Token(Token = "0x4000051")]
    	[FieldOffset(Offset = "0x14")]
    	[SerializeField]
    	private float _tweenDuration;

    	[Token(Token = "0x4000052")]
    	[FieldOffset(Offset = "0x18")]
    	private MotionHandle _motion;

    	[Token(Token = "0x600005F")]
    	[Address(RVA = "0xCDCDE0", Offset = "0xCDB7E0", VA = "0x180CDCDE0")]
    	public void Scale(Transform transform, bool scaleUp)
    	{
    	}

    	[Token(Token = "0x6000060")]
    	[Address(RVA = "0xCDD130", Offset = "0xCDBB30", VA = "0x180CDD130")]
    	public ScaleUtility()
    	{
    	}
    }
}
