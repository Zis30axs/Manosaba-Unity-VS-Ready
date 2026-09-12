using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000054")]
public class PlainText
{
	[Token(Token = "0x40000D1")]
	[FieldOffset(Offset = "0x0")]
	public static readonly PlainText Empty;

	[Token(Token = "0x17000054")]
	public string Text
	{
		[Token(Token = "0x60001C4")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000055")]
	public char this[int index]
	{
		[Token(Token = "0x60001C7")]
		[Address(RVA = "0x10E38A0", Offset = "0x10E22A0", VA = "0x1810E38A0")]
		get
		{
			return default(char);
		}
	}

	[Token(Token = "0x60001C3")]
	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
	public PlainText(string text)
	{
	}

	[Token(Token = "0x60001C5")]
	[Address(RVA = "0x10E37B0", Offset = "0x10E21B0", VA = "0x1810E37B0")]
	public static implicit operator string(PlainText? plainText)
	{
		return null;
	}

	[Token(Token = "0x60001C6")]
	[Address(RVA = "0x10E37C0", Offset = "0x10E21C0", VA = "0x1810E37C0")]
	public static implicit operator PlainText(string? @string)
	{
		return null;
	}

	[Token(Token = "0x60001C8")]
	[Address(RVA = "0x10E38E0", Offset = "0x10E22E0", VA = "0x1810E38E0", Slot = "4")]
	public IEnumerator<char> GetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x60001C9")]
	[Address(RVA = "0x10E38E0", Offset = "0x10E22E0", VA = "0x1810E38E0", Slot = "5")]
	[ExcludeFromCodeCoverage]
	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x60001CA")]
	[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "3")]
	public override string ToString()
	{
		return null;
	}
}
