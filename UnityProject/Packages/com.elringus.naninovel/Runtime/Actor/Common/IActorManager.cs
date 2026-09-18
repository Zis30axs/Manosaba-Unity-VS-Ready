using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to manage <see cref="IActor"/> actors.
    /// </summary>
    public interface IActorManager : IEngineService
    {
        /// <summary>
        /// Invoked when an actor with the ID is added to the manager.
        /// </summary>
        event Action<string> OnActorAdded;
        /// <summary>
        /// Invoked when an actor with the ID is removed from the manager.
        /// </summary>
        event Action<string> OnActorRemoved;

        /// <summary>
        /// Base configuration of the manager.
        /// </summary>
        ActorManagerConfiguration ActorManagerConfiguration { get; }
        /// <summary>
        /// Actors currently managed by the service.
        /// </summary>
        IEnumerable<IActor> Actors { get; }

        /// <summary>
        /// Checks whether an actor with specified ID is currently managed
        /// by the service, ie instantiated and not removed.
        /// </summary>
        bool ActorExists (string actorId);
        /// <summary>
        /// Retrieves a managed actor with specified ID.
        /// </summary>
        IActor GetActor (string actorId);
        /// <summary>
        /// Adds a new managed actor with specified ID.
        /// </summary>
        UniTask<IActor> AddActor (string actorId);
        /// <summary>
        /// Removes a managed actor with specified ID.
        /// </summary>
        void RemoveActor (string actorId);
        /// <summary>
        /// Removes all the actors managed by the service.
        /// </summary>
        void RemoveAllActors ();
        /// <summary>
        /// Retrieves state of a managed actor with specified ID.
        /// </summary>
        ActorState GetActorState (string actorId);
        /// <summary>
        /// Retrieves appearance resource loader for actor with specified ID
        /// or null in case actor implementation doesn't require loading resources.  
        /// </summary>
        /// <remarks>
        /// This works even in case actor with specified ID is not currently instantiated
        /// by the manager, as appearance loader lifetime is independent of the associated
        /// actor, which is required to expose appearance resource management to external
        /// entities, such as script commands.
        /// </remarks>
        [CanBeNull] IResourceLoader GetAppearanceLoader (string actorId);
    }

    /// <summary>
    /// Implementation is able to manage <see cref="TActor"/> actors.
    /// </summary>
    /// <typeparam name="TActor">Type of managed actors.</typeparam>
    /// <typeparam name="TState">Type of state describing managed actors.</typeparam>
    /// <typeparam name="TMeta">Type of metadata required to construct managed actors.</typeparam>
    /// <typeparam name="TConfig">Type of the service configuration.</typeparam>
    public interface IActorManager<TActor, TState, TMeta, TConfig> : IActorManager, IEngineService<TConfig>, IStatefulService<GameStateMap>
        where TActor : IActor
        where TState : ActorState<TActor>, new()
        where TMeta : ActorMetadata
        where TConfig : ActorManagerConfiguration<TMeta>
    {
        /// <summary>
        /// Actors currently managed by the service.
        /// </summary>
        new IReadOnlyCollection<TActor> Actors { get; }

        /// <summary>
        /// Adds a new managed actor with specified ID.
        /// </summary>
        new UniTask<TActor> AddActor (string actorId);
        /// <summary>
        /// Adds a new managed actor with specified ID and state.
        /// </summary>
        UniTask<TActor> AddActor (string actorId, TState state);
        /// <summary>
        /// Retrieves a managed actor with specified ID.
        /// </summary>
        new TActor GetActor (string actorId);
        /// <summary>
        /// Retrieves state of a managed actor with specified ID.
        /// </summary>
        new TState GetActorState (string actorId);
    }
}
