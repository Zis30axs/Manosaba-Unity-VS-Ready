using System;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000060")]
public readonly struct Token : IEquatable<Token>
{
	[Token(Token = "0x400010A")]
	[FieldOffset(Offset = "0x0")]
	public readonly TokenType Type;

	[Token(Token = "0x400010B")]
	[FieldOffset(Offset = "0x4")]
	public readonly ErrorType Error;

	[Token(Token = "0x400010C")]
	[FieldOffset(Offset = "0x8")]
	public readonly InlineRange Range;

	[Token(Token = "0x1700006C")]
	public int Start
	{
		[Token(Token = "0x600020F")]
		[Address(RVA = "0x55F3F0", Offset = "0x55DDF0", VA = "0x18055F3F0")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x1700006D")]
	public int EndIndex
	{
		[Token(Token = "0x6000210")]
		[Address(RVA = "0x10E8F80", Offset = "0x10E7980", VA = "0x1810E8F80")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x1700006E")]
	public int Length
	{
		[Token(Token = "0x6000211")]
		[Address(RVA = "0x5BD720", Offset = "0x5BC120", VA = "0x1805BD720")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x6000212")]
	[Address(RVA = "0x10E8F90", Offset = "0x10E7990", VA = "0x1810E8F90")]
	public Token(TokenType type, int startIndex, int length)
	{
	}

	[Token(Token = "0x6000213")]
	[Address(RVA = "0x10E8FE0", Offset = "0x10E79E0", VA = "0x1810E8FE0")]
	public Token(ErrorType error, int startIndex, int length)
	{
	}

	[Token(Token = "0x6000214")]
	[Address(RVA = "0x10E9030", Offset = "0x10E7A30", VA = "0x1810E9030")]
	private Token(TokenType type, ErrorType error, InlineRange range)
	{
	}

	[Token(Token = "0x6000215")]
	[Address(RVA = "0x10E9040", Offset = "0x10E7A40", VA = "0x1810E9040")]
	public bool IsError(ErrorType error)
	{
		return default(bool);
	}

	[Token(Token = "0x6000216")]
	[Address(RVA = "0x10E9050", Offset = "0x10E7A50", VA = "0x1810E9050", Slot = "3")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x6000217")]
	[Address(RVA = "0x10E91D0", Offset = "0x10E7BD0", VA = "0x1810E91D0", Slot = "4")]
	public bool Equals(Token other)
	{
		return default(bool);
	}

	[Token(Token = "0x6000218")]
	[Address(RVA = "0x10E9200", Offset = "0x10E7C00", VA = "0x1810E9200", Slot = "0")]
	public override bool Equals(object? obj)
	{
		return default(bool);
	}

	[Token(Token = "0x6000219")]
	[Address(RVA = "0x10E92B0", Offset = "0x10E7CB0", VA = "0x1810E92B0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
