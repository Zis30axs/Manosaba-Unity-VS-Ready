using System;
using System.Runtime.CompilerServices;
using System.Text;
using Il2CppDummyDll;

namespace WitchTrials.Models;

[Token(Token = "0x200017E")]
public record ScenarioInfo
{
	[Token(Token = "0x17000115")]
	[CompilerGenerated]
	protected virtual Type EqualityContract
	{
		[Token(Token = "0x600067E")]
		[Address(RVA = "0x39B6F0", Offset = "0x39A0F0", VA = "0x18039B6F0", Slot = "5")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000116")]
	public int Act
	{
		[Token(Token = "0x600067F")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
		[Token(Token = "0x6000680")]
		[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000117")]
	public int Chapter
	{
		[Token(Token = "0x6000681")]
		[Address(RVA = "0x38F580", Offset = "0x38DF80", VA = "0x18038F580")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
		[Token(Token = "0x6000682")]
		[Address(RVA = "0x39B770", Offset = "0x39A170", VA = "0x18039B770")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000118")]
	public ScenarioPartKind Part
	{
		[Token(Token = "0x6000683")]
		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590")]
		[CompilerGenerated]
		get
		{
			return default(ScenarioPartKind);
		}
		[Token(Token = "0x6000684")]
		[Address(RVA = "0x39B780", Offset = "0x39A180", VA = "0x18039B780")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000119")]
	public int Section
	{
		[Token(Token = "0x6000685")]
		[Address(RVA = "0x38F5A0", Offset = "0x38DFA0", VA = "0x18038F5A0")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
		[Token(Token = "0x6000686")]
		[Address(RVA = "0x39B790", Offset = "0x39A190", VA = "0x18039B790")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x600067D")]
	[Address(RVA = "0x39B6D0", Offset = "0x39A0D0", VA = "0x18039B6D0")]
	public ScenarioInfo(int Act, int Chapter, ScenarioPartKind Part, int Section)
	{
	}

	[Token(Token = "0x6000687")]
	[Address(RVA = "0x39B7A0", Offset = "0x39A1A0", VA = "0x18039B7A0", Slot = "3")]
	[CompilerGenerated]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x6000688")]
	[Address(RVA = "0x39B8E0", Offset = "0x39A2E0", VA = "0x18039B8E0", Slot = "6")]
	[CompilerGenerated]
	protected virtual bool PrintMembers(StringBuilder builder)
	{
		return default(bool);
	}

	[Token(Token = "0x600068B")]
	[Address(RVA = "0x39BBB0", Offset = "0x39A5B0", VA = "0x18039BBB0", Slot = "2")]
	[CompilerGenerated]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x600068D")]
	[Address(RVA = "0x39BE00", Offset = "0x39A800", VA = "0x18039BE00", Slot = "7")]
	[CompilerGenerated]
	public virtual bool Equals(ScenarioInfo? other)
	{
		return default(bool);
	}

	[Token(Token = "0x600068F")]
	[Address(RVA = "0x39C020", Offset = "0x39AA20", VA = "0x18039C020")]
	[CompilerGenerated]
	protected ScenarioInfo(ScenarioInfo original)
	{
	}

	[Token(Token = "0x6000690")]
	[Address(RVA = "0x39C050", Offset = "0x39AA50", VA = "0x18039C050")]
	[CompilerGenerated]
	public void Deconstruct(out int Act, out int Chapter, out ScenarioPartKind Part, out int Section)
	{
		Act = default;
		Chapter = default;
		Part = default;
		Section = default;
		Act = default;
		Chapter = default;
		Part = default;
		Section = default;
	}
}
