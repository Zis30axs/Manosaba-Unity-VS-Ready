using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Used by actors which appearance resources are loaded independently of the main actor resource
    /// or otherwise actors which resources are mapped 1-1 to appearances, like sprite actors.
    /// </summary>
    public class StandaloneAppearanceLoader<TResource> : LocalizableResourceLoader<TResource> where TResource : Object
    {
        public StandaloneAppearanceLoader (string actorId, ActorMetadata meta, IResourceProviderManager resources, ILocalizationManager l10n) :
            base(resources.GetProviders(meta.Loader.ProviderTypes), resources, l10n, $"{meta.Loader.PathPrefix}/{actorId}") { }
    }

    /// <summary>
    /// Used by actors which appearance resources are all embedded into single resource and can't be loaded independently,
    /// such as diced sprite actors, which have all their appearances backed into single atlas texture.
    /// </summary>
    public class EmbeddedAppearanceLoader<TResource> : LocalizableResourceLoader<TResource>, IResourceLoader<TResource> where TResource : Object
    {
        private readonly string actorId;

        public EmbeddedAppearanceLoader (string actorId, ActorMetadata meta, IResourceProviderManager resources, ILocalizationManager l10n) :
            base(resources.GetProviders(meta.Loader.ProviderTypes), resources, l10n, meta.Loader.PathPrefix)
        {
            this.actorId = actorId;
        }

        UniTask<bool> IResourceLoader.Exists (string path) => Exists(actorId);
        bool IResourceLoader.IsLoaded (string path) => IsLoaded(actorId);
        Resource IResourceLoader.GetLoaded (string path) => GetLoaded(actorId);
        async UniTask<Resource> IResourceLoader.Load (string path, object holder) => await Load(actorId, holder);
        UniTask<Resource<TResource>> IResourceLoader<TResource>.Load (string path, object holder) => Load(actorId, holder);
        void IResourceLoader.Hold (string path, object holder) => Hold(actorId, holder);
        void IResourceLoader.Release (string path, object holder, bool unload) => Release(actorId, holder, unload);
        void IResourceLoader.ReleaseAll (object holder, bool unload) => ReleaseAll(unload);
        bool IResourceLoader.IsHeldBy (string path, object holder) => IsHeldBy(actorId, holder);
        int IResourceLoader.CountHolders (string path) => CountHolders(actorId);
    }
}
