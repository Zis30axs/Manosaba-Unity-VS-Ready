#nullable enable annotations
﻿using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000043")]
    public class ScriptParser
    {
    	[Token(Token = "0x40000B6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly Lexer lexer;

    	[Token(Token = "0x40000B7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly List<Token> tokens;

    	[Token(Token = "0x40000B8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly CommandLineParser commandParser;

    	[Token(Token = "0x40000B9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private readonly CommentLineParser commentParser;

    	[Token(Token = "0x40000BA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private readonly GenericLineParser genericParser;

    	[Token(Token = "0x40000BB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private readonly LabelLineParser labelParser;

    	[Token(Token = "0x600018A")]
    	[Address(RVA = "0x10E1760", Offset = "0x10E0160", VA = "0x1810E1760")]
    	public ScriptParser([Optional] ParseOptions? options)
    	{
    	}

    	[Token(Token = "0x600018B")]
    	[Address(RVA = "0x10E1D20", Offset = "0x10E0720", VA = "0x1810E1D20")]
    	public static string[] SplitText(string? scriptText)
    	{
    		return null;
    	}

    	[Token(Token = "0x600018C")]
    	[Address(RVA = "0x10E1E40", Offset = "0x10E0840", VA = "0x1810E1E40")]
    	public List<IScriptLine> ParseText(string scriptText)
    	{
    		return null;
    	}

    	[Token(Token = "0x600018D")]
    	[Address(RVA = "0x10E1F50", Offset = "0x10E0950", VA = "0x1810E1F50")]
    	public IScriptLine ParseLine(string lineText)
    	{
    		return null;
    	}
    }
}
