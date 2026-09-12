using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel.Metadata;

[Token(Token = "0x2000067")]
public readonly struct Condition : IEquatable<Condition>
{
	[Token(Token = "0x17000072")]
	public IScriptLine Line
	{
		[Token(Token = "0x6000228")]
		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000073")]
	public Naninovel.Parsing.Command Command
	{
		[Token(Token = "0x6000229")]
		[Address(RVA = "0x457050", Offset = "0x455A50", VA = "0x180457050")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000074")]
	public Naninovel.Parsing.Parameter? Parameter
	{
		[Token(Token = "0x600022A")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000075")]
	public bool Inverted
	{
		[Token(Token = "0x600022B")]
		[Address(RVA = "0x44BDC0", Offset = "0x44A7C0", VA = "0x18044BDC0")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6000227")]
	[Address(RVA = "0x10E9580", Offset = "0x10E7F80", VA = "0x1810E9580")]
	public Condition(IScriptLine line, Naninovel.Parsing.Command command, [Optional] Naninovel.Parsing.Parameter? parameter, bool inverted = false)
	{
	}

	[Token(Token = "0x600022C")]
	[Address(RVA = "0x10E9690", Offset = "0x10E8090", VA = "0x1810E9690", Slot = "4")]
	public bool Equals(Condition other)
	{
		return default(bool);
	}

	[Token(Token = "0x600022D")]
	[Address(RVA = "0x10E9740", Offset = "0x10E8140", VA = "0x1810E9740", Slot = "0")]
	public override bool Equals(object? obj)
	{
		return default(bool);
	}

	[Token(Token = "0x600022E")]
	[Address(RVA = "0x10E97E0", Offset = "0x10E81E0", VA = "0x1810E97E0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
