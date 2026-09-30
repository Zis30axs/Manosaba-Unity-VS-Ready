using Il2CppDummyDll;

namespace Naninovel.ArabicSupport
{
    [Token(Token = "0x20004F4")]
    public static class GlyphFixer
    {
    	[Token(Token = "0x60018AD")]
    	[Address(RVA = "0x60CAD0", Offset = "0x60B4D0", VA = "0x18060CAD0")]
    	public static void Fix(FastStringBuilder input, FastStringBuilder output, bool preserveNumbers, bool farsi)
    	{
    	}

    	[Token(Token = "0x60018AE")]
    	[Address(RVA = "0x60CEE0", Offset = "0x60B8E0", VA = "0x18060CEE0")]
    	public static void FixYah(FastStringBuilder text, bool farsi)
    	{
    	}

    	[Token(Token = "0x60018AF")]
    	[Address(RVA = "0x60CF70", Offset = "0x60B970", VA = "0x18060CF70")]
    	private static bool HandleSpecialLam(FastStringBuilder input, FastStringBuilder output, int i)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60018B0")]
    	[Address(RVA = "0x60D0B0", Offset = "0x60BAB0", VA = "0x18060D0B0")]
    	public static void FixNumbers(FastStringBuilder text, bool farsi)
    	{
    	}

    	[Token(Token = "0x60018B1")]
    	[Address(RVA = "0x60D3F0", Offset = "0x60BDF0", VA = "0x18060D3F0")]
    	private static bool IsLeadingLetter(FastStringBuilder letters, int index)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60018B2")]
    	[Address(RVA = "0x60D5D0", Offset = "0x60BFD0", VA = "0x18060D5D0")]
    	private static bool IsFinishingLetter(FastStringBuilder letters, int index)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60018B3")]
    	[Address(RVA = "0x60D6E0", Offset = "0x60C0E0", VA = "0x18060D6E0")]
    	private static bool IsMiddleLetter(FastStringBuilder letters, int index)
    	{
    		return default(bool);
    	}
    }
}
