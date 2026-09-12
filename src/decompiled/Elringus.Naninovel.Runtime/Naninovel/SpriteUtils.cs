using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000300")]
public static class SpriteUtils
{
	[Token(Token = "0x6000E44")]
	[Address(RVA = "0x54E9A0", Offset = "0x54D3A0", VA = "0x18054E9A0")]
	public static void SetOpacity(this SpriteRenderer spriteRenderer, float opacity)
	{
	}

	[Token(Token = "0x6000E45")]
	[Address(RVA = "0x54EB10", Offset = "0x54D510", VA = "0x18054EB10")]
	public static bool IsTransparent(this SpriteRenderer spriteRenderer)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E46")]
	[Address(RVA = "0x54EBD0", Offset = "0x54D5D0", VA = "0x18054EBD0")]
	public static bool IsOpaque(this SpriteRenderer spriteRenderer)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E47")]
	[Address(RVA = "0x54EC90", Offset = "0x54D690", VA = "0x18054EC90")]
	public static Rect GetVerticesRect(this Sprite sprite)
	{
		return default(Rect);
	}
}
