using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000033")]
    internal class LexState
    {
    	[Token(Token = "0x4000071")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly Utilities utils;

    	[Token(Token = "0x4000072")]
    	[FieldOffset(Offset = "0x20")]
    	private string text;

    	[Token(Token = "0x4000073")]
    	[FieldOffset(Offset = "0x28")]
    	private int startIndentIndex;

    	[Token(Token = "0x4000074")]
    	[FieldOffset(Offset = "0x30")]
    	private ICollection<Token> tokens;

    	[Token(Token = "0x17000031")]
    	public int Index
    	{
    		[Token(Token = "0x600010D")]
    		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    		[Token(Token = "0x600010E")]
    		[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000032")]
    	public int Length
    	{
    		[Token(Token = "0x600010F")]
    		[Address(RVA = "0x10CEBC0", Offset = "0x10CD5C0", VA = "0x1810CEBC0")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000033")]
    	public int EndIndex
    	{
    		[Token(Token = "0x6000110")]
    		[Address(RVA = "0x10D81D0", Offset = "0x10D6BD0", VA = "0x1810D81D0")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000034")]
    	public bool IsLast
    	{
    		[Token(Token = "0x6000111")]
    		[Address(RVA = "0x10D81F0", Offset = "0x10D6BF0", VA = "0x1810D81F0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000035")]
    	public bool EndReached
    	{
    		[Token(Token = "0x6000112")]
    		[Address(RVA = "0x10D8220", Offset = "0x10D6C20", VA = "0x1810D8220")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000036")]
    	public bool IsSpace
    	{
    		[Token(Token = "0x6000113")]
    		[Address(RVA = "0x10D8250", Offset = "0x10D6C50", VA = "0x1810D8250")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000037")]
    	public bool IsNotSpace
    	{
    		[Token(Token = "0x6000114")]
    		[Address(RVA = "0x10D82D0", Offset = "0x10D6CD0", VA = "0x1810D82D0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000038")]
    	public bool IsPreviousSpace
    	{
    		[Token(Token = "0x6000115")]
    		[Address(RVA = "0x10D8350", Offset = "0x10D6D50", VA = "0x1810D8350")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000039")]
    	public bool IsNextSpace
    	{
    		[Token(Token = "0x6000116")]
    		[Address(RVA = "0x10D83C0", Offset = "0x10D6DC0", VA = "0x1810D83C0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x600010C")]
    	[Address(RVA = "0x10D7FF0", Offset = "0x10D69F0", VA = "0x1810D7FF0")]
    	public LexState(ISyntax stx)
    	{
    	}

    	[Token(Token = "0x6000117")]
    	[Address(RVA = "0x10D8430", Offset = "0x10D6E30", VA = "0x1810D8430")]
    	public void Reset(string text, ICollection<Token> tokens)
    	{
    	}

    	[Token(Token = "0x6000118")]
    	[Address(RVA = "0x10D8670", Offset = "0x10D7070", VA = "0x1810D8670")]
    	public int Move()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000119")]
    	[Address(RVA = "0x10D8680", Offset = "0x10D7080", VA = "0x1810D8680")]
    	public void AddToken(TokenType type, int startIndex, int length)
    	{
    	}

    	[Token(Token = "0x600011A")]
    	[Address(RVA = "0x10D87B0", Offset = "0x10D71B0", VA = "0x1810D87B0")]
    	public void AddError(ErrorType type, int startIndex, int length)
    	{
    	}

    	[Token(Token = "0x600011B")]
    	[Address(RVA = "0x10D88F0", Offset = "0x10D72F0", VA = "0x1810D88F0")]
    	public void SkipSpace()
    	{
    	}

    	[Token(Token = "0x600011C")]
    	[Address(RVA = "0x10D8990", Offset = "0x10D7390", VA = "0x1810D8990")]
    	public void SkipIndent()
    	{
    	}

    	[Token(Token = "0x600011D")]
    	[Address(RVA = "0x10D8AE0", Offset = "0x10D74E0", VA = "0x1810D8AE0")]
    	public bool Is(char @char)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600011E")]
    	[Address(RVA = "0x10D8B40", Offset = "0x10D7540", VA = "0x1810D8B40")]
    	public bool IsPrevious(char @char)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600011F")]
    	[Address(RVA = "0x10D8BA0", Offset = "0x10D75A0", VA = "0x1810D8BA0")]
    	public bool IsNext(char @char)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000120")]
    	[Address(RVA = "0x10D8C00", Offset = "0x10D7600", VA = "0x1810D8C00")]
    	public bool IsUnescaped(char @char)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000121")]
    	[Address(RVA = "0x10D8C90", Offset = "0x10D7690", VA = "0x1810D8C90")]
    	public bool IsNextUnescaped(char @char)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000122")]
    	[Address(RVA = "0x10D8D20", Offset = "0x10D7720", VA = "0x1810D8D20")]
    	private bool IsIndexValid(int index)
    	{
    		return default(bool);
    	}
    }
}
