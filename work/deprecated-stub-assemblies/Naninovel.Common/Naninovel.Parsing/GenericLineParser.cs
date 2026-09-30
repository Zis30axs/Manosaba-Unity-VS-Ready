#nullable enable annotations
﻿using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000038")]
    public class GenericLineParser
    {
    	[Token(Token = "0x400008A")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly CommandParser commandParser;

    	[Token(Token = "0x400008B")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly MixedValueParser valueParser;

    	[Token(Token = "0x400008C")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly List<IGenericContent> content;

    	[Token(Token = "0x400008D")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly LineWalker walker;

    	[Token(Token = "0x400008E")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly ISyntax stx;

    	[Token(Token = "0x400008F")]
    	[FieldOffset(Offset = "0x38")]
    	private PlainText? authorId;

    	[Token(Token = "0x4000090")]
    	[FieldOffset(Offset = "0x40")]
    	private PlainText? authorAppearance;

    	[Token(Token = "0x4000091")]
    	[FieldOffset(Offset = "0x48")]
    	private GenericPrefix? prefix;

    	[Token(Token = "0x6000144")]
    	[Address(RVA = "0x10DBA30", Offset = "0x10DA430", VA = "0x1810DBA30")]
    	public GenericLineParser(ParseOptions options)
    	{
    	}

    	[Token(Token = "0x6000145")]
    	[Address(RVA = "0x10DBD00", Offset = "0x10DA700", VA = "0x1810DBD00")]
    	public GenericLine Parse(string lineText, IReadOnlyList<Token> tokens)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000146")]
    	[Address(RVA = "0x10DBDF0", Offset = "0x10DA7F0", VA = "0x1810DBDF0")]
    	private void Reset(string lineText, IReadOnlyList<Token> tokens)
    	{
    	}

    	[Token(Token = "0x6000147")]
    	[Address(RVA = "0x10DBFA0", Offset = "0x10DA9A0", VA = "0x1810DBFA0")]
    	private bool TryNext()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000148")]
    	[Address(RVA = "0x10DC850", Offset = "0x10DB250", VA = "0x1810DC850")]
    	private void ParseAuthorId(Token authorIdToken)
    	{
    	}

    	[Token(Token = "0x6000149")]
    	[Address(RVA = "0x10DC9A0", Offset = "0x10DB3A0", VA = "0x1810DC9A0")]
    	private void ParseAuthorAppearance(Token authorAppearanceToken)
    	{
    	}

    	[Token(Token = "0x600014A")]
    	[Address(RVA = "0x10DCAF0", Offset = "0x10DB4F0", VA = "0x1810DCAF0")]
    	private void ParsePrefix(Token authorAssignToken)
    	{
    	}

    	[Token(Token = "0x600014B")]
    	[Address(RVA = "0x10DCD30", Offset = "0x10DB730", VA = "0x1810DCD30")]
    	private void ParseGenericText(Token textToken)
    	{
    	}

    	[Token(Token = "0x600014C")]
    	[Address(RVA = "0x10DCE20", Offset = "0x10DB820", VA = "0x1810DCE20")]
    	private bool IsEmptyInlined(Token openInlinedToken)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600014D")]
    	[Address(RVA = "0x10DCEE0", Offset = "0x10DB8E0", VA = "0x1810DCEE0")]
    	private void ParseInlined()
    	{
    	}

    	[Token(Token = "0x600014E")]
    	[Address(RVA = "0x10DCFF0", Offset = "0x10DB9F0", VA = "0x1810DCFF0")]
    	private void ParseEmptyInlined()
    	{
    	}

    	[Token(Token = "0x600014F")]
    	[Address(RVA = "0x10DD130", Offset = "0x10DBB30", VA = "0x1810DD130")]
    	private void AssociateInlined(Token inlinedToken)
    	{
    	}
    }
}
