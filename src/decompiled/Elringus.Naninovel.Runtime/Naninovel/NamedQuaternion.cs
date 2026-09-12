using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000216")]
public class NamedQuaternion : Named<NullableQuaternion>
{
	[Token(Token = "0x600094E")]
	[Address(RVA = "0x526650", Offset = "0x525050", VA = "0x180526650")]
	public NamedQuaternion()
	{
	}

	[Token(Token = "0x600094F")]
	[Address(RVA = "0x526680", Offset = "0x525080", VA = "0x180526680")]
	public NamedQuaternion(string name, Quaternion? value)
	{
	}
}
