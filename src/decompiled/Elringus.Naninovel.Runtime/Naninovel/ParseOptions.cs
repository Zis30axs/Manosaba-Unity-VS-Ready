using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000415")]
public readonly struct ParseOptions : IEquatable<ParseOptions>
{
	[Token(Token = "0x4000EEE")]
	[FieldOffset(Offset = "0x0")]
	public readonly ICollection<ScriptParseError> Errors;

	[Token(Token = "0x4000EEF")]
	[FieldOffset(Offset = "0x8")]
	public readonly bool Transient;

	[Token(Token = "0x60013CA")]
	[Address(RVA = "0x5B2910", Offset = "0x5B1310", VA = "0x1805B2910")]
	public ParseOptions(ICollection<ScriptParseError> errors, bool transient)
	{
	}

	[Token(Token = "0x60013CB")]
	[Address(RVA = "0x5B2970", Offset = "0x5B1370", VA = "0x1805B2970", Slot = "4")]
	public bool Equals(ParseOptions other)
	{
		return default(bool);
	}

	[Token(Token = "0x60013CC")]
	[Address(RVA = "0x5B29D0", Offset = "0x5B13D0", VA = "0x1805B29D0", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x60013CD")]
	[Address(RVA = "0x5B2AA0", Offset = "0x5B14A0", VA = "0x1805B2AA0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
