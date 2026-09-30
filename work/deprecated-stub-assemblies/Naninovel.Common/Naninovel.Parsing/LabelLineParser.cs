using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000039")]
    public class LabelLineParser
    {
    	[Token(Token = "0x4000092")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly LineWalker walker;

    	[Token(Token = "0x4000093")]
    	[FieldOffset(Offset = "0x18")]
    	private PlainText label;

    	[Token(Token = "0x6000150")]
    	[Address(RVA = "0x10DD200", Offset = "0x10DBC00", VA = "0x1810DD200")]
    	public LabelLineParser(ParseOptions options)
    	{
    	}

    	[Token(Token = "0x6000151")]
    	[Address(RVA = "0x10DD360", Offset = "0x10DBD60", VA = "0x1810DD360")]
    	public LabelLine Parse(string lineText, IReadOnlyList<Token> tokens)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000152")]
    	[Address(RVA = "0x10DD770", Offset = "0x10DC170", VA = "0x1810DD770")]
    	private void Reset(string lineText, IReadOnlyList<Token> tokens)
    	{
    	}

    	[Token(Token = "0x6000153")]
    	[Address(RVA = "0x10DD850", Offset = "0x10DC250", VA = "0x1810DD850")]
    	private void ParseLabel(Token labelToken)
    	{
    	}
    }
}
