using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20002C9")]
    public readonly struct FloatTween : ITweenValue
    {
    	[Token(Token = "0x4000B06")]
    	[FieldOffset(Offset = "0xC")]
    	private readonly float from;

    	[Token(Token = "0x4000B07")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly float to;

    	[Token(Token = "0x4000B08")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly Action<float> onTween;

    	[Token(Token = "0x170001DE")]
    	public Tween Props
    	{
    		[Token(Token = "0x6000D66")]
    		[Address(RVA = "0x5439D0", Offset = "0x5423D0", VA = "0x1805439D0", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return default(Tween);
    		}
    	}

    	[Token(Token = "0x6000D67")]
    	[Address(RVA = "0x545710", Offset = "0x544110", VA = "0x180545710")]
    	public FloatTween(float from, float to, Tween props, Action<float> onTween)
    	{ this = default; }

    	[Token(Token = "0x6000D68")]
    	[Address(RVA = "0x545780", Offset = "0x544180", VA = "0x180545780", Slot = "5")]
    	public void Tween(float ratio)
    	{
    	}
    }
}
