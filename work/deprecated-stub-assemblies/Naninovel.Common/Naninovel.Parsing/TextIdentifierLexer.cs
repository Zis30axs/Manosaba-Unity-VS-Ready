using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000034")]
    internal class TextIdentifierLexer
    {
    	[Token(Token = "0x4000075")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private ISyntax _003Cstx_003EP;

    	[Token(Token = "0x4000076")]
    	[FieldOffset(Offset = "0x18")]
    	private LexState state;

    	[Token(Token = "0x4000077")]
    	[FieldOffset(Offset = "0x20")]
    	private int startIndex;

    	[Token(Token = "0x4000078")]
    	[FieldOffset(Offset = "0x24")]
    	private bool hasBody;

    	[Token(Token = "0x6000123")]
    	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    	public TextIdentifierLexer(ISyntax stx)
    	{
    	}

    	[Token(Token = "0x6000124")]
    	[Address(RVA = "0x10D8D50", Offset = "0x10D7750", VA = "0x1810D8D50")]
    	public static bool IsOpening(LexState state, ISyntax stx)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000125")]
    	[Address(RVA = "0x10D8E50", Offset = "0x10D7850", VA = "0x1810D8E50")]
    	public void AddIdentifier(LexState state)
    	{
    	}

    	[Token(Token = "0x6000126")]
    	[Address(RVA = "0x10D5540", Offset = "0x10D3F40", VA = "0x1810D5540")]
    	private void Reset(LexState state)
    	{
    	}

    	[Token(Token = "0x6000127")]
    	[Address(RVA = "0x10D9080", Offset = "0x10D7A80", VA = "0x1810D9080")]
    	private void AddOpening()
    	{
    	}

    	[Token(Token = "0x6000128")]
    	[Address(RVA = "0x10D90E0", Offset = "0x10D7AE0", VA = "0x1810D90E0")]
    	private void AddClosing()
    	{
    	}

    	[Token(Token = "0x6000129")]
    	[Address(RVA = "0x10D9130", Offset = "0x10D7B30", VA = "0x1810D9130")]
    	private void AddBody()
    	{
    	}

    	[Token(Token = "0x600012A")]
    	[Address(RVA = "0x10D9200", Offset = "0x10D7C00", VA = "0x1810D9200")]
    	private void AddIdentifier()
    	{
    	}

    	[Token(Token = "0x600012B")]
    	[Address(RVA = "0x10D9240", Offset = "0x10D7C40", VA = "0x1810D9240")]
    	private void CheckMissingBody()
    	{
    	}

    	[Token(Token = "0x600012C")]
    	[Address(RVA = "0x10D9290", Offset = "0x10D7C90", VA = "0x1810D9290")]
    	private bool IsClosing()
    	{
    		return default(bool);
    	}
    }
}
