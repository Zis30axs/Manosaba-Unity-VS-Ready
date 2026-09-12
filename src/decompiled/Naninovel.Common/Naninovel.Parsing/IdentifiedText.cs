using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x200004A")]
public class IdentifiedText : IValueComponent, ILineComponent
{
	[Token(Token = "0x17000048")]
	public PlainText Text
	{
		[Token(Token = "0x60001A8")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000049")]
	public TextIdentifier Id
	{
		[Token(Token = "0x60001A9")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60001A7")]
	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
	public IdentifiedText(PlainText text, TextIdentifier id)
	{
	}

	[Token(Token = "0x60001AA")]
	[Address(RVA = "0x10E2CC0", Offset = "0x10E16C0", VA = "0x1810E2CC0", Slot = "3")]
	public override string ToString()
	{
		return null;
	}
}
