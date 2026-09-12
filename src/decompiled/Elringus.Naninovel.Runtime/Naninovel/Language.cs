using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x200037F")]
public struct Language
{
	[Token(Token = "0x4000D0A")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private string tag;

	[Token(Token = "0x4000D0B")]
	[FieldOffset(Offset = "0x8")]
	[SerializeField]
	private string name;

	[Token(Token = "0x1700024B")]
	public string Tag
	{
		[Token(Token = "0x60010F0")]
		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700024C")]
	public string Name
	{
		[Token(Token = "0x60010F1")]
		[Address(RVA = "0x457050", Offset = "0x455A50", VA = "0x180457050")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60010F2")]
	[Address(RVA = "0x42B240", Offset = "0x429C40", VA = "0x18042B240")]
	public Language(string tag, string name)
	{
	}
}
