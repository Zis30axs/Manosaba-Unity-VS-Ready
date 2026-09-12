using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000046")]
public class CommentLine : IScriptLine
{
	[Token(Token = "0x17000040")]
	public PlainText Comment
	{
		[Token(Token = "0x6000197")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000041")]
	public int Indent
	{
		[Token(Token = "0x6000198")]
		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x6000196")]
	[Address(RVA = "0x3A2CB0", Offset = "0x3A16B0", VA = "0x1803A2CB0")]
	public CommentLine(PlainText comment, int indent = 0)
	{
	}

	[Token(Token = "0x6000199")]
	[Address(RVA = "0x10E2610", Offset = "0x10E1010", VA = "0x1810E2610", Slot = "3")]
	public override string ToString()
	{
		return null;
	}
}
