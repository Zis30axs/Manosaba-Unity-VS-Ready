using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000402")]
public struct RawValue : IEquatable<RawValue>
{
	[Token(Token = "0x4000EBC")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private RawValuePart[] parts;

	[Token(Token = "0x170002A6")]
	public IReadOnlyList<RawValuePart> Parts
	{
		[Token(Token = "0x6001354")]
		[Address(RVA = "0x5AA580", Offset = "0x5A8F80", VA = "0x1805AA580")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002A7")]
	public bool Dynamic
	{
		[Token(Token = "0x6001355")]
		[Address(RVA = "0x5AA5D0", Offset = "0x5A8FD0", VA = "0x1805AA5D0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6001356")]
	[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
	public RawValue(RawValuePart[] parts)
	{
	}

	[Token(Token = "0x6001357")]
	[Address(RVA = "0x5AA5E0", Offset = "0x5A8FE0", VA = "0x1805AA5E0", Slot = "3")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x6001358")]
	[Address(RVA = "0x5AA700", Offset = "0x5A9100", VA = "0x1805AA700", Slot = "4")]
	public bool Equals(RawValue other)
	{
		return default(bool);
	}

	[Token(Token = "0x6001359")]
	[Address(RVA = "0x5AA7F0", Offset = "0x5A91F0", VA = "0x1805AA7F0", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x600135A")]
	[Address(RVA = "0x5AA880", Offset = "0x5A9280", VA = "0x1805AA880", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x600135B")]
	[Address(RVA = "0x5AA900", Offset = "0x5A9300", VA = "0x1805AA900")]
	private bool IsDynamic()
	{
		return default(bool);
	}
}
