using System.Collections.Generic;
using System.Text;
using Il2CppDummyDll;

namespace WitchTrials.Models;

[Token(Token = "0x2000165")]
public static class WitchBookDataHelper
{
	[Token(Token = "0x40006C2")]
	[FieldOffset(Offset = "0x0")]
	private static readonly StringBuilder s_builder;

	[Token(Token = "0x60005C9")]
	[Address(RVA = "0x38F610", Offset = "0x38E010", VA = "0x18038F610")]
	public static string BuildClueTextureAddress(string id)
	{
		return null;
	}

	[Token(Token = "0x60005CA")]
	[Address(RVA = "0x38F970", Offset = "0x38E370", VA = "0x18038F970")]
	public static string BuildProfileTextureAddress(string id)
	{
		return null;
	}

	[Token(Token = "0x60005CB")]
	[Address(RVA = "0x38F9C0", Offset = "0x38E3C0", VA = "0x18038F9C0")]
	public static List<WitchBookCategory> ParseCategories(string[] categoryNames)
	{
		return null;
	}
}
