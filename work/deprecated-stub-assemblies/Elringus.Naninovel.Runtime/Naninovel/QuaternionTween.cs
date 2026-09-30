using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20002CB")]
    public readonly struct QuaternionTween : ITweenValue
    {
    	[Token(Token = "0x4000B0A")]
    	[FieldOffset(Offset = "0xC")]
    	private readonly Quaternion startValue;

    	[Token(Token = "0x4000B0B")]
    	[FieldOffset(Offset = "0x1C")]
    	private readonly Quaternion targetValue;

    	[Token(Token = "0x4000B0C")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly Action<Quaternion> onTween;

    	[Token(Token = "0x170001E0")]
    	public Tween Props
    	{
    		[Token(Token = "0x6000D6B")]
    		[Address(RVA = "0x5439D0", Offset = "0x5423D0", VA = "0x1805439D0", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return default(Tween);
    		}
    	}

    	[Token(Token = "0x6000D6C")]
    	[Address(RVA = "0x5457F0", Offset = "0x5441F0", VA = "0x1805457F0")]
    	public QuaternionTween(Quaternion from, Quaternion to, Tween props, Action<Quaternion> onTween)
    	{ this = default; }

    	[Token(Token = "0x6000D6D")]
    	[Address(RVA = "0x545870", Offset = "0x544270", VA = "0x180545870", Slot = "5")]
    	public void Tween(float ratio)
    	{
    	}
    }
}
