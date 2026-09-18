// #define NANINOVEL_RESOURCES_DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace Naninovel
{
    /// <summary>
    /// Allows to load and unload <see cref="Resource{TResource}"/> objects via a prioritized <see cref="ProvisionSource"/> list.
    /// </summary>
    public class ResourceLoader<TResource> : IResourceLoader<TResource>
        where TResource : UnityEngine.Object
    {
        protected class LoadedResource
        {
            public readonly Resource<TResource> Resource;
            public readonly ProvisionSource ProvisionSource;
            public readonly string LocalPath;
            public UnityEngine.Object Object => Resource.Object;
            public string FullPath => Resource.Path;
            public bool Valid => Resource.Valid;
            public IReadOnlyCollection<object> Holders => holders;

            private readonly HashSet<object> holders = new();

            public LoadedResource (Resource<TResource> resource, ProvisionSource provisionSource)
            {
                Resource = resource;
                ProvisionSource = provisionSource;
                LocalPath = provisionSource.BuildLocalPath(resource.Path);
            }

            public void AddHolder (object holder) => holders.Add(holder);
            public void RemoveHolder (object holder) => holders.Remove(holder);
            public bool IsHeldBy (object holder) => holders.Contains(holder);
            public void AddHoldersFrom (LoadedResource resource) => holders.UnionWith(resource.holders);
        }

        public event Action<Resource<TResource>> OnLoaded;
        public event Action<Resource<TResource>> OnUnloaded;

        /// <summary>
        /// Prioritized provision sources list used by the loader.
        /// </summary>
        protected readonly List<ProvisionSource> ProvisionSources = new();
        /// <summary>
        /// Resources loaded by the loader mapped by their full path.
        /// </summary>
        protected readonly Dictionary<string, LoadedResource> LoadedByFullPath = new();
        /// <summary>
        /// Resources loaded by the loader mapped by their local path.
        /// </summary>
        protected readonly Dictionary<string, LoadedResource> LoadedByLocalPath = new();
        /// <summary>
        /// Resources loaded by the loader mapped by their resource's object.
        /// </summary>
        protected readonly Dictionary<UnityEngine.Object, LoadedResource> LoadedByObject = new();
        /// <summary>
        /// Tracks active resource holders.
        /// </summary>
        protected readonly IHoldersTracker HoldersTracker;
        /// <summary>
        /// Optional prefix prepended to local paths managed by this loader;
        /// full path is resolved by prepending the prefix to local path.
        /// </summary>
        protected readonly string PathPrefix;

        public ResourceLoader (IList<IResourceProvider> providers, IHoldersTracker holdersTracker, string pathPrefix = "")
        {
            PathPrefix = pathPrefix;
            HoldersTracker = holdersTracker;
            foreach (var provider in providers)
                ProvisionSources.Add(new(provider, pathPrefix));
        }

        public string GetFullPath (string localPath)
        {
            if (string.IsNullOrEmpty(PathPrefix)) return localPath;
            return $"{PathPrefix}/{localPath}";
        }

        public virtual string GetLocalPath (string fullPath)
        {
            return LoadedByFullPath.GetValueOrDefault(fullPath)?.LocalPath;
        }

        public string GetLocalPath (UnityEngine.Object obj)
        {
            return LoadedByObject.GetValueOrDefault(obj)?.LocalPath;
        }

        public virtual string GetLocalPath (TResource obj)
        {
            return LoadedByObject.GetValueOrDefault(obj)?.LocalPath;
        }

        public virtual void Hold (string path, object holder)
        {
            var resource = GetLoadedResourceOrNull(path);
            if (resource is null || !resource.Valid)
                throw new Error($"Failed to hold '{GetFullPath(path)}' by '{holder}': resource is not loaded.");

            resource.AddHolder(holder);
            if (resource.Holders.Count == 1)
                HoldersTracker.Hold(resource.Object, this);
            LogDebug($"Held '{resource.FullPath}' by '{holder}' via '{resource.ProvisionSource.Provider}'.");
        }

        public virtual void Release (string path, object holder, bool unload = true)
        {
            var resource = GetLoadedResourceOrNull(path);
            if (resource == null) return;

            Release(resource, holder, unload);
            LogDebug($"Released '{resource.FullPath}' by '{holder}' via '{resource.ProvisionSource.Provider}'.");
        }

        public virtual void ReleaseAll (object holder, bool unload = true)
        {
            foreach (var res in LoadedByLocalPath.Values.ToArray())
                Release(res, holder, unload);
        }

        public virtual void UnloadAll ()
        {
            foreach (var res in LoadedByLocalPath.Values.ToArray())
                Unload(res);
        }

        public virtual bool IsHeldBy (string path, object holder)
        {
            return GetLoadedResourceOrNull(path)?.IsHeldBy(holder) ?? false;
        }

        public virtual int CountHolders (string path = null)
        {
            if (!string.IsNullOrEmpty(path))
                return GetLoadedResourceOrNull(path)?.Holders.Count ?? 0;
            using var _ = SetPool<object>.Rent(out var holders);
            foreach (var res in LoadedByFullPath.Values)
                holders.UnionWith(res.Holders);
            return holders.Count;
        }

        public virtual void GetHolders (ISet<object> holders, string path = null)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (GetLoadedResourceOrNull(path)?.Holders is { } result)
                    holders.UnionWith(result);
                return;
            }

            foreach (var res in LoadedByFullPath.Values)
                holders.UnionWith(res.Holders);
        }

        public virtual bool IsLoaded (string path)
        {
            return LoadedByLocalPath.TryGetValue(path, out var res) && res.Valid;
        }

        public virtual Resource<TResource> GetLoaded (string path)
        {
            return GetLoadedResourceOrNull(path)?.Resource;
        }

        public virtual async UniTask<Resource<TResource>> Load (string path, object holder = null)
        {
            if (IsLoaded(path))
            {
                if (holder != null) Hold(path, holder);
                return GetLoaded(path);
            }

            foreach (var source in ProvisionSources)
            {
                var fullPath = source.BuildFullPath(path);
                if (!await source.Provider.ResourceExists<TResource>(fullPath)) continue;

                var resource = await source.Provider.LoadResource<TResource>(fullPath);
                AddLoadedResource(new(resource, source));

                if (holder != null) Hold(path, holder);

                return resource;
            }

            return Resource<TResource>.Invalid;
        }

        public virtual async UniTask<IReadOnlyCollection<Resource<TResource>>> LoadAll (string path = null, object holder = null)
        {
            var result = new List<Resource<TResource>>();
            var addedPaths = new HashSet<string>();
            using var _ = ListPool<UniTask<Resource<TResource>>>.Rent(out var loadTasks);
            var loadData = new Dictionary<string, (ProvisionSource, string)>();

            foreach (var source in ProvisionSources)
            {
                var fullPath = source.BuildFullPath(path);
                var locatedResourcePaths = await source.Provider.LocateResources<TResource>(fullPath);
                foreach (var locatedResourcePath in locatedResourcePaths)
                {
                    var localPath = source.BuildLocalPath(locatedResourcePath);
                    if (!addedPaths.Add(localPath)) continue;

                    if (IsLoaded(localPath))
                    {
                        result.Add(GetLoaded(localPath));
                        continue;
                    }

                    loadTasks.Add(source.Provider.LoadResource<TResource>(locatedResourcePath));
                    loadData[locatedResourcePath] = (source, localPath);
                }
            }

            var resources = await UniTask.WhenAll(loadTasks);

            foreach (var resource in resources)
            {
                var (source, _) = loadData[resource.Path];
                AddLoadedResource(new(resource, source));
                if (holder != null) Hold(GetLocalPath(resource.Path), holder);
                result.Add(resource);
            }

            return result;
        }

        public virtual IReadOnlyCollection<Resource<TResource>> GetAllLoaded ()
        {
            if (LoadedByFullPath.Count == 0) return Array.Empty<Resource<TResource>>();
            var result = new List<Resource<TResource>>();
            foreach (var res in LoadedByFullPath.Values)
                if (res.Valid)
                    result.Add(res.Resource);
            return result;
        }

        public virtual async UniTask<IReadOnlyCollection<string>> Locate (string path = null)
        {
            using var _ = ListPool<UniTask<IEnumerable<string>>>.Rent(out var tasks);

            foreach (var source in ProvisionSources)
            {
                var fullPath = source.BuildFullPath(path);
                tasks.Add(source.Provider.LocateResources<TResource>(fullPath)
                    .ContinueWith(ps => ps.Select(p => source.BuildLocalPath(p))));
            }

            var result = await UniTask.WhenAll(tasks);

            return result.SelectMany(s => s).Distinct().ToArray();
        }

        public virtual async UniTask<bool> Exists (string path)
        {
            if (IsLoaded(path)) return true;

            foreach (var source in ProvisionSources)
            {
                var fullPath = source.BuildFullPath(path);
                if (await source.Provider.ResourceExists<TResource>(fullPath))
                    return true;
            }

            return false;
        }

        [CanBeNull]
        protected virtual LoadedResource GetLoadedResourceOrNull (string localPath)
        {
            return LoadedByLocalPath.TryGetValue(localPath, out var res) && res.Valid ? res : null;
        }

        protected virtual void Release (LoadedResource resource, object holder, bool unload = true)
        {
            resource.RemoveHolder(holder);
            if (resource.Valid && (resource.Holders.Count > 0 || HoldersTracker.Release(resource.Object, this) > 0 || !unload)) return;
            Unload(resource);
        }

        protected virtual void Unload (LoadedResource resource)
        {
            resource.ProvisionSource.Provider.UnloadResourceBlocking(resource.FullPath);
            RemoveLoadedResource(resource);
        }

        protected virtual void AddLoadedResource (LoadedResource resource)
        {
            LoadedByFullPath[resource.FullPath] = resource;
            LoadedByLocalPath[resource.LocalPath] = resource;
            if (resource.Object) LoadedByObject[resource.Object] = resource;
            OnLoaded?.Invoke(resource.Resource);
            LogDebug($"<color=green>Loaded '{resource.FullPath}' via '{resource.ProvisionSource.Provider}'.</color>");
        }

        protected virtual void RemoveLoadedResource (LoadedResource resource)
        {
            // Notify before removing, as listener may need to resolve local path of the resource.
            OnUnloaded?.Invoke(resource.Resource);
            LoadedByFullPath.Remove(resource.FullPath);
            LoadedByLocalPath.Remove(resource.LocalPath);
            if (resource.Object) LoadedByObject.Remove(resource.Object);
            LogDebug($"<color=red>Unloaded '{resource.FullPath}' via '{resource.ProvisionSource.Provider}'.</color>");
        }

        Resource IResourceLoader.GetLoaded (string path) => GetLoaded(path);
        IReadOnlyCollection<Resource> IResourceLoader.GetAllLoaded () => GetAllLoaded();
        async UniTask<Resource> IResourceLoader.Load (string path, object holder) => await Load(path, holder);
        async UniTask<IReadOnlyCollection<Resource>> IResourceLoader.LoadAll (string path, object holder) => await LoadAll(path, holder);

        [Conditional("NANINOVEL_RESOURCES_DEBUG")]
        private static void LogDebug (string message) => Engine.Log(message);
    }
}
