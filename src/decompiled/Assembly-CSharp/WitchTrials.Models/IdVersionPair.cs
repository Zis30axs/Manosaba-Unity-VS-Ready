using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace WitchTrials.Models;

[Serializable]
[Token(Token = "0x200019E")]
public class IdVersionPair : IEquatable<IdVersionPair>, IEqualityComparer<IdVersionPair>
{
	[Token(Token = "0x4000780")]
	[FieldOffset(Offset = "0x10")]
	public string Id;

	[Token(Token = "0x4000781")]
	[FieldOffset(Offset = "0x18")]
	public int Version;

	[Token(Token = "0x6000712")]
	[Address(RVA = "0x3A2CB0", Offset = "0x3A16B0", VA = "0x1803A2CB0")]
	public IdVersionPair(string id, int version)
	{
	}

	[Token(Token = "0x6000713")]
	[Address(RVA = "0x3A2D10", Offset = "0x3A1710", VA = "0x1803A2D10", Slot = "5")]
	public bool Equals(IdVersionPair x, IdVersionPair y)
	{
		return default(bool);
	}

	[Token(Token = "0x6000714")]
	[Address(RVA = "0x3A2DF0", Offset = "0x3A17F0", VA = "0x1803A2DF0", Slot = "6")]
	public int GetHashCode(IdVersionPair obj)
	{
		return default(int);
	}

	[Token(Token = "0x6000715")]
	[Address(RVA = "0x3A2E80", Offset = "0x3A1880", VA = "0x1803A2E80", Slot = "4")]
	public bool Equals(IdVersionPair other)
	{
		return default(bool);
	}

	[Token(Token = "0x6000716")]
	[Address(RVA = "0x3A2F10", Offset = "0x3A1910", VA = "0x1803A2F10")]
	public bool EqualId(string id)
	{
		return default(bool);
	}

	[Token(Token = "0x6000717")]
	[Address(RVA = "0x3A2FB0", Offset = "0x3A19B0", VA = "0x1803A2FB0")]
	public static IdVersionPair Clone(IdVersionPair from)
	{
		return null;
	}
}
