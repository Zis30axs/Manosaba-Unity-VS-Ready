using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x200002F")]
    internal class ExpressionLexer
    {
    	[Token(Token = "0x400005B")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private ISyntax _003Cstx_003EP;

    	[Token(Token = "0x400005C")]
    	[FieldOffset(Offset = "0x18")]
    	private LexState state;

    	[Token(Token = "0x400005D")]
    	[FieldOffset(Offset = "0x20")]
    	private int startIndex;

    	[Token(Token = "0x400005E")]
    	[FieldOffset(Offset = "0x24")]
    	private bool hasBody;

    	[Token(Token = "0x60000E2")]
    	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    	public ExpressionLexer(ISyntax stx)
    	{
    	}

    	[Token(Token = "0x60000E3")]
    	[Address(RVA = "0x10D52B0", Offset = "0x10D3CB0", VA = "0x1810D52B0")]
    	public static bool IsOpening(LexState state, ISyntax stx)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000E4")]
    	[Address(RVA = "0x10D5340", Offset = "0x10D3D40", VA = "0x1810D5340")]
    	public void AddExpression(LexState state)
    	{
    	}

    	[Token(Token = "0x60000E5")]
    	[Address(RVA = "0x10D5540", Offset = "0x10D3F40", VA = "0x1810D5540")]
    	private void Reset(LexState state)
    	{
    	}

    	[Token(Token = "0x60000E6")]
    	[Address(RVA = "0x10D55B0", Offset = "0x10D3FB0", VA = "0x1810D55B0")]
    	private void AddOpening()
    	{
    	}

    	[Token(Token = "0x60000E7")]
    	[Address(RVA = "0x10D5600", Offset = "0x10D4000", VA = "0x1810D5600")]
    	private void AddBody()
    	{
    	}

    	[Token(Token = "0x60000E8")]
    	[Address(RVA = "0x10D56C0", Offset = "0x10D40C0", VA = "0x1810D56C0")]
    	private bool IsClosing()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000E9")]
    	[Address(RVA = "0x10D5750", Offset = "0x10D4150", VA = "0x1810D5750")]
    	private void AddClosing()
    	{
    	}

    	[Token(Token = "0x60000EA")]
    	[Address(RVA = "0x10D57B0", Offset = "0x10D41B0", VA = "0x1810D57B0")]
    	private void AddExpression()
    	{
    	}

    	[Token(Token = "0x60000EB")]
    	[Address(RVA = "0x10D57F0", Offset = "0x10D41F0", VA = "0x1810D57F0")]
    	private void CheckMissingBody()
    	{
    	}
    }
}
