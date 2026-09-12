using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000213")]
public class NamedVector2 : Named<NullableVector2>
{
	[Token(Token = "0x6000948")]
	[Address(RVA = "0x525F30", Offset = "0x524930", VA = "0x180525F30")]
	public NamedVector2()
	{
	}

	[Token(Token = "0x6000949")]
	[Address(RVA = "0x525F60", Offset = "0x524960", VA = "0x180525F60")]
	public NamedVector2(string name, Vector2? value)
	{
	}
}
