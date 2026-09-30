using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000037")]
    public class CommentLineParser
    {
    	[Token(Token = "0x4000088")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly LineWalker walker;

    	[Token(Token = "0x4000089")]
    	[FieldOffset(Offset = "0x18")]
    	private PlainText comment;

    	[Token(Token = "0x6000140")]
    	[Address(RVA = "0x10DB340", Offset = "0x10D9D40", VA = "0x1810DB340")]
    	public CommentLineParser(ParseOptions options)
    	{
    	}

    	[Token(Token = "0x6000141")]
    	[Address(RVA = "0x10DB4A0", Offset = "0x10D9EA0", VA = "0x1810DB4A0")]
    	public CommentLine Parse(string lineText, IReadOnlyList<Token> tokens)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000142")]
    	[Address(RVA = "0x10DB840", Offset = "0x10DA240", VA = "0x1810DB840")]
    	private void Reset(string lineText, IReadOnlyList<Token> tokens)
    	{
    	}

    	[Token(Token = "0x6000143")]
    	[Address(RVA = "0x10DB920", Offset = "0x10DA320", VA = "0x1810DB920")]
    	private void ParseComment(Token commentToken)
    	{
    	}
    }
}
