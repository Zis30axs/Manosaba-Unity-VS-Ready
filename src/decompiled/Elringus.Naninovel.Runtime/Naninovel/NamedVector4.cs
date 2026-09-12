using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000215")]
public class NamedVector4 : Named<NullableVector4>
{
	[Token(Token = "0x600094C")]
	[Address(RVA = "0x5263F0", Offset = "0x524DF0", VA = "0x1805263F0")]
	public NamedVector4()
	{
	}

	[Token(Token = "0x600094D")]
	[Address(RVA = "0x526420", Offset = "0x524E20", VA = "0x180526420")]
	public NamedVector4(string name, Vector4? value)
	{
	}
}
