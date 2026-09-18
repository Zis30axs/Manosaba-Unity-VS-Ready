using System;
using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to manage the localization activities.
    /// </summary>
    public interface ILocalizationManager : IEngineService<LocalizationConfiguration>
    {
        /// <summary>
        /// Event invoked when the locale is changed.
        /// </summary>
        event Action<LocaleChangedArgs> OnLocaleChanged;

        /// <summary>
        /// Language tag of the currently selected localization.
        /// </summary>
        string SelectedLocale { get; }
        /// <summary>
        /// Language tags of the available localizations.
        /// </summary>
        IReadOnlyCollection<string> AvailableLocales { get; }
        /// <summary>
        /// Providers used to access the localization resources.
        /// </summary>
        IReadOnlyList<IResourceProvider> Providers { get; }

        /// <summary>
        /// Whether localization with the specified language tag is available.
        /// </summary>
        bool LocaleAvailable (string locale);
        /// <summary>
        /// Selects (switches to) localization with the specified language tag.
        /// </summary>
        UniTask SelectLocale (string locale);
        /// <summary>
        /// Adds an async delegate to invoke after changing a locale.
        /// </summary>
        void AddChangeLocaleTask (Func<LocaleChangedArgs, UniTask> handler, int priority = 0);
        /// <summary>
        /// Removes an async delegate to invoke after changing a locale.
        /// </summary>
        void RemoveChangeLocaleTask (Func<LocaleChangedArgs, UniTask> handler);
    }
}
