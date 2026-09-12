using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel.ManagedText;

namespace Naninovel;

[Token(Token = "0x20003B2")]
public static class TextManagerExtensions
{
	[Token(Token = "0x60011DE")]
	[Address(RVA = "0x595D60", Offset = "0x594760", VA = "0x180595D60")]
	public static ManagedTextDocument GetDocumentOrErr(this ITextManager manager, string documentPath)
	{
		return null;
	}

	[Token(Token = "0x60011DF")]
	[Address(RVA = "0x595E30", Offset = "0x594830", VA = "0x180595E30")]
	public static ManagedTextRecord? GetRecord(this ITextManager manager, string key, string documentPath)
	{
		return null;
	}

	[Token(Token = "0x60011E0")]
	[Address(RVA = "0x595F80", Offset = "0x594980", VA = "0x180595F80")]
	public static bool TryGetRecord(this ITextManager manager, string key, string documentPath, out ManagedTextRecord record)
	{
		record = default;
		record = default;
		return default(bool);
	}

	[Token(Token = "0x60011E1")]
	[Address(RVA = "0x596010", Offset = "0x594A10", VA = "0x180596010")]
	[CanBeNull]
	public static string GetRecordValue(this ITextManager manager, string key, string documentPath)
	{
		return null;
	}
}
