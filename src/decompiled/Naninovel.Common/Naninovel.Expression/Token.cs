using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x200009E")]
internal readonly struct Token
{
	[Token(Token = "0x400020A")]
	[FieldOffset(Offset = "0x0")]
	public readonly TokenType Type;

	[Token(Token = "0x400020B")]
	[FieldOffset(Offset = "0x4")]
	public readonly int Index;

	[Token(Token = "0x400020C")]
	[FieldOffset(Offset = "0x8")]
	public readonly string Content;

	[Token(Token = "0x6000398")]
	[Address(RVA = "0x10FBFE0", Offset = "0x10FA9E0", VA = "0x1810FBFE0")]
	public Token(TokenType type, int index, string content = "")
	{
	}
}
