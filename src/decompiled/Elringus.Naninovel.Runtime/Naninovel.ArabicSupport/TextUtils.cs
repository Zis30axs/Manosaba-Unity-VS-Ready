using Il2CppDummyDll;

namespace Naninovel.ArabicSupport;

[Token(Token = "0x20004EB")]
public static class TextUtils
{
	[Token(Token = "0x400132F")]
	private const char UpperCaseA = 'A';

	[Token(Token = "0x4001330")]
	private const char LowerCaseZ = 'z';

	[Token(Token = "0x600187E")]
	[Address(RVA = "0x6082F0", Offset = "0x606CF0", VA = "0x1806082F0")]
	public static bool IsNumber(char ch, bool preserverNumbers, bool farsi)
	{
		return default(bool);
	}

	[Token(Token = "0x600187F")]
	[Address(RVA = "0x608340", Offset = "0x606D40", VA = "0x180608340")]
	public static bool IsEnglishNumber(char ch)
	{
		return default(bool);
	}

	[Token(Token = "0x6001880")]
	[Address(RVA = "0x608360", Offset = "0x606D60", VA = "0x180608360")]
	public static bool IsFarsiNumber(char ch)
	{
		return default(bool);
	}

	[Token(Token = "0x6001881")]
	[Address(RVA = "0x608380", Offset = "0x606D80", VA = "0x180608380")]
	public static bool IsHinduNumber(char ch)
	{
		return default(bool);
	}

	[Token(Token = "0x6001882")]
	[Address(RVA = "0x6083A0", Offset = "0x606DA0", VA = "0x1806083A0")]
	public static bool IsEnglishLetter(char ch)
	{
		return default(bool);
	}

	[Token(Token = "0x6001883")]
	[Address(RVA = "0x6083C0", Offset = "0x606DC0", VA = "0x1806083C0")]
	public static bool IsRTLCharacter(char ch)
	{
		return default(bool);
	}

	[Token(Token = "0x6001884")]
	[Address(RVA = "0x6084F0", Offset = "0x606EF0", VA = "0x1806084F0")]
	public static bool IsRTLInput(string input)
	{
		return default(bool);
	}
}
