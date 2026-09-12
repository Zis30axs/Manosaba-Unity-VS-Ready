using Il2CppDummyDll;

namespace PPT.Noise;

[Token(Token = "0x2000006")]
public static class Perlin
{
	[Token(Token = "0x400001D")]
	[FieldOffset(Offset = "0x0")]
	private static readonly byte[] p;

	[Token(Token = "0x600000B")]
	[Address(RVA = "0x1239030", Offset = "0x1237A30", VA = "0x181239030")]
	public static float perlin(float x, float y, float z)
	{
		return default(float);
	}

	[Token(Token = "0x600000C")]
	[Address(RVA = "0x12396B0", Offset = "0x12380B0", VA = "0x1812396B0")]
	public static float OctavePerlin(float x, float y, float z, int octaves, float persistence)
	{
		return default(float);
	}

	[Token(Token = "0x600000D")]
	[Address(RVA = "0x12397C0", Offset = "0x12381C0", VA = "0x1812397C0")]
	private static float Fade(float t)
	{
		return default(float);
	}

	[Token(Token = "0x600000E")]
	[Address(RVA = "0x1239800", Offset = "0x1238200", VA = "0x181239800")]
	private static float Grad(int hash, float x, float y, float z)
	{
		return default(float);
	}
}
