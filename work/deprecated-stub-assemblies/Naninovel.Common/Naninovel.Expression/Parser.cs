#nullable enable annotations
﻿using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x200009D")]
    public class Parser
    {
    	[Token(Token = "0x4000201")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly Stack<Token> tokens;

    	[Token(Token = "0x4000202")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly List<(string var, string exp)> asses;

    	[Token(Token = "0x4000203")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly Lexer lexer;

    	[Token(Token = "0x4000204")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly ParseOptions options;

    	[Token(Token = "0x4000205")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly AssignmentParser assParser;

    	[Token(Token = "0x4000206")]
    	[FieldOffset(Offset = "0x38")]
    	private Token lastToken;

    	[Token(Token = "0x4000207")]
    	[FieldOffset(Offset = "0x48")]
    	private string text;

    	[Token(Token = "0x4000208")]
    	[FieldOffset(Offset = "0x50")]
    	private int assOffset;

    	[Token(Token = "0x4000209")]
    	[FieldOffset(Offset = "0x54")]
    	private bool anyError;

    	[Token(Token = "0x170000D9")]
    	private Token token
    	{
    		[Token(Token = "0x600037B")]
    		[Address(RVA = "0x10FC140", Offset = "0x10FAB40", VA = "0x1810FC140")]
    		get
    		{
    			return default(Token);
    		}
    	}

    	[Token(Token = "0x600037C")]
    	[Address(RVA = "0x10FC1F0", Offset = "0x10FABF0", VA = "0x1810FC1F0")]
    	public Parser(ParseOptions options)
    	{
    	}

    	[Token(Token = "0x600037D")]
    	[Address(RVA = "0x10FC610", Offset = "0x10FB010", VA = "0x1810FC610")]
    	public bool TryParse(string text, out IExpression exp)
    	{
    		exp = default;
    		exp = default;
    		return default(bool);
    	}

    	[Token(Token = "0x600037E")]
    	[Address(RVA = "0x10FC6B0", Offset = "0x10FB0B0", VA = "0x1810FC6B0")]
    	public bool TryParseAssignments(string text, IList<Assignment> assignments)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600037F")]
    	[Address(RVA = "0x10FCBA0", Offset = "0x10FB5A0", VA = "0x1810FCBA0")]
    	private void Reset(string text)
    	{
    	}

    	[Token(Token = "0x6000380")]
    	[Address(RVA = "0x10FCD20", Offset = "0x10FB720", VA = "0x1810FCD20")]
    	private IExpression Map(IExpression exp, int idx, int length)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000381")]
    	[Address(RVA = "0x10FCDE0", Offset = "0x10FB7E0", VA = "0x1810FCDE0")]
    	private IExpression? TryExpression()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000382")]
    	[Address(RVA = "0x10FCDF0", Offset = "0x10FB7F0", VA = "0x1810FCDF0")]
    	private IExpression? TryTernary()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000383")]
    	[Address(RVA = "0x10FD0C0", Offset = "0x10FBAC0", VA = "0x1810FD0C0")]
    	private IExpression? TryLogicalOr()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000384")]
    	[Address(RVA = "0x10FD340", Offset = "0x10FBD40", VA = "0x1810FD340")]
    	private IExpression? TryLogicalAnd()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000385")]
    	[Address(RVA = "0x10FD5C0", Offset = "0x10FBFC0", VA = "0x1810FD5C0")]
    	private IExpression? TryRelational()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000386")]
    	[Address(RVA = "0x10FDA40", Offset = "0x10FC440", VA = "0x1810FDA40")]
    	private IExpression? TryAdditive()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000387")]
    	[Address(RVA = "0x10FDD50", Offset = "0x10FC750", VA = "0x1810FDD50")]
    	private IExpression? TryMultiplicative()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000388")]
    	[Address(RVA = "0x10FE080", Offset = "0x10FCA80", VA = "0x1810FE080")]
    	private IExpression? TryUnary()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000389")]
    	[Address(RVA = "0x10FE3B0", Offset = "0x10FCDB0", VA = "0x1810FE3B0")]
    	private IExpression? TryPow()
    	{
    		return null;
    	}

    	[Token(Token = "0x600038A")]
    	[Address(RVA = "0x10FE600", Offset = "0x10FD000", VA = "0x1810FE600")]
    	private IExpression? TrySymbol()
    	{
    		return null;
    	}

    	[Token(Token = "0x600038B")]
    	[Address(RVA = "0x10FE660", Offset = "0x10FD060", VA = "0x1810FE660")]
    	private IExpression TryFunction(string name)
    	{
    		return null;
    	}

    	[Token(Token = "0x600038C")]
    	[Address(RVA = "0x10FEA40", Offset = "0x10FD440", VA = "0x1810FEA40")]
    	private IExpression TryBoolean(string name)
    	{
    		return null;
    	}

    	[Token(Token = "0x600038D")]
    	[Address(RVA = "0x10FED10", Offset = "0x10FD710", VA = "0x1810FED10")]
    	private IExpression DoVariable(string name)
    	{
    		return null;
    	}

    	[Token(Token = "0x600038E")]
    	[Address(RVA = "0x10FEDE0", Offset = "0x10FD7E0", VA = "0x1810FEDE0")]
    	private IExpression? TryString()
    	{
    		return null;
    	}

    	[Token(Token = "0x600038F")]
    	[Address(RVA = "0x10FF0D0", Offset = "0x10FDAD0", VA = "0x1810FF0D0")]
    	private IExpression? TryNumeric()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000390")]
    	[Address(RVA = "0x10FF2C0", Offset = "0x10FDCC0", VA = "0x1810FF2C0")]
    	private IExpression? TryClosure()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000391")]
    	[Address(RVA = "0x10FF3D0", Offset = "0x10FDDD0", VA = "0x1810FF3D0")]
    	private Token Consume()
    	{
    		return default(Token);
    	}

    	[Token(Token = "0x6000392")]
    	[Address(RVA = "0x10FF490", Offset = "0x10FDE90", VA = "0x1810FF490")]
    	private bool Expect(string content)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000393")]
    	[Address(RVA = "0x10FF560", Offset = "0x10FDF60", VA = "0x1810FF560")]
    	private Invalid Err(string message)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000394")]
    	[Address(RVA = "0x10FF6D0", Offset = "0x10FE0D0", VA = "0x1810FF6D0")]
    	private void Err(ParseDiagnostic diag)
    	{
    	}

    	[Token(Token = "0x6000395")]
    	[Address(RVA = "0x10FF780", Offset = "0x10FE180", VA = "0x1810FF780")]
    	private bool Is(string content)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000396")]
    	[Address(RVA = "0x10FF7F0", Offset = "0x10FE1F0", VA = "0x1810FF7F0")]
    	private bool IsOp()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000397")]
    	[Address(RVA = "0x10FF810", Offset = "0x10FE210", VA = "0x1810FF810")]
    	private bool IsEnd()
    	{
    		return default(bool);
    	}
    }
}
