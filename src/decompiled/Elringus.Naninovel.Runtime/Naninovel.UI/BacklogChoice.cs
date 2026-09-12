using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x20004F5")]
public readonly struct BacklogChoice : IEquatable<BacklogChoice>
{
	[Token(Token = "0x1700037D")]
	public LocalizableText Summary
	{
		[Token(Token = "0x60018B4")]
		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
		[CompilerGenerated]
		get
		{
			return default(LocalizableText);
		}
	}

	[Token(Token = "0x1700037E")]
	public bool Selected
	{
		[Token(Token = "0x60018B5")]
		[Address(RVA = "0x485440", Offset = "0x483E40", VA = "0x180485440")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x60018B6")]
	[Address(RVA = "0x5B2910", Offset = "0x5B1310", VA = "0x1805B2910")]
	public BacklogChoice(LocalizableText summary, bool selected)
	{
	}

	[Token(Token = "0x60018B7")]
	[Address(RVA = "0x60D890", Offset = "0x60C290", VA = "0x18060D890", Slot = "4")]
	public bool Equals(BacklogChoice other)
	{
		return default(bool);
	}

	[Token(Token = "0x60018B8")]
	[Address(RVA = "0x60D920", Offset = "0x60C320", VA = "0x18060D920", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x60018B9")]
	[Address(RVA = "0x60DA20", Offset = "0x60C420", VA = "0x18060DA20", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
