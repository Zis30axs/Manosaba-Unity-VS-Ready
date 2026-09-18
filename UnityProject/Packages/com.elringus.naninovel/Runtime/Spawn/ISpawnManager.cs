using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to manage objects spawned with <see cref="Commands.Spawn"/> commands.
    /// </summary>
    public interface ISpawnManager : IEngineService<SpawnConfiguration>
    {
        /// <summary>
        /// Currently spawned objects.
        /// </summary>
        IReadOnlyCollection<SpawnedObject> Spawned { get; }

        /// <summary>
        /// Spawns an object with the specified path.
        /// </summary>
        UniTask<SpawnedObject> Spawn (string path, AsyncToken token = default);
        /// <summary>
        /// Checks whether an object with the specified path is currently spawned.
        /// </summary>
        bool IsSpawned (string path);
        /// <summary>
        /// Returns a spawned object with the specified path.
        /// </summary>
        SpawnedObject GetSpawned (string path);
        /// <summary>
        /// Destroys a spawned object with the specified path.
        /// </summary>
        /// <param name="dispose">Whether to also dispose the associated <see cref="SpawnedObject.GameObject"/>.</param>
        void DestroySpawned (string path, bool dispose = true);

        /// <summary>
        /// Preloads and holds resources required to spawn an object with the specified path.
        /// </summary>
        UniTask HoldResources (string path, object holder);
        /// <summary>
        /// Releases resources required to spawn an object with the specified path.
        /// </summary>
        void ReleaseResources (string path, object holder);
    }
}
