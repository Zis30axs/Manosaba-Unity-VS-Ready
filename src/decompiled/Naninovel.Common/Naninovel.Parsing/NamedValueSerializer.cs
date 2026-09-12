using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000057")]
public class NamedValueSerializer
{
	[Token(Token = "0x40000D7")]
	[FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private ISyntax _003Cstx_003EP;

	[Token(Token = "0x40000D8")]
	[FieldOffset(Offset = "0x18")]
	private readonly Utilities utils;

	[Token(Token = "0x60001D3")]
	[Address(RVA = "0x10E42C0", Offset = "0x10E2CC0", VA = "0x1810E42C0")]
	public NamedValueSerializer(ISyntax stx)
	{
	}

	[Token(Token = "0x60001D4")]
	[Address(RVA = "0x10E4410", Offset = "0x10E2E10", VA = "0x1810E4410")]
	public string? Serialize(string? name, string? value)
	{
		return null;
	}

	[Token(Token = "0x60001D5")]
	[Address(RVA = "0x10E4510", Offset = "0x10E2F10", VA = "0x1810E4510")]
	private string EscapeName(string name)
	{
		return null;
	}
}
