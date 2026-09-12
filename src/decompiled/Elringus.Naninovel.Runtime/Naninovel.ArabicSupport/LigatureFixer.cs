using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.ArabicSupport;

[Token(Token = "0x20004F1")]
public static class LigatureFixer
{
	[Token(Token = "0x4001347")]
	[FieldOffset(Offset = "0x0")]
	private static readonly List<char> LtrTextHolder;

	[Token(Token = "0x6001893")]
	[Address(RVA = "0x60A790", Offset = "0x609190", VA = "0x18060A790")]
	public static void Fix(FastStringBuilder input, FastStringBuilder output, bool farsi, bool fixTextTags, bool preserveNumbers)
	{
	}
}
