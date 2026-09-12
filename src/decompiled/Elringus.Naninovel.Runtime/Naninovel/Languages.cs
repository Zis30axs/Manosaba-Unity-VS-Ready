using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000380")]
public static class Languages
{
	[Token(Token = "0x4000D0C")]
	[FieldOffset(Offset = "0x0")]
	private static readonly Dictionary<string, string> RFC5646_TAGS;

	[Token(Token = "0x1700024D")]
	private static IReadOnlyList<Language> configured
	{
		[Token(Token = "0x60010F3")]
		[Address(RVA = "0x57DA00", Offset = "0x57C400", VA = "0x18057DA00")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60010F4")]
	[Address(RVA = "0x57DAB0", Offset = "0x57C4B0", VA = "0x18057DAB0")]
	public static IReadOnlyDictionary<string, string> GetRFCTagToName()
	{
		return null;
	}

	[Token(Token = "0x60010F5")]
	[Address(RVA = "0x57DB10", Offset = "0x57C510", VA = "0x18057DB10")]
	public static Language[] GetDefault()
	{
		return null;
	}

	[Token(Token = "0x60010F6")]
	[Address(RVA = "0x57DD40", Offset = "0x57C740", VA = "0x18057DD40")]
	public static IReadOnlyList<Language> GetAll()
	{
		return null;
	}

	[Token(Token = "0x60010F7")]
	[Address(RVA = "0x57DD90", Offset = "0x57C790", VA = "0x18057DD90")]
	public static bool ContainsTag(string tag)
	{
		return default(bool);
	}

	[Token(Token = "0x60010F8")]
	[Address(RVA = "0x57E060", Offset = "0x57CA60", VA = "0x18057E060")]
	public static bool ContainsName(string name)
	{
		return default(bool);
	}

	[Token(Token = "0x60010F9")]
	[Address(RVA = "0x57E330", Offset = "0x57CD30", VA = "0x18057E330")]
	public static string GetNameByTag(string tag)
	{
		return null;
	}
}
