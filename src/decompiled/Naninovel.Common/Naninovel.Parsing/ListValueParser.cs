using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x200003C")]
public class ListValueParser
{
	[Token(Token = "0x400009A")]
	[FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private ISyntax _003Cstx_003EP;

	[Token(Token = "0x400009B")]
	[FieldOffset(Offset = "0x18")]
	private readonly List<string?> items;

	[Token(Token = "0x400009C")]
	[FieldOffset(Offset = "0x20")]
	private readonly Utilities utils;

	[Token(Token = "0x400009D")]
	[FieldOffset(Offset = "0x28")]
	private int prevDelimiterIndex;

	[Token(Token = "0x400009E")]
	[FieldOffset(Offset = "0x30")]
	private string value;

	[Token(Token = "0x6000169")]
	[Address(RVA = "0x10DECD0", Offset = "0x10DD6D0", VA = "0x1810DECD0")]
	public ListValueParser(ISyntax stx)
	{
	}

	[Token(Token = "0x600016A")]
	[Address(RVA = "0x10DEF20", Offset = "0x10DD920", VA = "0x1810DEF20")]
	public string[] Parse(string value)
	{
		return null;
	}

	[Token(Token = "0x600016B")]
	[Address(RVA = "0x10DF1E0", Offset = "0x10DDBE0", VA = "0x1810DF1E0")]
	private void Reset(string value)
	{
	}

	[Token(Token = "0x600016C")]
	[Address(RVA = "0x10DF280", Offset = "0x10DDC80", VA = "0x1810DF280")]
	private void ProcessCharAt(int index)
	{
	}

	[Token(Token = "0x600016D")]
	[Address(RVA = "0x10DF3C0", Offset = "0x10DDDC0", VA = "0x1810DF3C0")]
	private bool IsDelimiter(int i)
	{
		return default(bool);
	}

	[Token(Token = "0x600016E")]
	[Address(RVA = "0x10DF4C0", Offset = "0x10DDEC0", VA = "0x1810DF4C0")]
	private string? ExtractBeforeDelimiter(int delimiterIndex)
	{
		return null;
	}
}
