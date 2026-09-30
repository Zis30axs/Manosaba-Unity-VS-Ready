using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000030")]
    internal class GenericLineLexer
    {
    	[Token(Token = "0x400005F")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private ExpressionLexer _003CexpressionLexer_003EP;

    	[Token(Token = "0x4000060")]
    	[FieldOffset(Offset = "0x18")]
    	[CompilerGenerated]
    	private CommandBodyLexer _003CcommandLexer_003EP;

    	[Token(Token = "0x4000061")]
    	[FieldOffset(Offset = "0x20")]
    	[CompilerGenerated]
    	private TextIdentifierLexer _003CtextIdLexer_003EP;

    	[Token(Token = "0x4000062")]
    	[FieldOffset(Offset = "0x28")]
    	[CompilerGenerated]
    	private ISyntax _003Cstx_003EP;

    	[Token(Token = "0x4000063")]
    	[FieldOffset(Offset = "0x30")]
    	private LexState state;

    	[Token(Token = "0x4000064")]
    	[FieldOffset(Offset = "0x38")]
    	private bool authorAdded;

    	[Token(Token = "0x4000065")]
    	[FieldOffset(Offset = "0x39")]
    	private bool canAddAuthor;

    	[Token(Token = "0x4000066")]
    	[FieldOffset(Offset = "0x3A")]
    	private bool expressionAdded;

    	[Token(Token = "0x4000067")]
    	[FieldOffset(Offset = "0x3C")]
    	private int startIndex;

    	[Token(Token = "0x4000068")]
    	[FieldOffset(Offset = "0x40")]
    	private int lastTextStart;

    	[Token(Token = "0x4000069")]
    	[FieldOffset(Offset = "0x44")]
    	private int lastNotSpace;

    	[Token(Token = "0x400006A")]
    	[FieldOffset(Offset = "0x48")]
    	private int firstAppearance;

    	[Token(Token = "0x1700002F")]
    	private bool hasAppearance
    	{
    		[Token(Token = "0x60000EF")]
    		[Address(RVA = "0x10D58C0", Offset = "0x10D42C0", VA = "0x1810D58C0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x60000EE")]
    	[Address(RVA = "0x10A4880", Offset = "0x10A3280", VA = "0x1810A4880")]
    	public GenericLineLexer(ExpressionLexer expressionLexer, CommandBodyLexer commandLexer, TextIdentifierLexer textIdLexer, ISyntax stx)
    	{
    	}

    	[Token(Token = "0x60000F0")]
    	[Address(RVA = "0x10D58D0", Offset = "0x10D42D0", VA = "0x1810D58D0")]
    	public LineType AddGenericLine(LexState state)
    	{
    		return default(LineType);
    	}

    	[Token(Token = "0x60000F1")]
    	[Address(RVA = "0x10D5C20", Offset = "0x10D4620", VA = "0x1810D5C20")]
    	private void Reset(LexState state)
    	{
    	}

    	[Token(Token = "0x60000F2")]
    	[Address(RVA = "0x10D5CB0", Offset = "0x10D46B0", VA = "0x1810D5CB0")]
    	private bool TryAddAuthorPrefix()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000F3")]
    	[Address(RVA = "0x10D5EE0", Offset = "0x10D48E0", VA = "0x1810D5EE0")]
    	private bool TryAddInlinedCommand()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000F4")]
    	[Address(RVA = "0x10D6120", Offset = "0x10D4B20", VA = "0x1810D6120")]
    	private bool TryAddExpression()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000F5")]
    	[Address(RVA = "0x10D6200", Offset = "0x10D4C00", VA = "0x1810D6200")]
    	private bool TryAddTextId()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000F6")]
    	[Address(RVA = "0x10D6260", Offset = "0x10D4C60", VA = "0x1810D6260")]
    	private void Move()
    	{
    	}

    	[Token(Token = "0x60000F7")]
    	[Address(RVA = "0x10D63F0", Offset = "0x10D4DF0", VA = "0x1810D63F0")]
    	private void AddPrecedingText()
    	{
    	}
    }
}
