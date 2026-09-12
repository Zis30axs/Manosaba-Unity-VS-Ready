using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000044")]
public class Command : ILineComponent
{
	[Token(Token = "0x1700003C")]
	public PlainText Identifier
	{
		[Token(Token = "0x600018F")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700003D")]
	public IReadOnlyList<Parameter> Parameters
	{
		[Token(Token = "0x6000190")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600018E")]
	[Address(RVA = "0x10E2150", Offset = "0x10E0B50", VA = "0x1810E2150")]
	public Command(PlainText identifier, [Optional] IReadOnlyList<Parameter>? parameters)
	{
	}

	[Token(Token = "0x6000191")]
	[Address(RVA = "0x10E2250", Offset = "0x10E0C50", VA = "0x1810E2250", Slot = "3")]
	public override string ToString()
	{
		return null;
	}
}
