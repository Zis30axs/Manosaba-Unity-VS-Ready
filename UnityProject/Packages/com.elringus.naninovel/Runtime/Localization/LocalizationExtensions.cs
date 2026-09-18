namespace Naninovel
{
    /// <summary>
    /// Provides extension methods for <see cref="ILocalizationManager"/>.
    /// </summary>
    public static class LocalizationExtensions
    {
        /// <summary>
        /// Whether specified locale equals <see cref="LocalizationConfiguration.SourceLocale"/>.
        /// </summary>
        public static bool IsSourceLocale (this ILocalizationManager manager, string locale)
        {
            return locale == manager.Configuration.SourceLocale;
        }

        /// <summary>
        /// Whether <see cref="LocalizationConfiguration.SourceLocale"/> is currently selected.
        /// </summary>
        public static bool IsSourceLocaleSelected (this ILocalizationManager manager)
        {
            return manager.IsSourceLocale(manager.SelectedLocale);
        }
    }
}
