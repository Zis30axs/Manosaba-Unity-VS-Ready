using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000056")]
public class ListValueSerializer
{
	[Token(Token = "0x40000D4")]
	[FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private ISyntax _003Cstx_003EP;

	[Token(Token = "0x40000D5")]
	[FieldOffset(Offset = "0x18")]
	private readonly Utilities utils;

	[Token(Token = "0x40000D6")]
	[FieldOffset(Offset = "0x20")]
	private readonly StringBuilder builder;

	[Token(Token = "0x60001CF")]
	[Address(RVA = "0x10E3C00", Offset = "0x10E2600", VA = "0x1810E3C00")]
	public ListValueSerializer(ISyntax stx)
	{
	}

	[Token(Token = "0x60001D0")]
	[Address(RVA = "0x10E3DD0", Offset = "0x10E27D0", VA = "0x1810E3DD0")]
	public string Serialize(IReadOnlyList<string?> value)
	{
		return null;
	}

	[Token(Token = "0x60001D1")]
	[Address(RVA = "0x10E3FE0", Offset = "0x10E29E0", VA = "0x1810E3FE0")]
	private void AppendElement(IReadOnlyList<string?> value, int index)
	{
	}

	[Token(Token = "0x60001D2")]
	[Address(RVA = "0x10E40E0", Offset = "0x10E2AE0", VA = "0x1810E40E0")]
	private string EscapeElement(string element)
	{
		return null;
	}
}
