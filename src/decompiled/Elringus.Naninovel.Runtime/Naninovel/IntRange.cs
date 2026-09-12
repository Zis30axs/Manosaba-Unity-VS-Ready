using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000209")]
public struct IntRange : IEquatable<IntRange>
{
	[Token(Token = "0x40008A8")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private int startIndex;

	[Token(Token = "0x40008A9")]
	[FieldOffset(Offset = "0x4")]
	[SerializeField]
	private int endIndex;

	[Token(Token = "0x1700014D")]
	public int StartIndex
	{
		[Token(Token = "0x6000928")]
		[Address(RVA = "0x437AC0", Offset = "0x4364C0", VA = "0x180437AC0")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x1700014E")]
	public int EndIndex
	{
		[Token(Token = "0x6000929")]
		[Address(RVA = "0x4364D0", Offset = "0x434ED0", VA = "0x1804364D0")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x600092A")]
	[Address(RVA = "0x525660", Offset = "0x524060", VA = "0x180525660")]
	public IntRange(int startIndex, int endIndex)
	{
	}

	[Token(Token = "0x600092B")]
	[Address(RVA = "0x525670", Offset = "0x524070", VA = "0x180525670")]
	public bool Contains(int index)
	{
		return default(bool);
	}

	[Token(Token = "0x600092C")]
	[Address(RVA = "0x525680", Offset = "0x524080", VA = "0x180525680", Slot = "4")]
	public bool Equals(IntRange other)
	{
		return default(bool);
	}

	[Token(Token = "0x600092D")]
	[Address(RVA = "0x5256A0", Offset = "0x5240A0", VA = "0x1805256A0", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x600092E")]
	[Address(RVA = "0x525730", Offset = "0x524130", VA = "0x180525730", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x600092F")]
	[Address(RVA = "0x525740", Offset = "0x524140", VA = "0x180525740")]
	public static bool operator ==(IntRange left, IntRange right)
	{
		return default(bool);
	}

	[Token(Token = "0x6000930")]
	[Address(RVA = "0x525760", Offset = "0x524160", VA = "0x180525760")]
	public static bool operator !=(IntRange left, IntRange right)
	{
		return default(bool);
	}
}
