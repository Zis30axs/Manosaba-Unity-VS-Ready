using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000214")]
public class NamedVector3 : Named<NullableVector3>
{
	[Token(Token = "0x600094A")]
	[Address(RVA = "0x526190", Offset = "0x524B90", VA = "0x180526190")]
	public NamedVector3()
	{
	}

	[Token(Token = "0x600094B")]
	[Address(RVA = "0x5261C0", Offset = "0x524BC0", VA = "0x1805261C0")]
	public NamedVector3(string name, Vector3? value)
	{
	}
}
