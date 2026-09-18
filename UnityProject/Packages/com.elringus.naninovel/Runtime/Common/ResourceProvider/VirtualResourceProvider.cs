using System;
using System.Collections.Generic;
using System.Linq;

namespace Naninovel
{
    /// <summary>
    /// A mock <see cref="IResourceProvider"/> implementation allowing to add resources at runtime.
    /// </summary>
    public class VirtualResourceProvider : IResourceProvider
    {
        /// <summary>
        /// Whether <see cref="UnloadResourceBlocking"/> and similar methods should remove the added resources.
        /// </summary>
        public bool RemoveResourcesOnUnload { get; set; } = true;
        public bool IsLoading => false;
        public float LoadProgress => 1;
        public IReadOnlyCollection<Resource> LoadedResources => Resources?.Values;

        #pragma warning disable 0067
        public event Action<float> OnLoadProgress;
        public event Action<string> OnMessage;
        #pragma warning restore 0067

        protected readonly Dictionary<string, Resource> Resources = new();
        protected readonly HashSet<string> FolderPaths = new();

        public bool SupportsType<T> () where T : UnityEngine.Object => true;

        public void AddResource<T> (string path, T obj) where T : UnityEngine.Object
        {
            Resources.Add(path, new Resource<T>(path, obj));
        }

        public void SetResource<T> (string path, T obj) where T : UnityEngine.Object
        {
            Resources[path] = new Resource<T>(path, obj);
        }

        public void RemoveResource (string path)
        {
            Resources.Remove(path);
        }

        public void RemoveAllResources ()
        {
            Resources.Clear();
            FolderPaths.Clear();
        }

        public void AddFolder (string folderPath)
        {
            FolderPaths.Add(folderPath);
        }

        public void RemoveFolder (string path)
        {
            FolderPaths.Remove(path);
        }

        public Resource<T> LoadResourceBlocking<T> (string path) where T : UnityEngine.Object
        {
            return Resources.TryGetValue(path, out var resource) ? resource as Resource<T> : null;
        }

        public UniTask<Resource<T>> LoadResource<T> (string path) where T : UnityEngine.Object
        {
            var resource = LoadResourceBlocking<T>(path);
            return UniTask.FromResult(resource);
        }

        public IReadOnlyCollection<Resource<T>> LoadResourcesBlocking<T> (string path) where T : UnityEngine.Object
        {
            return Resources.Where(kv => kv.Value?.Object.GetType() == typeof(T)).Select(kv => kv.Key).LocateResourcePathsAtFolder(path).Select(LoadResourceBlocking<T>).ToArray();
        }

        public UniTask<IReadOnlyCollection<Resource<T>>> LoadResources<T> (string path) where T : UnityEngine.Object
        {
            var resources = LoadResourcesBlocking<T>(path);
            return UniTask.FromResult(resources);
        }

        public IReadOnlyCollection<Folder> LocateFoldersBlocking (string path)
        {
            return FolderPaths.LocateFolderPathsAtFolder(path).Select(p => new Folder(p)).ToArray();
        }

        public UniTask<IReadOnlyCollection<Folder>> LocateFolders (string path)
        {
            var folders = LocateFoldersBlocking(path);
            return UniTask.FromResult(folders);
        }

        public IReadOnlyCollection<string> LocateResourcesBlocking<T> (string path) where T : UnityEngine.Object
        {
            return Resources.Where(kv => kv.Value?.Object.GetType() == typeof(T)).Select(kv => kv.Key).LocateResourcePathsAtFolder(path).ToArray();
        }

        public UniTask<IReadOnlyCollection<string>> LocateResources<T> (string path) where T : UnityEngine.Object
        {
            var resources = LocateResourcesBlocking<T>(path);
            return UniTask.FromResult(resources);
        }

        public bool ResourceExistsBlocking<T> (string path) where T : UnityEngine.Object
        {
            return Resources.ContainsKey(path) && Resources[path].Object.GetType() == typeof(T);
        }

        public UniTask<bool> ResourceExists<T> (string path) where T : UnityEngine.Object
        {
            var result = ResourceExistsBlocking<T>(path);
            return UniTask.FromResult(result);
        }

        public bool ResourceLoaded (string path)
        {
            return Resources.ContainsKey(path);
        }

        public bool ResourceLoading (string path)
        {
            return false;
        }

        public void UnloadResourceBlocking (string path)
        {
            if (RemoveResourcesOnUnload)
                RemoveResource(path);
        }

        public UniTask UnloadResource (string path)
        {
            UnloadResourceBlocking(path);
            return UniTask.CompletedTask;
        }

        public void UnloadResourcesBlocking ()
        {
            if (RemoveResourcesOnUnload)
                RemoveAllResources();
        }

        public UniTask UnloadResources ()
        {
            UnloadResourcesBlocking();
            return UniTask.CompletedTask;
        }

        public Resource<T> GetLoadedResourceOrNull<T> (string path) where T : UnityEngine.Object
        {
            if (!ResourceLoaded(path)) return null;
            return LoadResourceBlocking<T>(path);
        }
    }
}
