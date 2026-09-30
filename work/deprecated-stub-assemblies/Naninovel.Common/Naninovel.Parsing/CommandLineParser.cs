using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000035")]
    public class CommandLineParser
    {
    	[Token(Token = "0x4000079")]
    	[FieldOffset(Offset = "0x0")]
    	private static readonly Command emptyBody;

    	[Token(Token = "0x400007A")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly CommandParser commandParser;

    	[Token(Token = "0x400007B")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly LineWalker walker;

    	[Token(Token = "0x400007C")]
    	[FieldOffset(Offset = "0x20")]
    	private Command command;

    	[Token(Token = "0x600012F")]
    	[Address(RVA = "0x10D93B0", Offset = "0x10D7DB0", VA = "0x1810D93B0")]
    	public CommandLineParser(ParseOptions options)
    	{
    	}

    	[Token(Token = "0x6000130")]
    	[Address(RVA = "0x10D95B0", Offset = "0x10D7FB0", VA = "0x1810D95B0")]
    	public CommandLine Parse(string lineText, IReadOnlyList<Token> tokens)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000131")]
    	[Address(RVA = "0x10D9890", Offset = "0x10D8290", VA = "0x1810D9890")]
    	private void Reset(string lineText, IReadOnlyList<Token> tokens)
    	{
    	}
    }
}
