using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Provides extension methods for <see cref="IResourceLoader"/>.
    /// </summary>
    public static class ResourceLoaderExtensions
    {
        /// <summary>
        /// Loads a resource with the specified path. When holder is specified, will as well Hold(string, object) the resource.
        /// Throws when loading failed or the resource doesn't exist.
        /// </summary>
        public static async UniTask<Resource> LoadOrErr (this IResourceLoader loader, string path, [CanBeNull] object holder = null)
        {
            var resource = await loader.Load(path, holder);
            if (!resource.Valid) throw new Error($"Failed to load '{path}' resource: make sure the resource is registered with a resource provider.");
            return resource;
        }

        /// <summary>
        /// Given specified resource is loaded by the loader, hold it.
        /// </summary>
        public static void Hold (this IResourceLoader loader, Resource resource, object holder)
        {
            var localPath = loader.GetLocalPath(resource);
            if (!string.IsNullOrEmpty(localPath))
                loader.Hold(localPath, holder);
        }

        /// <summary>
        /// Given specified resource is loaded by the loader, release it.
        /// </summary>
        public static void Release (this IResourceLoader loader, Resource resource, object holder, bool unload = true)
        {
            var localPath = loader.GetLocalPath(resource);
            if (!string.IsNullOrEmpty(localPath))
                loader.Release(localPath, holder, unload);
        }

        /// <summary>
        /// Attempts to retrieve a resource with the specified local path; returns false when it's not loaded by this loader
        /// or is not a valid (destroyed) Unity object.
        /// </summary>
        public static bool TryGetLoaded (this IResourceLoader loader, string path, out Object resource)
        {
            resource = loader.GetLoaded(path);
            return loader.IsLoaded(path);
        }

        /// <inheritdoc cref="TryGetLoaded"/>
        public static bool TryGetLoaded<TResource> (this IResourceLoader<TResource> loader, string path, out TResource resource)
            where TResource : Object
        {
            resource = loader.GetLoaded(path);
            return loader.IsLoaded(path);
        }

        /// <summary>
        /// Returns a resource with the specified local path in case it's loaded by this loader
        /// and is a valid (not destroyed) Unity object, throws otherwise.
        /// </summary>
        /// <exception cref="Error">Thrown when requested resource is not loaded.</exception>
        public static Object GetLoadedOrErr (this IResourceLoader loader, string path)
        {
            if (!loader.IsLoaded(path)) throw new Error($"Failed to get '{path}' resource: not loaded.");
            return loader.GetLoaded(path)!;
        }

        /// <summary>
        /// Loads a resource with the specified type and path. When holder is specified, will as well Hold(string, object) the resource.
        /// Throws when loading failed or the resource doesn't exist.
        /// </summary>
        public static async UniTask<Resource<TResource>> LoadOrErr<TResource> (this IResourceLoader<TResource> loader, string path, [CanBeNull] object holder = null)
            where TResource : Object
        {
            var resource = await loader.Load(path, holder);
            if (!resource.Valid) throw new Error($"Failed to load '{path}' resource of type '{typeof(TResource).FullName}': make sure the resource is registered with a resource provider.");
            return resource;
        }

        /// <inheritdoc cref="GetLoadedOrErr"/>
        public static TResource GetLoadedOrErr<TResource> (this IResourceLoader<TResource> loader, string path)
            where TResource : Object
        {
            if (!loader.IsLoaded(path)) throw new Error($"Failed to get '{path}' resource of type '{typeof(TResource).FullName}': not loaded.");
            return loader.GetLoaded(path)!;
        }

        /// <summary>
        /// Given both 'from' and 'to' objects are resources loaded by the loader, holds the 'to' object while
        /// releasing the 'from' object, but only in case they are not the same object and returns the 'to' object.
        /// </summary>
        /// <remarks>
        /// This is intended to be used as a shortcut when re-assigning resource objects, encapsulating the
        /// invocations of <see cref="IResourceLoader.Hold"/> and <see cref="IResourceLoader.Release"/>.
        /// </remarks>
        [CanBeNull] [return: NotNullIfNotNull("to")]
        public static TResource Juggle<TResource> (this IResourceLoader<TResource> loader, [CanBeNull] TResource from, [CanBeNull] TResource to, object holder)
            where TResource : Object
        {
            var toPath = to ? loader.GetLocalPath(to) : null;
            var fromPath = from ? loader.GetLocalPath(from) : null;
            if (toPath != null) loader.Hold(toPath, holder);
            if (fromPath != null && fromPath != toPath) loader.Release(fromPath, holder);
            return to;
        }
    }
}
