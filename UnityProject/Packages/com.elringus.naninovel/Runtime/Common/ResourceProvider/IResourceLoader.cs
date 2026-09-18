using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Naninovel
{
    /// <summary>
    /// Loads and unloads <see cref="Resource"/> objects, agnostic to the provision source.
    /// </summary>
    /// <remarks>
    /// Path argument in all the APIs is assumed local to the loader, ie w/o the provision source prefix.
    /// To get local path, use <see cref="GetLocalPath(string)"/> or <see cref="GetLocalPath(Resource)"/>.
    /// </remarks>
    public interface IResourceLoader
    {
        /// <summary>
        /// Resolves full path by prepending path prefix of this loader to the specified local path.
        /// </summary>
        string GetFullPath (string localPath);
        /// <summary>
        /// Given specified full path starts with path prefix managed by this loader,
        /// returns local (to the loader) path of the resource by removing the prefix, null otherwise.
        /// </summary>
        string GetLocalPath (string fullPath);
        /// <summary>
        /// Given specified resource object is loaded by this loader,
        /// returns local (to the loader) path of the resource, null otherwise.
        /// </summary>
        [CanBeNull] string GetLocalPath (UnityEngine.Object obj);
        /// <summary>
        /// Checks whether a resource with the specified path is available (can be loaded).
        /// </summary>
        UniTask<bool> Exists (string path);
        /// <summary>
        /// Locates paths of all the available resources (optionally) filtered by a base path.
        /// </summary>
        UniTask<IReadOnlyCollection<string>> Locate ([CanBeNull] string path = null);
        /// <summary>
        /// Checks whether a resource with the specified local path is loaded by this loader
        /// and is a valid Unity object (not destroyed).
        /// </summary>
        bool IsLoaded (string path);
        /// <summary>
        /// Returns a resource with the specified local path in case it's loaded by this loader, null otherwise.
        /// </summary>
        [CanBeNull] Resource GetLoaded (string path);
        /// <summary>
        /// Returns all the resources currently loaded by this loader.
        /// </summary>
        IReadOnlyCollection<Resource> GetAllLoaded ();
        /// <summary>
        /// Attempts to load a resource with the specified path.
        /// When <see cref="holder"/> is specified, will as well <see cref="Hold"/> the resource.
        /// </summary>
        UniTask<Resource> Load (string path, [CanBeNull] object holder = null);
        /// <summary>
        /// Attempts to load all the available resources (optionally) filtered by a base path.
        /// When <see cref="holder"/> is specified, will as well <see cref="Hold"/> the resources.
        /// </summary>
        UniTask<IReadOnlyCollection<Resource>> LoadAll ([CanBeNull] string path = null, [CanBeNull] object holder = null);
        /// <summary>
        /// Given resource with specified path is loaded by this loader (throws otherwise),
        /// registers specified object as holder of the resource.
        /// The resource won't be unloaded while it's held by at least one object.
        /// </summary>
        void Hold (string path, object holder);
        /// <summary>
        /// Given resource with specified path is loaded by this loader,
        /// removes specified object from holder list of the resource.
        /// Will (optionally) unload the resource in case no other objects are holding it.
        /// </summary>
        void Release (string path, object holder, bool unload = true);
        /// <summary>
        /// Removes specified holder object from holder list of all the resources loaded by this loader.
        /// Will (optionally) unload the affected resources in case no other objects are holding them.
        /// </summary>
        void ReleaseAll (object holder, bool unload = true);
        /// <summary>
        /// Given resource with specified path is loaded by this loader,
        /// checks whether specified holder object is in holder list of the resource.
        /// </summary>
        bool IsHeldBy (string path, object holder);
        /// <summary>
        /// Returns number of unique holders currently holding any resources in the loader.
        /// When <paramref name="path"/> is specified, will only count holders associated with the local resource path.
        /// </summary>
        int CountHolders ([CanBeNull] string path = null);
        /// <summary>
        /// Collects unique holders currently holding any resources in the loader to the specified set.
        /// When <paramref name="path"/> is specified, will only collect holders associated with the local resource path.
        /// </summary>
        void GetHolders (ISet<object> holders, [CanBeNull] string path = null);
    }

    /// <summary>
    /// Implementation is able to load and unload <see cref="Resource{TResource}"/> objects, agnostic to the provision source.
    /// </summary>
    public interface IResourceLoader<TResource> : IResourceLoader
        where TResource : UnityEngine.Object
    {
        /// <summary>
        /// Event invoked when a resource managed by this loader is loaded.
        /// </summary>
        public event Action<Resource<TResource>> OnLoaded;
        /// <summary>
        /// When invoked when a resource managed by this loader is unloaded.
        /// </summary>
        public event Action<Resource<TResource>> OnUnloaded;

        /// <inheritdoc cref="IResourceLoader.GetLocalPath(UnityEngine.Object)"/>
        [CanBeNull] string GetLocalPath (TResource obj);
        /// <inheritdoc cref="IResourceLoader.GetLoaded"/>
        [CanBeNull] new Resource<TResource> GetLoaded (string path);
        /// <inheritdoc cref="IResourceLoader.GetAllLoaded"/>
        new IReadOnlyCollection<Resource<TResource>> GetAllLoaded ();
        /// <inheritdoc cref="IResourceLoader.Load"/>
        new UniTask<Resource<TResource>> Load (string path, [CanBeNull] object holder = null);
        /// <inheritdoc cref="IResourceLoader.LoadAll"/>
        new UniTask<IReadOnlyCollection<Resource<TResource>>> LoadAll (string path = null, [CanBeNull] object holder = null);
    }
}
