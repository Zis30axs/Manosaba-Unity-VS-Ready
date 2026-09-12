using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x2000099")]
internal class Lexer
{
	[Token(Token = "0x40001F1")]
	[FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private Action<ParseDiagnostic> _003Cerr_003EP;

	[Token(Token = "0x40001F2")]
	[FieldOffset(Offset = "0x18")]
	private readonly List<Token> tokens;

	[Token(Token = "0x40001F3")]
	[FieldOffset(Offset = "0x20")]
	private readonly StringBuilder str;

	[Token(Token = "0x40001F4")]
	[FieldOffset(Offset = "0x28")]
	private string text;

	[Token(Token = "0x40001F5")]
	[FieldOffset(Offset = "0x30")]
	private int index;

	[Token(Token = "0x40001F6")]
	[FieldOffset(Offset = "0x34")]
	private int balance;

	[Token(Token = "0x600035E")]
	[Address(RVA = "0x10F9180", Offset = "0x10F7B80", VA = "0x1810F9180")]
	public Lexer(Action<ParseDiagnostic> err)
	{
	}

	[Token(Token = "0x600035F")]
	[Address(RVA = "0x10F9390", Offset = "0x10F7D90", VA = "0x1810F9390")]
	public Token[] Lex(string text)
	{
		return null;
	}

	[Token(Token = "0x6000360")]
	[Address(RVA = "0x10F95F0", Offset = "0x10F7FF0", VA = "0x1810F95F0")]
	private void Reset(string text)
	{
	}

	[Token(Token = "0x6000361")]
	[Address(RVA = "0x10F9700", Offset = "0x10F8100", VA = "0x1810F9700")]
	private bool LexNext(char c)
	{
		return default(bool);
	}

	[Token(Token = "0x6000362")]
	[Address(RVA = "0x10F99C0", Offset = "0x10F83C0", VA = "0x1810F99C0")]
	private char Peek(int offset = 0)
	{
		return default(char);
	}

	[Token(Token = "0x6000363")]
	[Address(RVA = "0x10F9A10", Offset = "0x10F8410", VA = "0x1810F9A10")]
	private char Consume()
	{
		return default(char);
	}

	[Token(Token = "0x6000364")]
	[Address(RVA = "0x10F9A70", Offset = "0x10F8470", VA = "0x1810F9A70")]
	private void LexOperator(string op)
	{
	}

	[Token(Token = "0x6000365")]
	[Address(RVA = "0x10F9D10", Offset = "0x10F8710", VA = "0x1810F9D10")]
	private void LexNumber()
	{
	}

	[Token(Token = "0x6000366")]
	[Address(RVA = "0x10FA180", Offset = "0x10F8B80", VA = "0x1810FA180")]
	private void LexIdentifier()
	{
	}

	[Token(Token = "0x6000367")]
	[Address(RVA = "0x10FA450", Offset = "0x10F8E50", VA = "0x1810FA450")]
	private void LexString()
	{
	}

	[Token(Token = "0x6000368")]
	[Address(RVA = "0x10FA8D0", Offset = "0x10F92D0", VA = "0x1810FA8D0")]
	private bool IsSpace(char c)
	{
		return default(bool);
	}

	[Token(Token = "0x6000369")]
	[Address(RVA = "0x10FA920", Offset = "0x10F9320", VA = "0x1810FA920")]
	private bool IsOperator(char c, out string op)
	{
		op = default;
		op = default;
		return default(bool);
	}

	[Token(Token = "0x600036A")]
	[Address(RVA = "0x10FA9D0", Offset = "0x10F93D0", VA = "0x1810FA9D0")]
	private bool IsNumber(char c)
	{
		return default(bool);
	}

	[Token(Token = "0x600036B")]
	[Address(RVA = "0x10FAA40", Offset = "0x10F9440", VA = "0x1810FAA40")]
	private bool IsIdentifier(char c)
	{
		return default(bool);
	}

	[Token(Token = "0x600036C")]
	[Address(RVA = "0x10FAA90", Offset = "0x10F9490", VA = "0x1810FAA90")]
	private bool IsQuote(char c)
	{
		return default(bool);
	}

	[Token(Token = "0x600036D")]
	[Address(RVA = "0x10FAAA0", Offset = "0x10F94A0", VA = "0x1810FAAA0")]
	private bool IsEnd()
	{
		return default(bool);
	}
}
