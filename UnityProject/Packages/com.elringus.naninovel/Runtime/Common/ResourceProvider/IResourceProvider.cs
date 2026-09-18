using System;
using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to load and unload <see cref="Resource"/> objects from a specific provision source.
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// Event executed when load progress is changed.
        /// </summary>
        event Action<float> OnLoadProgress;
        /// <summary>
        /// Event executed when an information message is sent by the provider.
        /// </summary>
        event Action<string> OnMessage;

        /// <summary>
        /// Whether any resource loading operations are currently active.
        /// </summary>
        bool IsLoading { get; }
        /// <summary>
        /// Current resources loading progress, in 0.0 to 1.0 range.
        /// </summary>
        float LoadProgress { get; }
        /// <summary>
        /// Returns a collection of resources loaded by the provider.
        /// </summary>
        IReadOnlyCollection<Resource> LoadedResources { get; }

        /// <summary>
        /// Whether the provider can work with resource objects of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the resource object.</typeparam>
        bool SupportsType<T> () where T : UnityEngine.Object;
        /// <summary>
        /// Loads resource with the specified path; will returned cached version in case it's already loaded.
        /// </summary>
        /// <typeparam name="T">Type of the resource to load.</typeparam>
        /// <param name="path">Path to the resource location.</param>
        UniTask<Resource<T>> LoadResource<T> (string path) where T : UnityEngine.Object;
        /// <summary>
        /// Loads all available resources at the specified path.
        /// </summary>
        /// <typeparam name="T">Type of the resources to load.</typeparam>
        /// <param name="path">Path to the resources location.</param>
        UniTask<IReadOnlyCollection<Resource<T>>> LoadResources<T> (string path) where T : UnityEngine.Object;
        /// <summary>
        /// Locates all available resources at the specified path.
        /// </summary>
        /// <typeparam name="T">Type of the resources to locate.</typeparam>
        /// <param name="path">Path (root) to the resources location.</param>
        /// <returns>Collection of the located resource paths.</returns>
        UniTask<IReadOnlyCollection<string>> LocateResources<T> (string path) where T : UnityEngine.Object;
        /// <summary>
        /// Locates all available folders at the specified path.
        /// </summary>
        /// <param name="path">Path to the parent folder or empty string if none.</param>
        UniTask<IReadOnlyCollection<Folder>> LocateFolders (string path);
        /// <summary>
        /// Checks whether resource with the specified type and path is available.
        /// </summary>
        /// <typeparam name="T">Type of the resource to look for.</typeparam>
        /// <param name="path">Path to the resource location.</param>
        UniTask<bool> ResourceExists<T> (string path) where T : UnityEngine.Object;
        /// <summary>
        /// Unloads resource at the specified path (in case it was previously loaded by the provider).
        /// </summary>
        /// <param name="path">Path to the resource location.</param>
        void UnloadResourceBlocking (string path);
        /// <summary>
        /// Unloads all resources loaded by the provider.
        /// </summary>
        void UnloadResourcesBlocking ();
        /// <summary>
        /// Checks whether resource with the specified path is loaded (available in the cache).
        /// </summary>
        bool ResourceLoaded (string path);
        /// <summary>
        /// Checks whether resource with the specified path is currently being loaded.
        /// </summary>
        bool ResourceLoading (string path);
        /// <summary>
        /// Attempts to retrieve a loaded (cached) resource with the specified path; returns null if the resource is not loaded.
        /// </summary>
        Resource<T> GetLoadedResourceOrNull<T> (string path) where T : UnityEngine.Object;
    }
}
