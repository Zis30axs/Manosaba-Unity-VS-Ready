using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x200002D")]
internal class CommandParameterLexer
{
	[Token(Token = "0x400004F")]
	[FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private ExpressionLexer _003CexpressionLexer_003EP;

	[Token(Token = "0x4000050")]
	[FieldOffset(Offset = "0x18")]
	[CompilerGenerated]
	private TextIdentifierLexer _003CtextIdLexer_003EP;

	[Token(Token = "0x4000051")]
	[FieldOffset(Offset = "0x20")]
	[CompilerGenerated]
	private ISyntax _003Cstx_003EP;

	[Token(Token = "0x4000052")]
	[FieldOffset(Offset = "0x28")]
	private LexState state;

	[Token(Token = "0x4000053")]
	[FieldOffset(Offset = "0x30")]
	private int startIndex;

	[Token(Token = "0x4000054")]
	[FieldOffset(Offset = "0x34")]
	private int idEndIndex;

	[Token(Token = "0x4000055")]
	[FieldOffset(Offset = "0x38")]
	private bool inlined;

	[Token(Token = "0x4000056")]
	[FieldOffset(Offset = "0x39")]
	private bool quoted;

	[Token(Token = "0x4000057")]
	[FieldOffset(Offset = "0x3A")]
	private bool addedNameless;

	[Token(Token = "0x4000058")]
	[FieldOffset(Offset = "0x3C")]
	private int lastBoolFlagIndex;

	[Token(Token = "0x4000059")]
	[FieldOffset(Offset = "0x40")]
	private bool multipleBoolFlags;

	[Token(Token = "0x60000CF")]
	[Address(RVA = "0x38F3B0", Offset = "0x38DDB0", VA = "0x18038F3B0")]
	public CommandParameterLexer(ExpressionLexer expressionLexer, TextIdentifierLexer textIdLexer, ISyntax stx)
	{
	}

	[Token(Token = "0x60000D0")]
	[Address(RVA = "0x10D4570", Offset = "0x10D2F70", VA = "0x1810D4570")]
	public void AddParameters(LexState state, bool inlined)
	{
	}

	[Token(Token = "0x60000D1")]
	[Address(RVA = "0x10D47A0", Offset = "0x10D31A0", VA = "0x1810D47A0")]
	private void Reset(LexState state, bool inlined)
	{
	}

	[Token(Token = "0x60000D2")]
	[Address(RVA = "0x10D4830", Offset = "0x10D3230", VA = "0x1810D4830")]
	private void ResetParameterState()
	{
	}

	[Token(Token = "0x60000D3")]
	[Address(RVA = "0x10D4870", Offset = "0x10D3270", VA = "0x1810D4870")]
	private bool TryAddBoolFlag()
	{
		return default(bool);
	}

	[Token(Token = "0x60000D4")]
	[Address(RVA = "0x10D4AF0", Offset = "0x10D34F0", VA = "0x1810D4AF0")]
	private bool TryToggleQuoted()
	{
		return default(bool);
	}

	[Token(Token = "0x60000D5")]
	[Address(RVA = "0x10D4B40", Offset = "0x10D3540", VA = "0x1810D4B40")]
	private bool TryAddExpression()
	{
		return default(bool);
	}

	[Token(Token = "0x60000D6")]
	[Address(RVA = "0x10D4C00", Offset = "0x10D3600", VA = "0x1810D4C00")]
	private bool TryAddTextId()
	{
		return default(bool);
	}

	[Token(Token = "0x60000D7")]
	[Address(RVA = "0x10D4C50", Offset = "0x10D3650", VA = "0x1810D4C50")]
	private bool TryAddIdentifier()
	{
		return default(bool);
	}

	[Token(Token = "0x60000D8")]
	[Address(RVA = "0x10D4DA0", Offset = "0x10D37A0", VA = "0x1810D4DA0")]
	private bool TryAddValue()
	{
		return default(bool);
	}

	[Token(Token = "0x60000D9")]
	[Address(RVA = "0x10D4E30", Offset = "0x10D3830", VA = "0x1810D4E30")]
	private void AddValue()
	{
	}

	[Token(Token = "0x60000DA")]
	[Address(RVA = "0x10D4FE0", Offset = "0x10D39E0", VA = "0x1810D4FE0")]
	private bool WasIdentifierAdded()
	{
		return default(bool);
	}

	[Token(Token = "0x60000DB")]
	[Address(RVA = "0x10D4FF0", Offset = "0x10D39F0", VA = "0x1810D4FF0")]
	private bool WasBoolFlagAdded()
	{
		return default(bool);
	}

	[Token(Token = "0x60000DC")]
	[Address(RVA = "0x10D5000", Offset = "0x10D3A00", VA = "0x1810D5000")]
	private void AddFinalValue()
	{
	}
}
