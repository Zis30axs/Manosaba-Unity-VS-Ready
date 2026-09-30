using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x200003D")]
    internal class MixedValueParser
    {
    	[Token(Token = "0x400009F")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private bool _003Cunwrap_003EP;

    	[Token(Token = "0x40000A0")]
    	[FieldOffset(Offset = "0x18")]
    	[CompilerGenerated]
    	private ISyntax _003Cstx_003EP;

    	[Token(Token = "0x40000A1")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly Utilities utils;

    	[Token(Token = "0x40000A2")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly List<IValueComponent> value;

    	[Token(Token = "0x40000A3")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly Queue<Token> expressions;

    	[Token(Token = "0x40000A4")]
    	[FieldOffset(Offset = "0x38")]
    	private readonly Queue<Token> textIds;

    	[Token(Token = "0x40000A5")]
    	[FieldOffset(Offset = "0x40")]
    	private readonly Queue<Token> textIdBodies;

    	[Token(Token = "0x600016F")]
    	[Address(RVA = "0x10DF5E0", Offset = "0x10DDFE0", VA = "0x1810DF5E0")]
    	public MixedValueParser(bool unwrap, ISyntax stx)
    	{
    	}

    	[Token(Token = "0x6000170")]
    	[Address(RVA = "0x10DF940", Offset = "0x10DE340", VA = "0x1810DF940")]
    	public void AddExpressionToken(Token token)
    	{
    	}

    	[Token(Token = "0x6000171")]
    	[Address(RVA = "0x10DF9B0", Offset = "0x10DE3B0", VA = "0x1810DF9B0")]
    	public void AddTextIdToken(Token token)
    	{
    	}

    	[Token(Token = "0x6000172")]
    	[Address(RVA = "0x10DFA20", Offset = "0x10DE420", VA = "0x1810DFA20")]
    	public void AddTextIdBodyToken(Token token)
    	{
    	}

    	[Token(Token = "0x6000173")]
    	[Address(RVA = "0x10DFA90", Offset = "0x10DE490", VA = "0x1810DFA90")]
    	public void ClearAddedExpressions()
    	{
    	}

    	[Token(Token = "0x6000174")]
    	[Address(RVA = "0x10DFAE0", Offset = "0x10DE4E0", VA = "0x1810DFAE0")]
    	public MixedValue Parse(Token valueToken, LineWalker walker, bool unescapeAuthor)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000175")]
    	[Address(RVA = "0x10DFED0", Offset = "0x10DE8D0", VA = "0x1810DFED0")]
    	private string UnescapePlain(string value, bool wrapped)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000176")]
    	[Address(RVA = "0x10E0180", Offset = "0x10DEB80", VA = "0x1810E0180")]
    	private string UnescapeAuthor(string value)
    	{
    		return null;
    	}
    }
}
