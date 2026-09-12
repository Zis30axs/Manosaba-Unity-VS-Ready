using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Metadata;

[Token(Token = "0x200006C")]
public readonly struct Endpoint : IEquatable<Endpoint>
{
	[Token(Token = "0x17000079")]
	public string? ScriptPath
	{
		[Token(Token = "0x6000241")]
		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700007A")]
	public string? Label
	{
		[Token(Token = "0x6000242")]
		[Address(RVA = "0x457050", Offset = "0x455A50", VA = "0x180457050")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000240")]
	[Address(RVA = "0x42B240", Offset = "0x429C40", VA = "0x18042B240")]
	public Endpoint(string? scriptPath, string? label)
	{
	}

	[Token(Token = "0x6000243")]
	[Address(RVA = "0x457440", Offset = "0x455E40", VA = "0x180457440", Slot = "4")]
	public bool Equals(Endpoint other)
	{
		return default(bool);
	}

	[Token(Token = "0x6000244")]
	[Address(RVA = "0x10EAAD0", Offset = "0x10E94D0", VA = "0x1810EAAD0", Slot = "0")]
	public override bool Equals(object? obj)
	{
		return default(bool);
	}

	[Token(Token = "0x6000245")]
	[Address(RVA = "0x5358C0", Offset = "0x5342C0", VA = "0x1805358C0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
