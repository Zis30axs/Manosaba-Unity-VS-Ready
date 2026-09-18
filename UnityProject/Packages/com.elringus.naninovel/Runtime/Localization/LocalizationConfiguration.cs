using UnityEngine;

namespace Naninovel
{
    [EditInProjectSettings]
    public class LocalizationConfiguration : Configuration
    {
        public const string DefaultLocalizationPathPrefix = "Localization";

        [Tooltip("Configuration of the resource loader used with the localization resources.")]
        public ResourceLoaderConfiguration Loader = new() { PathPrefix = DefaultLocalizationPathPrefix };
        [Tooltip("RFC5646 language tags mapped to default language display names. Restart Unity editor for changes to take effect.")]
        public Language[] Languages = Naninovel.Languages.GetDefault();
        [Tooltip("Locale of the source project resources (language in which the project assets are being authored).")]
        public string SourceLocale = "en";
        [Tooltip("Locale selected by default when running the game for the first time. Will select `Source Locale` when not specified.")]
        public string DefaultLocale;
        [Tooltip("When enabled and the game is running for the first time, attempts to automatically detect locale based on system language. When succeeds and the locale is supported by the game, selects it; otherwise falls back to 'Default Locale'.")]
        public bool AutoDetectLocale = true;

        [Header("Localization Documents")]
        [Tooltip("Text character to join common localized script records, such as parts of generic text lines and localizable parameter values.")]
        public string RecordSeparator = "|";
        [Tooltip("Text character to insert before annotation lines to distinguish them for the localized text. Annotations are comments optionally added to the generated localization documents to provide additional context for the translators, such as author of the printed text messages, inlined commands and command lines containing localized parameters. Stub character is used to replace localized parts of such annotations, as they're duplicated on the next comment line containing the text to localize.")]
        public string AnnotationPrefix = "> ";
    }
}
