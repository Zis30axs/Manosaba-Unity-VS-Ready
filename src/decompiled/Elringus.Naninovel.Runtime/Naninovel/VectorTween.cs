using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20002D1")]
public readonly struct VectorTween : ITweenValue
{
	[Token(Token = "0x4000B24")]
	[FieldOffset(Offset = "0xC")]
	private readonly Vector3 from;

	[Token(Token = "0x4000B25")]
	[FieldOffset(Offset = "0x18")]
	private readonly Vector3 to;

	[Token(Token = "0x4000B26")]
	[FieldOffset(Offset = "0x28")]
	private readonly Action<Vector3> onTween;

	[Token(Token = "0x170001E5")]
	public Tween Props
	{
		[Token(Token = "0x6000D8A")]
		[Address(RVA = "0x5439D0", Offset = "0x5423D0", VA = "0x1805439D0", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return default(Tween);
		}
	}

	[Token(Token = "0x6000D8B")]
	[Address(RVA = "0x545BA0", Offset = "0x5445A0", VA = "0x180545BA0")]
	public VectorTween(Vector3 from, Vector3 to, Tween props, Action<Vector3> onTween)
	{
	}

	[Token(Token = "0x6000D8C")]
	[Address(RVA = "0x545C30", Offset = "0x544630", VA = "0x180545C30", Slot = "5")]
	public void Tween(float ratio)
	{
	}
}
