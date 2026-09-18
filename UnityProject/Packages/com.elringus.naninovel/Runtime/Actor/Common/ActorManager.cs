using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="IActorManager{TActor, TState, TMeta, TConfig}"/>
    public abstract class ActorManager<TActor, TState, TMeta, TConfig> : IActorManager<TActor, TState, TMeta, TConfig>
        where TActor : IActor
        where TState : ActorState<TActor>, new()
        where TMeta : ActorMetadata
        where TConfig : ActorManagerConfiguration<TMeta>
    {
        [Serializable]
        public class GameState : ISerializationCallbackReceiver
        {
            public Dictionary<string, TState> ActorsMap { get; set; } = new();

            [SerializeField] private SerializableLiteralStringMap actorsJsonMap = new();

            public virtual void OnBeforeSerialize ()
            {
                actorsJsonMap.Clear();
                foreach (var kv in ActorsMap)
                {
                    var stateJson = kv.Value.ToJson();
                    actorsJsonMap.Add(kv.Key, stateJson);
                }
            }

            public virtual void OnAfterDeserialize ()
            {
                ActorsMap.Clear();
                foreach (var kv in actorsJsonMap)
                {
                    var state = new TState();
                    state.OverwriteFromJson(kv.Value);
                    ActorsMap.Add(kv.Key, state);
                }
            }
        }

        public event Action<string> OnActorAdded;
        public event Action<string> OnActorRemoved;

        public TConfig Configuration { get; }
        public ActorManagerConfiguration ActorManagerConfiguration => Configuration;
        IReadOnlyCollection<TActor> IActorManager<TActor, TState, TMeta, TConfig>.Actors => ManagedActors.Values;
        IEnumerable<IActor> IActorManager.Actors => ManagedActors.Values.Cast<IActor>();

        /// <summary>
        /// Actor ID to actor instance map.
        /// </summary>
        protected readonly Dictionary<string, TActor> ManagedActors;
        /// <summary>
        /// Actor ID to appearance resource loader singleton map.
        /// </summary>
        protected readonly Dictionary<string, IResourceLoader> AppearanceLoaders;

        private static IReadOnlyDictionary<string, Type> implNameToType;
        private readonly Dictionary<string, UniTaskCompletionSource<TActor>> pendingAddActorTasks;

        protected ActorManager (TConfig config)
        {
            Configuration = config;
            ManagedActors = new(StringComparer.Ordinal);
            AppearanceLoaders = new(StringComparer.Ordinal);
            pendingAddActorTasks = new();
        }

        public virtual UniTask InitializeService ()
        {
            implNameToType ??= Engine.Types.ActorImplementations
                .ToDictionary(a => a.AssemblyQualifiedName);
            return UniTask.CompletedTask;
        }

        public virtual void ResetService ()
        {
            RemoveAllActors();
        }

        public virtual void DestroyService ()
        {
            RemoveAllActors();
        }

        public virtual void SaveServiceState (GameStateMap stateMap)
        {
            var state = new GameState();
            foreach (var kv in ManagedActors)
            {
                var actorState = new TState();
                actorState.OverwriteFromActor(kv.Value);
                state.ActorsMap.Add(kv.Key, actorState);
            }
            stateMap.SetState(state);
        }

        public virtual async UniTask LoadServiceState (GameStateMap stateMap)
        {
            var state = stateMap.GetState<GameState>();
            if (state is null)
            {
                RemoveAllActors();
                return;
            }

            // Remove actors that doesn't exist in the serialized state.
            if (ManagedActors.Count > 0)
                foreach (var actorId in ManagedActors.Keys.ToList())
                    if (!state.ActorsMap.ContainsKey(actorId))
                        RemoveActor(actorId);

            foreach (var kv in state.ActorsMap)
            {
                var actor = await GetOrAddActor(kv.Key);
                await kv.Value.ApplyToActor(actor);
            }
        }

        public virtual bool ActorExists (string actorId) => !string.IsNullOrEmpty(actorId) && ManagedActors.ContainsKey(actorId);

        public virtual async UniTask<TActor> AddActor (string actorId)
        {
            if (ActorExists(actorId))
            {
                Engine.Warn($"Actor '{actorId}' was requested to be added, but it already exists.");
                return GetActor(actorId);
            }

            if (pendingAddActorTasks.TryGetValue(actorId, out var task))
                return await task.Task;

            pendingAddActorTasks[actorId] = new();

            var constructedActor = await ConstructActor(actorId);
            ManagedActors.Add(actorId, constructedActor);

            pendingAddActorTasks[actorId].TrySetResult(constructedActor);
            pendingAddActorTasks.Remove(actorId);

            OnActorAdded?.Invoke(actorId);

            return constructedActor;
        }

        async UniTask<IActor> IActorManager.AddActor (string actorId) => await AddActor(actorId);

        public virtual async UniTask<TActor> AddActor (string actorId, TState state)
        {
            if (string.IsNullOrWhiteSpace(actorId))
            {
                Engine.Warn($"Can't add an actor with '{state}' state: actor name is undefined.");
                return default;
            }

            var actor = await AddActor(actorId);
            await state.ApplyToActor(actor);
            return actor;
        }

        public virtual TActor GetActor (string actorId)
        {
            if (!ActorExists(actorId))
                throw new Error($"Can't find '{actorId}' actor.");

            return ManagedActors[actorId];
        }

        IActor IActorManager.GetActor (string actorId) => GetActor(actorId);

        public virtual async UniTask<TActor> GetOrAddActor (string actorId) => ActorExists(actorId) ? GetActor(actorId) : await AddActor(actorId);

        public virtual void RemoveActor (string actorId)
        {
            if (!ActorExists(actorId)) return;

            var actor = GetActor(actorId);
            ManagedActors.Remove(actor.Id);
            (actor as IDisposable)?.Dispose();

            OnActorRemoved?.Invoke(actorId);
        }

        public virtual void RemoveAllActors ()
        {
            if (ManagedActors.Count == 0) return;
            var managedActors = ManagedActors.Values.ToArray();
            for (int i = 0; i < managedActors.Length; i++)
                RemoveActor(managedActors[i].Id);
        }

        ActorState IActorManager.GetActorState (string actorId) => GetActorState(actorId);

        public virtual TState GetActorState (string actorId)
        {
            if (!ActorExists(actorId)) throw new Error($"Can't retrieve state of a '{actorId}' actor: actor not found.");
            var actor = GetActor(actorId);
            var state = new TState();
            state.OverwriteFromActor(actor);
            return state;
        }

        public IResourceLoader GetAppearanceLoader (string actorId)
        {
            if (AppearanceLoaders.TryGetValue(actorId, out var loader)) return loader;
            var meta = Configuration.GetMetadataOrDefault(actorId);
            var actorImpl = GetImplementationType(meta);
            var args = actorImpl.GetConstructors()[0].GetParameters();
            if (args.Length < 3) return AppearanceLoaders[actorId] = null;
            var loaderType = args[2].ParameterType;
            loader = (IResourceLoader)Activator.CreateInstance(loaderType, actorId, meta,
                Engine.GetServiceOrErr<IResourceProviderManager>(), Engine.GetServiceOrErr<ILocalizationManager>());
            return AppearanceLoaders[actorId] = loader;
        }

        /// <summary>
        /// Creates actor instance based on metadata associated with specified ID.
        /// </summary>
        protected virtual async UniTask<TActor> ConstructActor (string actorId)
        {
            var meta = Configuration.GetMetadataOrDefault(actorId);
            var implType = GetImplementationType(meta);
            var actor = default(TActor);
            var loader = GetAppearanceLoader(actorId);
            try
            {
                actor = loader != null
                    ? (TActor)Activator.CreateInstance(implType, actorId, meta, loader)
                    : (TActor)Activator.CreateInstance(implType, actorId, meta);
            }
            catch (Exception e) { throw new Error($"Failed to create instance of '{implType.FullName}' actor. Make sure the implementation has a compatible constructor.", e); }

            await actor.Initialize();
            await new TState().ApplyToActor(actor);

            return actor;
        }

        /// <summary>
        /// Returns implementation type associated with specified actor metadata.
        /// </summary>
        protected virtual Type GetImplementationType (TMeta meta)
        {
            if (implNameToType.TryGetValue(meta.Implementation, out var type)) return type;
            throw new Error($"'{meta.Implementation}' actor implementation type for '{typeof(TActor).Name}' is not found.");
        }
    }
}
