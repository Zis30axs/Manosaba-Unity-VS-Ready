using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x200010F")]
public static class AspectMatcher
{
	[Token(Token = "0x60004D7")]
	[Address(RVA = "0x485180", Offset = "0x483B80", VA = "0x180485180")]
	public static float Crop(Vector2 hostSize, Vector2 contentSize)
	{
		return default(float);
	}

	[Token(Token = "0x60004D8")]
	[Address(RVA = "0x4851B0", Offset = "0x483BB0", VA = "0x1804851B0")]
	public static float Fit(Vector2 hostSize, Vector2 contentSize)
	{
		return default(float);
	}

	[Token(Token = "0x60004D9")]
	[Address(RVA = "0x4851E0", Offset = "0x483BE0", VA = "0x1804851E0")]
	public static float Ratio(Vector2 hostSize, Vector2 contentSize, float ratio)
	{
		return default(float);
	}

	[Token(Token = "0x60004DA")]
	[Address(RVA = "0x485280", Offset = "0x483C80", VA = "0x180485280")]
	public static float Match(AspectMatchMode mode, Vector2 hostSize, Vector2 contentSize, float ratio)
	{
		return default(float);
	}
}
