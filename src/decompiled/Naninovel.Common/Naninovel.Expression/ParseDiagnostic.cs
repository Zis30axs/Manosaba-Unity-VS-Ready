using System;
using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x200009B")]
public readonly struct ParseDiagnostic : IEquatable<ParseDiagnostic>
{
	[Token(Token = "0x40001FB")]
	[FieldOffset(Offset = "0x0")]
	public readonly int Index;

	[Token(Token = "0x40001FC")]
	[FieldOffset(Offset = "0x4")]
	public readonly int Length;

	[Token(Token = "0x40001FD")]
	[FieldOffset(Offset = "0x8")]
	public readonly string Message;

	[Token(Token = "0x6000372")]
	[Address(RVA = "0x10FBFE0", Offset = "0x10FA9E0", VA = "0x1810FBFE0")]
	public ParseDiagnostic(int index, int length, string message)
	{
	}

	[Token(Token = "0x6000373")]
	[Address(RVA = "0x10FC040", Offset = "0x10FAA40", VA = "0x1810FC040", Slot = "4")]
	public bool Equals(ParseDiagnostic other)
	{
		return default(bool);
	}
}
