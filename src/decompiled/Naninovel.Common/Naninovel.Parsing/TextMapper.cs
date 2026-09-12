using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000029")]
public class TextMapper : ITextIdentifier
{
	[Token(Token = "0x4000047")]
	[FieldOffset(Offset = "0x10")]
	private readonly Dictionary<string, string> map;

	[Token(Token = "0x1700001B")]
	public IReadOnlyDictionary<string, string> Map
	{
		[Token(Token = "0x60000A8")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000A9")]
	[Address(RVA = "0x10D3900", Offset = "0x10D2300", VA = "0x1810D3900", Slot = "4")]
	public void Identify(string id, string text)
	{
	}

	[Token(Token = "0x60000AA")]
	[Address(RVA = "0x10D3970", Offset = "0x10D2370", VA = "0x1810D3970")]
	public void Clear()
	{
	}

	[Token(Token = "0x60000AB")]
	[Address(RVA = "0x10D39C0", Offset = "0x10D23C0", VA = "0x1810D39C0")]
	public TextMapper()
	{
	}
}
