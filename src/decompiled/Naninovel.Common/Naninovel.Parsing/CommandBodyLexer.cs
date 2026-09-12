using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x200002C")]
internal class CommandBodyLexer
{
	[Token(Token = "0x400004A")]
	[FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private CommandParameterLexer _003CparameterLexer_003EP;

	[Token(Token = "0x400004B")]
	[FieldOffset(Offset = "0x18")]
	[CompilerGenerated]
	private ISyntax _003Cstx_003EP;

	[Token(Token = "0x400004C")]
	[FieldOffset(Offset = "0x20")]
	private LexState state;

	[Token(Token = "0x400004D")]
	[FieldOffset(Offset = "0x28")]
	private int bodyStartIndex;

	[Token(Token = "0x400004E")]
	[FieldOffset(Offset = "0x2C")]
	private bool inlined;

	[Token(Token = "0x60000C4")]
	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
	public CommandBodyLexer(CommandParameterLexer parameterLexer, ISyntax stx)
	{
	}

	[Token(Token = "0x60000C5")]
	[Address(RVA = "0x10D3CC0", Offset = "0x10D26C0", VA = "0x1810D3CC0")]
	public static bool IsInlinedOpening(LexState state, ISyntax stx)
	{
		return default(bool);
	}

	[Token(Token = "0x60000C6")]
	[Address(RVA = "0x10D3D50", Offset = "0x10D2750", VA = "0x1810D3D50")]
	public static bool IsEndReached(LexState state, bool inlined, ISyntax stx)
	{
		return default(bool);
	}

	[Token(Token = "0x60000C7")]
	[Address(RVA = "0x10D3E20", Offset = "0x10D2820", VA = "0x1810D3E20")]
	public static bool IsLast(LexState state, bool inlined, ISyntax stx)
	{
		return default(bool);
	}

	[Token(Token = "0x60000C8")]
	[Address(RVA = "0x10D3F60", Offset = "0x10D2960", VA = "0x1810D3F60")]
	public void AddCommandBody(LexState state, bool inlined)
	{
	}

	[Token(Token = "0x60000C9")]
	[Address(RVA = "0x10D4100", Offset = "0x10D2B00", VA = "0x1810D4100")]
	private void Reset(LexState state, bool inlined)
	{
	}

	[Token(Token = "0x60000CA")]
	[Address(RVA = "0x10D4170", Offset = "0x10D2B70", VA = "0x1810D4170")]
	private void AddIdentifier()
	{
	}

	[Token(Token = "0x60000CB")]
	[Address(RVA = "0x10D4310", Offset = "0x10D2D10", VA = "0x1810D4310")]
	private void AddParameters()
	{
	}

	[Token(Token = "0x60000CC")]
	[Address(RVA = "0x10D4400", Offset = "0x10D2E00", VA = "0x1810D4400")]
	private void AddBody()
	{
	}
}
