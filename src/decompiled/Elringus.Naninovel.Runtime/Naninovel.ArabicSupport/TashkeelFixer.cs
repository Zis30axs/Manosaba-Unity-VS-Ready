using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.ArabicSupport;

[Token(Token = "0x20004ED")]
public static class TashkeelFixer
{
	[Token(Token = "0x4001333")]
	[FieldOffset(Offset = "0x0")]
	private static readonly List<TashkeelLocation> TashkeelLocations;

	[Token(Token = "0x4001334")]
	[FieldOffset(Offset = "0x8")]
	private static readonly string ShaddaDammatan;

	[Token(Token = "0x4001335")]
	[FieldOffset(Offset = "0x10")]
	private static readonly string ShaddaKasratan;

	[Token(Token = "0x4001336")]
	[FieldOffset(Offset = "0x18")]
	private static readonly string ShaddaSuperscriptAlef;

	[Token(Token = "0x4001337")]
	[FieldOffset(Offset = "0x20")]
	private static readonly string ShaddaFatha;

	[Token(Token = "0x4001338")]
	[FieldOffset(Offset = "0x28")]
	private static readonly string ShaddaDamma;

	[Token(Token = "0x4001339")]
	[FieldOffset(Offset = "0x30")]
	private static readonly string ShaddaKasra;

	[Token(Token = "0x400133A")]
	[FieldOffset(Offset = "0x38")]
	private static readonly string ShaddaWithFathaIsolatedForm;

	[Token(Token = "0x400133B")]
	[FieldOffset(Offset = "0x40")]
	private static readonly string ShaddaWithDammaIsolatedForm;

	[Token(Token = "0x400133C")]
	[FieldOffset(Offset = "0x48")]
	private static readonly string ShaddaWithKasraIsolatedForm;

	[Token(Token = "0x400133D")]
	[FieldOffset(Offset = "0x50")]
	private static readonly string ShaddaWithDammatanIsolatedForm;

	[Token(Token = "0x400133E")]
	[FieldOffset(Offset = "0x58")]
	private static readonly string ShaddaWithKasratanIsolatedForm;

	[Token(Token = "0x400133F")]
	[FieldOffset(Offset = "0x60")]
	private static readonly string ShaddaWithSuperscriptAlefIsolatedForm;

	[Token(Token = "0x600188A")]
	[Address(RVA = "0x608630", Offset = "0x607030", VA = "0x180608630")]
	public static void RemoveTashkeel(FastStringBuilder input)
	{
	}

	[Token(Token = "0x600188B")]
	[Address(RVA = "0x608E20", Offset = "0x607820", VA = "0x180608E20")]
	public static void RestoreTashkeel(FastStringBuilder letters)
	{
	}
}
