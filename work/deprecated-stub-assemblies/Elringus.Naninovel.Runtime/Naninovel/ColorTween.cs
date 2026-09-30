using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20002C6")]
    public readonly struct ColorTween : ITweenValue
    {
    	[Token(Token = "0x4000ADF")]
    	[FieldOffset(Offset = "0xC")]
    	private readonly Color from;

    	[Token(Token = "0x4000AE0")]
    	[FieldOffset(Offset = "0x1C")]
    	private readonly Color to;

    	[Token(Token = "0x4000AE1")]
    	[FieldOffset(Offset = "0x2C")]
    	private readonly ColorTweenMode mode;

    	[Token(Token = "0x4000AE2")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly Action<Color> onTween;

    	[Token(Token = "0x170001DD")]
    	public Tween Props
    	{
    		[Token(Token = "0x6000D41")]
    		[Address(RVA = "0x5439D0", Offset = "0x5423D0", VA = "0x1805439D0", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return default(Tween);
    		}
    	}

    	[Token(Token = "0x6000D42")]
    	[Address(RVA = "0x5439F0", Offset = "0x5423F0", VA = "0x1805439F0")]
    	public ColorTween(Color from, Color to, Tween props, ColorTweenMode mode, Action<Color> onTween)
    	{ this = default; }

    	[Token(Token = "0x6000D43")]
    	[Address(RVA = "0x543A70", Offset = "0x542470", VA = "0x180543A70", Slot = "5")]
    	public void Tween(float ratio)
    	{
    	}
    }
}
