using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Asset used to configure <see cref="IActorManager"/> services.
    /// </summary>
    public abstract class ActorManagerConfiguration : Configuration
    {
        [Tooltip("Default duration (in seconds) for all the actor modifications (changing appearance, position, tint, etc).")]
        public float DefaultDuration = .35f;
        [Tooltip("Easing function to use by default for all the actor modification animations (changing appearance, position, tint, etc).")]
        public EasingType DefaultEasing = EasingType.Linear;
        [Tooltip("Whether to automatically reveal (show) an actor when executing modification commands.")]
        public bool AutoShowOnModify = true;

        public abstract ActorMetadataMap MetadataMap { get; }

        /// <summary>
        /// Attempts to retrieve metadata of an actor with the specified ID;
        /// when not found, will return a default metadata.
        /// </summary>
        public ActorMetadata GetMetadataOrDefault (string actorId) => GetMetadataOrDefaultNonGeneric(actorId);

        /// <summary>
        /// Attempts to retrieve a pose associated with the specified actor ID and name (assigned via actor metadata).
        /// When not found, will attempt to retrieve a shared pose with the specified name (assigned via configuration).
        /// Will return null in case neither found.
        /// </summary>
        public ActorPose<TState> GetActorOrSharedPose<TState> (string actorId, string poseName) where TState : ActorState
        {
            return GetMetadataOrDefault(actorId).GetPose<TState>(poseName) ?? GetSharedPose<TState>(poseName);
        }

        protected abstract ActorMetadata GetMetadataOrDefaultNonGeneric (string actorId);
        protected abstract ActorPose<TState> GetSharedPose<TState> (string poseName) where TState : ActorState;
    }

    /// <summary>
    /// Asset used to configure <see cref="IActorManager"/> services.
    /// </summary>
    /// <typeparam name="TMeta">Type of actor metadata configured service operates with.</typeparam>
    public abstract class ActorManagerConfiguration<TMeta> : ActorManagerConfiguration
        where TMeta : ActorMetadata
    {
        public abstract TMeta DefaultActorMetadata { get; }
        public abstract ActorMetadataMap<TMeta> ActorMetadataMap { get; }
        public override ActorMetadataMap MetadataMap => ActorMetadataMap;

        /// <inheritdoc cref="ActorManagerConfiguration.GetMetadataOrDefault"/>
        public new TMeta GetMetadataOrDefault (string actorId)
        {
            return ActorMetadataMap.ContainsId(actorId ?? "") ? ActorMetadataMap[actorId] : DefaultActorMetadata;
        }

        protected override ActorMetadata GetMetadataOrDefaultNonGeneric (string actorId) => GetMetadataOrDefault(actorId);
    }
}
