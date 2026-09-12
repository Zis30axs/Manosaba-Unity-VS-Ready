using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000031")]
public class Lexer
{
	[Token(Token = "0x400006B")]
	[FieldOffset(Offset = "0x10")]
	private readonly LexState state;

	[Token(Token = "0x400006C")]
	[FieldOffset(Offset = "0x18")]
	private readonly CommandBodyLexer commandLexer;

	[Token(Token = "0x400006D")]
	[FieldOffset(Offset = "0x20")]
	private readonly GenericLineLexer genericLineLexer;

	[Token(Token = "0x400006E")]
	[FieldOffset(Offset = "0x28")]
	private readonly ISyntax stx;

	[Token(Token = "0x60000FE")]
	[Address(RVA = "0x10D6760", Offset = "0x10D5160", VA = "0x1810D6760")]
	public Lexer(ISyntax syntax)
	{
	}

	[Token(Token = "0x60000FF")]
	[Address(RVA = "0x10D6D50", Offset = "0x10D5750", VA = "0x1810D6D50")]
	public LineType TokenizeLine(string text, ICollection<Token> tokens)
	{
		return default(LineType);
	}

	[Token(Token = "0x6000100")]
	[Address(RVA = "0x10D72E0", Offset = "0x10D5CE0", VA = "0x1810D72E0")]
	public void TokenizeCommandBody(string text, ICollection<Token> tokens)
	{
	}

	[Token(Token = "0x6000101")]
	[Address(RVA = "0x10D7320", Offset = "0x10D5D20", VA = "0x1810D7320")]
	private LineType? TryEmptyLine()
	{
		return null;
	}

	[Token(Token = "0x6000102")]
	[Address(RVA = "0x10D73C0", Offset = "0x10D5DC0", VA = "0x1810D73C0")]
	private LineType? TryCommentLine()
	{
		return null;
	}

	[Token(Token = "0x6000103")]
	[Address(RVA = "0x10D7510", Offset = "0x10D5F10", VA = "0x1810D7510")]
	private LineType? TryLabelLine()
	{
		return null;
	}

	[Token(Token = "0x6000104")]
	[Address(RVA = "0x10D76E0", Offset = "0x10D60E0", VA = "0x1810D76E0")]
	private LineType? TryCommandLine()
	{
		return null;
	}

	[Token(Token = "0x6000105")]
	[Address(RVA = "0x10D7860", Offset = "0x10D6260", VA = "0x1810D7860")]
	private void AddLineIdentifier()
	{
	}
}
