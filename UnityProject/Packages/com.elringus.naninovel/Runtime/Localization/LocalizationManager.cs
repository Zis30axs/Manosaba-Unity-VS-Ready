using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="ILocalizationManager"/>
    [InitializeAtRuntime]
    public class LocalizationManager : IStatefulService<SettingsStateMap>, ILocalizationManager
    {
        [Serializable]
        public class Settings
        {
            public string SelectedLocale;
        }

        private readonly struct OnChangeTask
        {
            public readonly Func<LocaleChangedArgs, UniTask> Handler;
            public readonly int Priority;

            public OnChangeTask (Func<LocaleChangedArgs, UniTask> handler, int priority)
            {
                Handler = handler;
                Priority = priority;
            }
        }

        public event Action<LocaleChangedArgs> OnLocaleChanged;

        public virtual LocalizationConfiguration Configuration { get; }
        public virtual string SelectedLocale { get; private set; }
        IReadOnlyCollection<string> ILocalizationManager.AvailableLocales => AvailableLocales;
        IReadOnlyList<IResourceProvider> ILocalizationManager.Providers => Providers;

        protected virtual List<string> AvailableLocales { get; } = new();
        protected virtual List<IResourceProvider> Providers { get; } = new();

        private readonly IResourceProviderManager resources;
        private readonly List<OnChangeTask> changeTasks = new();

        public LocalizationManager (LocalizationConfiguration cfg, IResourceProviderManager resources)
        {
            Configuration = cfg;
            this.resources = resources;
        }

        public virtual async UniTask InitializeService ()
        {
            resources.GetProviders(Providers, Configuration.Loader.ProviderTypes);
            await RetrieveAvailableLocales();
        }

        public virtual void ResetService () { }

        public virtual void DestroyService () { }

        public virtual void SaveServiceState (SettingsStateMap stateMap)
        {
            var settings = new Settings {
                SelectedLocale = SelectedLocale
            };
            stateMap.SetState(settings);
        }

        public virtual async UniTask LoadServiceState (SettingsStateMap stateMap)
        {
            var locale = stateMap.GetState<Settings>()?.SelectedLocale;
            if (string.IsNullOrWhiteSpace(locale)) locale = GetDefaultLocale();
            await SelectLocale(locale);
        }

        public virtual void GetAvailableLocales (ICollection<string> tags)
        {
            foreach (var tag in AvailableLocales)
                tags.Add(tag);
        }

        public virtual bool LocaleAvailable (string locale) => AvailableLocales.Contains(locale);

        public virtual async UniTask SelectLocale (string locale)
        {
            if (!LocaleAvailable(locale))
            {
                Engine.Warn($"Failed to select locale: Locale '{locale}' is not available.");
                return;
            }

            if (locale == SelectedLocale) return;

            var eventArgs = new LocaleChangedArgs(locale, SelectedLocale);
            SelectedLocale = locale;

            using (new InteractionBlocker())
                foreach (var task in changeTasks.OrderBy(t => t.Priority))
                    await task.Handler(eventArgs);

            OnLocaleChanged?.Invoke(eventArgs);
        }

        public virtual void AddChangeLocaleTask (Func<LocaleChangedArgs, UniTask> handler, int priority)
        {
            if (!changeTasks.Any(t => t.Handler == handler))
                changeTasks.Add(new(handler, priority));
        }

        public virtual void RemoveChangeLocaleTask (Func<LocaleChangedArgs, UniTask> handler)
        {
            changeTasks.RemoveAll(t => t.Handler == handler);
        }

        /// <summary>
        /// Retrieves available localizations by locating folders inside the localization resources root.
        /// Folder names should correspond to the <see cref="Languages"/> tag entries (RFC5646).
        /// </summary>
        protected virtual async UniTask RetrieveAvailableLocales ()
        {
            var resources = await Providers.LocateFolders(Configuration.Loader.PathPrefix);
            AvailableLocales.Clear();
            AvailableLocales.AddRange(resources.Select(r => r.Name).Where(Languages.ContainsTag));
            AvailableLocales.Add(Configuration.SourceLocale);
        }

        protected virtual string GetDefaultLocale ()
        {
            if (Configuration.AutoDetectLocale && TryMapSystemLocale(out var locale))
                return locale;
            if (!string.IsNullOrEmpty(Configuration.DefaultLocale))
                return Configuration.DefaultLocale;
            return Configuration.SourceLocale;
        }

        protected virtual bool TryMapSystemLocale (out string locale)
        {
            var lang = Application.systemLanguage.ToString();
            foreach (var kv in Languages.GetRFCTagToName())
                if (kv.Value.EqualsFastIgnoreCase(lang))
                    return LocaleAvailable(locale = kv.Key);
            locale = null;
            return false;
        }
    }
}
