using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000222")]
public class NullableQuaternion : Nullable<Quaternion>
{
	[Token(Token = "0x6000978")]
	[Address(RVA = "0x527430", Offset = "0x525E30", VA = "0x180527430")]
	public static implicit operator NullableQuaternion(Quaternion value)
	{
		return null;
	}

	[Token(Token = "0x6000979")]
	[Address(RVA = "0x5274E0", Offset = "0x525EE0", VA = "0x1805274E0")]
	public static implicit operator Quaternion?(NullableQuaternion nullable)
	{
		return null;
	}

	[Token(Token = "0x600097A")]
	[Address(RVA = "0x5275C0", Offset = "0x525FC0", VA = "0x1805275C0")]
	public NullableQuaternion()
	{
	}
}
