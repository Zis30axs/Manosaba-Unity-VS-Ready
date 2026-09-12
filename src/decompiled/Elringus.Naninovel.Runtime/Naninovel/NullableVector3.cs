using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000220")]
public class NullableVector3 : Nullable<Vector3>
{
	[Token(Token = "0x6000972")]
	[Address(RVA = "0x5270A0", Offset = "0x525AA0", VA = "0x1805270A0")]
	public static implicit operator NullableVector3(Vector3 value)
	{
		return null;
	}

	[Token(Token = "0x6000973")]
	[Address(RVA = "0x527150", Offset = "0x525B50", VA = "0x180527150")]
	public static implicit operator Vector3?(NullableVector3 nullable)
	{
		return null;
	}

	[Token(Token = "0x6000974")]
	[Address(RVA = "0x527240", Offset = "0x525C40", VA = "0x180527240")]
	public NullableVector3()
	{
	}
}
