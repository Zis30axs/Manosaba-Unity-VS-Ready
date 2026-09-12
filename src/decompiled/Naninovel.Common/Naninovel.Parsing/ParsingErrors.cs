using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000042")]
public static class ParsingErrors
{
	[Token(Token = "0x40000B4")]
	public const string MissingLineId = "Line identifier is missing.";

	[Token(Token = "0x40000B5")]
	public const string MissingCommandTokens = "Failed to find both command ID and the related error tokens.";
}
