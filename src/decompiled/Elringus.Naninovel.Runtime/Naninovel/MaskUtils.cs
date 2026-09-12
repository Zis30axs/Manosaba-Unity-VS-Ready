using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20002F1")]
public static class MaskUtils
{
	[Token(Token = "0x6000E06")]
	[Address(RVA = "0x54B7A0", Offset = "0x54A1A0", VA = "0x18054B7A0")]
	public static int SetLayer(int mask, int layer, bool enabled)
	{
		return default(int);
	}

	[Token(Token = "0x6000E07")]
	[Address(RVA = "0x54B7C0", Offset = "0x54A1C0", VA = "0x18054B7C0")]
	public static bool GetLayer(int mask, int layer)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E08")]
	[Address(RVA = "0x54B7D0", Offset = "0x54A1D0", VA = "0x18054B7D0")]
	public static int SetLayer(int mask, string layerName, bool enabled)
	{
		return default(int);
	}

	[Token(Token = "0x6000E09")]
	[Address(RVA = "0x54B820", Offset = "0x54A220", VA = "0x18054B820")]
	public static bool GetLayer(int mask, string layerName)
	{
		return default(bool);
	}
}
