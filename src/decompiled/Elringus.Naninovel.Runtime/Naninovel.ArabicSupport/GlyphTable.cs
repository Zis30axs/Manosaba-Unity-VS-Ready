using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.ArabicSupport;

[Token(Token = "0x20004F2")]
public static class GlyphTable
{
	[Token(Token = "0x4001348")]
	[FieldOffset(Offset = "0x0")]
	private static readonly Dictionary<char, char> MapList;

	[Token(Token = "0x6001895")]
	[Address(RVA = "0x60B7B0", Offset = "0x60A1B0", VA = "0x18060B7B0")]
	static GlyphTable()
	{
	}

	[Token(Token = "0x6001896")]
	[Address(RVA = "0x60BBB0", Offset = "0x60A5B0", VA = "0x18060BBB0")]
	public static char Convert(char toBeConverted)
	{
		return default(char);
	}
}
