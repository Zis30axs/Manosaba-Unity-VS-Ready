using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel.Expression;

[Token(Token = "0x200009C")]
public class ParseOptions
{
	[Token(Token = "0x170000D6")]
	public Action<ParseDiagnostic>? HandleDiagnostic
	{
		[Token(Token = "0x6000374")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000375")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170000D7")]
	public Action<ExpressionRange>? HandleRange
	{
		[Token(Token = "0x6000376")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000377")]
		[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170000D8")]
	public ISyntax Syntax
	{
		[Token(Token = "0x6000378")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000379")]
		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x600037A")]
	[Address(RVA = "0x10FC090", Offset = "0x10FAA90", VA = "0x1810FC090")]
	public ParseOptions()
	{
	}
}
