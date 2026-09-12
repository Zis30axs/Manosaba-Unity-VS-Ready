using Il2CppDummyDll;

namespace CustomInspector;

[Token(Token = "0x200002C")]
public static class MultipleOf
{
	[Token(Token = "0x600003D")]
	[Address(RVA = "0x43D230", Offset = "0x43BC30", VA = "0x18043D230")]
	public static bool IsMultiple(double value, double step)
	{
		return default(bool);
	}

	[Token(Token = "0x600003E")]
	[Address(RVA = "0x43D2C0", Offset = "0x43BCC0", VA = "0x18043D2C0")]
	public static bool IsMultiple(float value, float step)
	{
		return default(bool);
	}

	[Token(Token = "0x600003F")]
	[Address(RVA = "0x43D350", Offset = "0x43BD50", VA = "0x18043D350")]
	public static double NearestMultiple(double value, double step)
	{
		return default(double);
	}

	[Token(Token = "0x6000040")]
	[Address(RVA = "0x43D390", Offset = "0x43BD90", VA = "0x18043D390")]
	public static float NearestMultiple(float value, float step)
	{
		return default(float);
	}

	[Token(Token = "0x6000041")]
	[Address(RVA = "0x43D430", Offset = "0x43BE30", VA = "0x18043D430")]
	public static int NearestMultiple(int value, int step)
	{
		return default(int);
	}
}
