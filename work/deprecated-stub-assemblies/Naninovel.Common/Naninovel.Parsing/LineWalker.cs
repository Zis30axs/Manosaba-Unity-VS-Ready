#nullable enable annotations
﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x200003A")]
    internal class LineWalker
    {
    	[Token(Token = "0x4000094")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private ParseHandlers _003Chandlers_003EP;

    	[Token(Token = "0x4000095")]
    	[FieldOffset(Offset = "0x18")]
    	private string lineText;

    	[Token(Token = "0x4000096")]
    	[FieldOffset(Offset = "0x20")]
    	private IReadOnlyList<Token> tokens;

    	[Token(Token = "0x4000097")]
    	[FieldOffset(Offset = "0x28")]
    	private int index;

    	[Token(Token = "0x6000154")]
    	[Address(RVA = "0x10DD960", Offset = "0x10DC360", VA = "0x1810DD960")]
    	public LineWalker(ParseHandlers handlers)
    	{
    	}

    	[Token(Token = "0x6000155")]
    	[Address(RVA = "0x10DDAD0", Offset = "0x10DC4D0", VA = "0x1810DDAD0")]
    	public void Reset(string lineText, IReadOnlyList<Token> tokens)
    	{
    	}

    	[Token(Token = "0x6000156")]
    	[Address(RVA = "0x10DDB90", Offset = "0x10DC590", VA = "0x1810DDB90")]
    	public char GetCharAt(int index)
    	{
    		return default(char);
    	}

    	[Token(Token = "0x6000157")]
    	[Address(RVA = "0x10DDBD0", Offset = "0x10DC5D0", VA = "0x1810DDBD0")]
    	public bool TryGetCharAt(int index, out char nextChar)
    	{
    		nextChar = default;
    		nextChar = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000158")]
    	[Address(RVA = "0x10DDC30", Offset = "0x10DC630", VA = "0x1810DDC30")]
    	public int GetIndent()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000159")]
    	[Address(RVA = "0x10DDDA0", Offset = "0x10DC7A0", VA = "0x1810DDDA0")]
    	public string Extract(int startIndex, int length)
    	{
    		return null;
    	}

    	[Token(Token = "0x600015A")]
    	[Address(RVA = "0x10DDE30", Offset = "0x10DC830", VA = "0x1810DDE30")]
    	public string Extract(Token token)
    	{
    		return null;
    	}

    	[Token(Token = "0x600015B")]
    	[Address(RVA = "0x10DDE40", Offset = "0x10DC840", VA = "0x1810DDE40")]
    	public void Error(string message)
    	{
    	}

    	[Token(Token = "0x600015C")]
    	[Address(RVA = "0x10DDF60", Offset = "0x10DC960", VA = "0x1810DDF60")]
    	public void Error(Token token)
    	{
    	}

    	[Token(Token = "0x600015D")]
    	[Address(RVA = "0x10DE150", Offset = "0x10DCB50", VA = "0x1810DE150")]
    	public void Associate(ILineComponent component, InlineRange range)
    	{
    	}

    	[Token(Token = "0x600015E")]
    	[Address(RVA = "0x10DE230", Offset = "0x10DCC30", VA = "0x1810DE230")]
    	public void Associate(ILineComponent component, Token token)
    	{
    	}

    	[Token(Token = "0x600015F")]
    	[Address(RVA = "0x10DE240", Offset = "0x10DCC40", VA = "0x1810DE240")]
    	public void Identify(MixedValue mixed)
    	{
    	}

    	[Token(Token = "0x6000160")]
    	[Address(RVA = "0x10DE540", Offset = "0x10DCF40", VA = "0x1810DE540")]
    	public bool Next(TokenType types, ErrorType errors, out Token token)
    	{
    		token = default;
    		token = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000161")]
    	[Address(RVA = "0x10DE600", Offset = "0x10DD000", VA = "0x1810DE600")]
    	public bool Next(TokenType types, out Token token)
    	{
    		token = default;
    		token = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000162")]
    	[Address(RVA = "0x10DE690", Offset = "0x10DD090", VA = "0x1810DE690")]
    	public bool Next(ErrorType errors, out Token token)
    	{
    		token = default;
    		token = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000163")]
    	[Address(RVA = "0x10DE760", Offset = "0x10DD160", VA = "0x1810DE760")]
    	public bool Next(out Token token)
    	{
    		token = default;
    		token = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000164")]
    	[Address(RVA = "0x10DE780", Offset = "0x10DD180", VA = "0x1810DE780")]
    	private bool MoveUntilMatch(TokenType? types, ErrorType? errors, out Token result)
    	{
    		result = default;
    		result = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000165")]
    	[Address(RVA = "0x10DEAA0", Offset = "0x10DD4A0", VA = "0x1810DEAA0")]
    	private static bool IsMatch(TokenType? types, ErrorType? errors, Token token)
    	{
    		return default(bool);
    	}
    }
}
