using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000050")]
public class LabelLine : IScriptLine
{
	[Token(Token = "0x1700004C")]
	public PlainText Label
	{
		[Token(Token = "0x60001B0")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700004D")]
	public int Indent
	{
		[Token(Token = "0x60001B1")]
		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x60001AF")]
	[Address(RVA = "0x3A2CB0", Offset = "0x3A16B0", VA = "0x1803A2CB0")]
	public LabelLine(PlainText label, int indent = 0)
	{
	}

	[Token(Token = "0x60001B2")]
	[Address(RVA = "0x10E2E30", Offset = "0x10E1830", VA = "0x1810E2E30", Slot = "3")]
	public override string ToString()
	{
		return null;
	}
}
