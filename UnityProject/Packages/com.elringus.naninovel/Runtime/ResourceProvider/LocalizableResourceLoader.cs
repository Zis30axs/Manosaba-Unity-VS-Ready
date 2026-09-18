using System.Collections.Generic;
using System.Linq;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="ResourceLoader{TResource}"/>, that will attempt to use <see cref="Naninovel.ILocalizationManager"/> to retrieve localized versions 
    /// of the requested resources and (optionally) fallback to the source (original) versions when localized versions are not available.
    /// </summary>
    public class LocalizableResourceLoader<TResource> : ResourceLoader<TResource>
        where TResource : UnityEngine.Object
    {
        /// <summary>
        /// Event invoked when loaded resource was reloaded due to locale change.
        /// </summary>
        public event System.Action<Resource<TResource>> OnLocalized;

        /// <summary>
        /// When set, will use the specified locale instead of the <see cref="ILocalizationManager.SelectedLocale"/>.
        /// </summary>
        public string OverrideLocale { get => overrideLocale; set => SetOverrideLocale(value); }

        protected readonly ILocalizationManager L10n;
        protected readonly List<IResourceProvider> SourceProviders;
        protected readonly string SourcePrefix;
        protected readonly bool FallbackToSource;

        private string overrideLocale;

        /// <param name="providers">Prioritized list of the source providers.</param>
        /// <param name="l10n">Localization manager instance.</param>
        /// <param name="sourcePrefix">Resource path prefix for the source providers.</param>
        /// <param name="fallbackToSource">Whether to fallback to the source versions of the resources when localized versions are not available.</param>
        public LocalizableResourceLoader (List<IResourceProvider> providers, IHoldersTracker holdersTracker, ILocalizationManager l10n,
            string sourcePrefix = null, bool fallbackToSource = true) : base(providers, holdersTracker, sourcePrefix)
        {
            L10n = l10n;
            SourceProviders = providers;
            SourcePrefix = sourcePrefix;
            FallbackToSource = fallbackToSource;

            L10n.AddChangeLocaleTask(HandleLocaleChanged, int.MinValue);
            InitializeProvisionSources();
        }

        ~LocalizableResourceLoader ()
        {
            L10n?.RemoveChangeLocaleTask(HandleLocaleChanged);
        }

        protected void SetOverrideLocale (string locale)
        {
            if (overrideLocale == locale) return;
            overrideLocale = locale;
            HandleLocaleChanged(default).Forget();
        }

        protected void InitializeProvisionSources ()
        {
            ProvisionSources.Clear();

            if (!L10n.IsSourceLocaleSelected() || !string.IsNullOrEmpty(overrideLocale))
            {
                var locale = string.IsNullOrEmpty(overrideLocale) ? L10n.SelectedLocale : overrideLocale;
                var localePrefix = $"{L10n.Configuration.Loader.PathPrefix}/{locale}/{SourcePrefix}";
                foreach (var provider in L10n.Providers)
                    ProvisionSources.Add(new(provider, localePrefix));
            }

            if (FallbackToSource)
                foreach (var provider in SourceProviders)
                    ProvisionSources.Add(new(provider, SourcePrefix));
        }

        protected async UniTask HandleLocaleChanged (LocaleChangedArgs _)
        {
            InitializeProvisionSources();

            using var __ = ListPool<UniTask>.Rent(out var tasks);
            foreach (var resource in LoadedByFullPath.Values.ToArray())
                tasks.Add(ReloadIfLocalized(resource));
            await UniTask.WhenAll(tasks);

            async UniTask ReloadIfLocalized (LoadedResource resource)
            {
                if (!resource.Valid || !await IsLocalized(resource)) return;

                LoadedByFullPath.Remove(resource.FullPath);
                LoadedByLocalPath.Remove(resource.LocalPath);
                LoadedByObject.Remove(resource.Object);

                if (HoldersTracker.Release(resource.Object, this) == 0)
                    resource.ProvisionSource.Provider.UnloadResourceBlocking(resource.FullPath);
                var localizedResource = await this.LoadOrErr(resource.LocalPath);
                HoldersTracker.Hold(localizedResource.Object, this);
                GetLoadedResourceOrNull(resource.LocalPath)?.AddHoldersFrom(resource);
                OnLocalized?.Invoke(localizedResource);
            }

            async UniTask<bool> IsLocalized (LoadedResource resource)
            {
                foreach (var source in ProvisionSources)
                {
                    if (source == resource.ProvisionSource) return false;
                    var fullPath = source.BuildFullPath(resource.LocalPath);
                    if (await source.Provider.ResourceExists<TResource>(fullPath)) return true;
                }
                return false;
            }
        }
    }
}
