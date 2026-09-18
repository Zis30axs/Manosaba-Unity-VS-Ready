using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents a named state of an actor.
    /// </summary>
    [System.Serializable]
    public abstract class ActorPose<TState>
        where TState : ActorState
    {
        /// <summary>
        /// Name (identifier) of the pose.
        /// </summary>
        public string Name => name;
        /// <summary>
        /// Actor state associated with the pose.
        /// </summary>
        public TState ActorState => actorState;

        [SerializeField] private string name;
        [SerializeField] private TState actorState;
        [HideInInspector]
        [SerializeField] private string[] overriddenProperties;

        public bool IsPropertyOverridden (string propertyName)
        {
            if (overriddenProperties is null) return false;
            for (int i = 0; i < overriddenProperties.Length; i++)
                if (overriddenProperties[i].EqualsFastIgnoreCase(propertyName))
                    return true;
            return false;
        }
    }
}
