using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000221")]
public class NullableVector4 : Nullable<Vector4>
{
	[Token(Token = "0x6000975")]
	[Address(RVA = "0x527270", Offset = "0x525C70", VA = "0x180527270")]
	public static implicit operator NullableVector4(Vector4 value)
	{
		return null;
	}

	[Token(Token = "0x6000976")]
	[Address(RVA = "0x527320", Offset = "0x525D20", VA = "0x180527320")]
	public static implicit operator Vector4?(NullableVector4 nullable)
	{
		return null;
	}

	[Token(Token = "0x6000977")]
	[Address(RVA = "0x527400", Offset = "0x525E00", VA = "0x180527400")]
	public NullableVector4()
	{
	}
}
