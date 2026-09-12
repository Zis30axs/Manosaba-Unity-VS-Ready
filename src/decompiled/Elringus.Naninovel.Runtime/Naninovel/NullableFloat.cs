using System;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x200021D")]
public class NullableFloat : Nullable<float>
{
	[Token(Token = "0x6000969")]
	[Address(RVA = "0x526BA0", Offset = "0x5255A0", VA = "0x180526BA0")]
	public static implicit operator NullableFloat(float value)
	{
		return null;
	}

	[Token(Token = "0x600096A")]
	[Address(RVA = "0x526C50", Offset = "0x525650", VA = "0x180526C50")]
	public static implicit operator float?(NullableFloat nullable)
	{
		return null;
	}

	[Token(Token = "0x600096B")]
	[Address(RVA = "0x526D10", Offset = "0x525710", VA = "0x180526D10")]
	public NullableFloat()
	{
	}
}
