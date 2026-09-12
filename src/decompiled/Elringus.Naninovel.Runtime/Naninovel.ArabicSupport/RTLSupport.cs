using Il2CppDummyDll;

namespace Naninovel.ArabicSupport;

[Token(Token = "0x20004EE")]
public static class RTLSupport
{
	[Token(Token = "0x4001340")]
	public const int DefaultBufferSize = 2048;

	[Token(Token = "0x4001341")]
	[FieldOffset(Offset = "0x0")]
	private static FastStringBuilder inputBuilder;

	[Token(Token = "0x4001342")]
	[FieldOffset(Offset = "0x8")]
	private static FastStringBuilder glyphFixerOutput;

	[Token(Token = "0x600188D")]
	[Address(RVA = "0x609AB0", Offset = "0x6084B0", VA = "0x180609AB0")]
	static RTLSupport()
	{
	}

	[Token(Token = "0x600188E")]
	[Address(RVA = "0x609D20", Offset = "0x608720", VA = "0x180609D20")]
	public static void FixRTL(string input, FastStringBuilder output, bool farsi = true, bool fixTextTags = true, bool preserveNumbers = false)
	{
	}
}
