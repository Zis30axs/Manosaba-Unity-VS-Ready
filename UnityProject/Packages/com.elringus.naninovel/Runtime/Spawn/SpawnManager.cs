using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="ISpawnManager"/>
    /// <remarks>Initialization order maxed, as spawned prefabs (eg, loaded when restoring saved game) may use other engine services.</remarks>
    [InitializeAtRuntime(int.MaxValue)]
    public class SpawnManager : IStatefulService<GameStateMap>, ISpawnManager
    {
        [Serializable]
        public class GameState
        {
            public List<SpawnedObjectState> SpawnedObjects;
        }

        public virtual SpawnConfiguration Configuration { get; }
        public virtual IReadOnlyCollection<SpawnedObject> Spawned => spawnedMap.Values;

        private readonly Dictionary<string, SpawnedObject> spawnedMap = new();
        private readonly IResourceProviderManager providersManager;
        private ResourceLoader<GameObject> loader;
        private GameObject container;

        public SpawnManager (SpawnConfiguration config, IResourceProviderManager providersManager)
        {
            Configuration = config;
            this.providersManager = providersManager;
        }

        public virtual UniTask InitializeService ()
        {
            loader = Configuration.Loader.CreateFor<GameObject>(providersManager);
            container = Engine.CreateObject("Spawn");
            return UniTask.CompletedTask;
        }

        public virtual void ResetService ()
        {
            DestroyAllSpawned();
        }

        public virtual void DestroyService ()
        {
            DestroyAllSpawned();
            loader?.ReleaseAll(this);
            ObjectUtils.DestroyOrImmediate(container);
        }

        public virtual void SaveServiceState (GameStateMap stateMap)
        {
            var state = new GameState {
                SpawnedObjects = spawnedMap.Values
                    .Select(o => new SpawnedObjectState(o)).ToList()
            };
            stateMap.SetState(state);
        }

        public virtual async UniTask LoadServiceState (GameStateMap stateMap)
        {
            var state = stateMap.GetState<GameState>();
            if (state?.SpawnedObjects?.Count > 0) await LoadObjects();
            else if (spawnedMap.Count > 0) DestroyAllSpawned();

            async UniTask LoadObjects ()
            {
                using var _ = ListPool<UniTask>.Rent(out var tasks);
                var toDestroy = spawnedMap.Values.ToList();
                foreach (var objState in state.SpawnedObjects)
                    if (IsSpawned(objState.Path)) UpdateObject(objState);
                    else tasks.Add(SpawnObject(objState));
                foreach (var obj in toDestroy)
                    DestroySpawned(obj.Path);
                await UniTask.WhenAll(tasks);

                async UniTask SpawnObject (SpawnedObjectState objState)
                {
                    var spawned = await Spawn(objState.Path);
                    objState.ApplyTo(spawned);
                    spawned.AwaitSpawn().Forget();
                }

                void UpdateObject (SpawnedObjectState objState)
                {
                    var spawned = GetSpawned(objState.Path);
                    toDestroy.Remove(spawned);
                    objState.ApplyTo(spawned);
                    spawned.AwaitSpawn().Forget();
                }
            }
        }

        public virtual async UniTask HoldResources (string path, object holder)
        {
            var resourcePath = SpawnConfiguration.ProcessInputPath(path, out _);
            await loader.Load(resourcePath, holder);
        }

        public virtual void ReleaseResources (string path, object holder)
        {
            var resourcePath = SpawnConfiguration.ProcessInputPath(path, out _);
            if (!loader.IsLoaded(resourcePath)) return;

            loader.Release(resourcePath, holder, false);
            if (loader.CountHolders(resourcePath) == 0)
            {
                if (IsSpawned(path))
                    DestroySpawned(path);
                loader.Release(resourcePath, holder);
            }
        }

        public virtual async UniTask<SpawnedObject> Spawn (string path, AsyncToken token = default)
        {
            if (IsSpawned(path)) throw new Error($"Object '{path}' is already spawned and can't be spawned again before it's destroyed.");

            var resourcePath = SpawnConfiguration.ProcessInputPath(path, out _);
            var prefabResource = await loader.LoadOrErr(resourcePath, this);
            token.ThrowIfCanceled();

            var gameObject = await Engine.Instantiate(prefabResource.Object, path, parent: container.transform, token: token);
            return spawnedMap[path] = new(path, gameObject);
        }

        public virtual void DestroySpawned (string path, bool dispose = true)
        {
            if (!IsSpawned(path)) return;
            var spawnedObject = GetSpawned(path);
            spawnedMap.Remove(path);
            if (dispose) ObjectUtils.DestroyOrImmediate(spawnedObject.GameObject);
        }

        public virtual bool IsSpawned (string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            return spawnedMap.ContainsKey(path);
        }

        public virtual SpawnedObject GetSpawned (string path)
        {
            return spawnedMap[path];
        }

        protected virtual void DestroyAllSpawned ()
        {
            foreach (var spawnedObj in spawnedMap.Values)
                ObjectUtils.DestroyOrImmediate(spawnedObj.GameObject);
            spawnedMap.Clear();
        }
    }
}
