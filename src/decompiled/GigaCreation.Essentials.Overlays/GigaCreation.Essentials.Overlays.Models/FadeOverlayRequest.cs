using System.Threading;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Overlays.Models;

[Token(Token = "0x200000B")]
public class FadeOverlayRequest
{
	[Token(Token = "0x400001F")]
	[FieldOffset(Offset = "0x10")]
	public bool IsFadeOut;

	[Token(Token = "0x4000020")]
	[FieldOffset(Offset = "0x14")]
	public float Duration;

	[Token(Token = "0x4000021")]
	[FieldOffset(Offset = "0x18")]
	public Color? Color;

	[Token(Token = "0x4000022")]
	[FieldOffset(Offset = "0x2C")]
	public bool WaitForCompletion;

	[Token(Token = "0x4000023")]
	[FieldOffset(Offset = "0x30")]
	public CancellationToken Token;

	[Token(Token = "0x600001E")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public FadeOverlayRequest()
	{
	}
}
