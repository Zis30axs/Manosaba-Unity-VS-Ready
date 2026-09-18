namespace Naninovel
{
    /// <summary>
    /// Provides extension methods for <see cref="IActorManager"/>.
    /// </summary>
    public static class ActorManagerExtensions
    {
        /// <summary>
        /// Returns a managed actor with the specified ID. If the actor doesn't exist, will add it.
        /// </summary>
        public static async UniTask<IActor> GetOrAddActor (this IActorManager manager, string actorId)
        {
            return manager.ActorExists(actorId) ? manager.GetActor(actorId) : await manager.AddActor(actorId);
        }

        /// <summary>
        /// Returns a managed actor with the specified ID. If the actor doesn't exist, will add it.
        /// </summary>
        public static async UniTask<TActor> GetOrAddActor<TActor, TState, TMeta, TConfig> (this IActorManager<TActor, TState, TMeta, TConfig> manager, string actorId)
            where TActor : IActor
            where TState : ActorState<TActor>, new()
            where TMeta : ActorMetadata
            where TConfig : ActorManagerConfiguration<TMeta>
        {
            return manager.ActorExists(actorId) ? manager.GetActor(actorId) : await manager.AddActor(actorId);
        }

        /// <summary>
        /// Retrieves metadata of the actor with specified ID or default when actor with the ID is not found.
        /// </summary>
        public static TMeta GetActorMetaOrDefault<TActor, TState, TMeta, TConfig> (this IActorManager<TActor, TState, TMeta, TConfig> manager, string actorId)
            where TActor : IActor
            where TState : ActorState<TActor>, new()
            where TMeta : ActorMetadata
            where TConfig : ActorManagerConfiguration<TMeta>
        {
            return manager.Configuration.GetMetadataOrDefault(actorId);
        }
    }
}
