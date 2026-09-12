using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000388")]
[EditInProjectSettings]
public class LocalizationConfiguration : Configuration
{
	[Token(Token = "0x4000D1F")]
	public const string DefaultLocalizationPathPrefix = "Localization";

	[Token(Token = "0x4000D20")]
	[FieldOffset(Offset = "0x18")]
	[Tooltip("Configuration of the resource loader used with the localization resources.")]
	public ResourceLoaderConfiguration Loader;

	[Token(Token = "0x4000D21")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("RFC5646 language tags mapped to default language display names. Restart Unity editor for changes to take effect.")]
	public Language[] Languages;

	[Token(Token = "0x4000D22")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("Locale of the source project resources (language in which the project assets are being authored).")]
	public string SourceLocale;

	[Token(Token = "0x4000D23")]
	[FieldOffset(Offset = "0x30")]
	[Tooltip("Locale selected by default when running the game for the first time. Will select `Source Locale` when not specified.")]
	public string DefaultLocale;

	[Token(Token = "0x4000D24")]
	[FieldOffset(Offset = "0x38")]
	[Tooltip("When enabled and the game is running for the first time, attempts to automatically detect locale based on system language. When succeeds and the locale is supported by the game, selects it; otherwise falls back to 'Default Locale'.")]
	public bool AutoDetectLocale;

	[Token(Token = "0x4000D25")]
	[FieldOffset(Offset = "0x40")]
	[Header("Localization Documents")]
	[Tooltip("Text character to join common localized script records, such as parts of generic text lines and localizable parameter values.")]
	public string RecordSeparator;

	[Token(Token = "0x4000D26")]
	[FieldOffset(Offset = "0x48")]
	[Tooltip("Text character to insert before annotation lines to distinguish them for the localized text. Annotations are comments optionally added to the generated localization documents to provide additional context for the translators, such as author of the printed text messages, inlined commands and command lines containing localized parameters. Stub character is used to replace localized parts of such annotations, as they're duplicated on the next comment line containing the text to localize.")]
	public string AnnotationPrefix;

	[Token(Token = "0x600112C")]
	[Address(RVA = "0x585560", Offset = "0x583F60", VA = "0x180585560")]
	public LocalizationConfiguration()
	{
	}
}
