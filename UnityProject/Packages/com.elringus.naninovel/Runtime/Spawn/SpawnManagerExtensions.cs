using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Provides extension methods for <see cref="ISpawnManager"/>.
    /// </summary>
    public static class SpawnManagerExtensions
    {
        /// <summary>
        /// Spawns a new object with the specified path or returns one if it's already spawned.
        /// </summary>
        public static async UniTask<SpawnedObject> GetOrSpawn (this ISpawnManager manager, string path,
            AsyncToken token = default)
        {
            return manager.IsSpawned(path)
                ? manager.GetSpawned(path)
                : await manager.Spawn(path, token);
        }

        /// <summary>
        /// Spawns an object with the specified path and applies specified spawn parameters.
        /// </summary>
        public static async UniTask<SpawnedObject> SpawnWithParameters (this ISpawnManager manager, string path,
            IReadOnlyList<string> parameters, AsyncToken token = default)
        {
            var spawnedObject = await manager.Spawn(path, token);
            spawnedObject.SetSpawnParameters(parameters, false);
            return spawnedObject;
        }

        /// <summary>
        /// Spawns an object with the specified path, applies specified spawn parameters and waits.
        /// </summary>
        public static async UniTask<SpawnedObject> SpawnWithParametersAndWait (this ISpawnManager manager, string path,
            IReadOnlyList<string> parameters, AsyncToken token = default)
        {
            var spawnedObject = await manager.SpawnWithParameters(path, parameters, token);
            await spawnedObject.AwaitSpawn(token);
            return spawnedObject;
        }
    }
}
