using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Serializable state of a <see cref="IActor"/>.
    /// </summary>
    [System.Serializable]
    public abstract class ActorState
    {
        /// <inheritdoc cref="IActor.Appearance"/>
        public string Appearance => appearance;
        /// <inheritdoc cref="IActor.Visible"/>
        public bool Visible => visible;
        /// <inheritdoc cref="IActor.Position"/>
        public Vector3 Position => position;
        /// <inheritdoc cref="IActor.Rotation"/>
        public Quaternion Rotation => rotation;
        /// <inheritdoc cref="IActor.Scale"/>
        public Vector3 Scale => scale;
        /// <inheritdoc cref="IActor.TintColor"/>
        public Color TintColor => tintColor;

        [SerializeField] private string appearance;
        [SerializeField] private bool visible;
        [ScenePosition]
        [SerializeField] private Vector3 position = Vector3.zero;
        [DrawAsEuler]
        [SerializeField] private Quaternion rotation = Quaternion.identity;
        [SerializeField] private Vector3 scale = Vector3.one;
        [SerializeField] private Color tintColor = Color.white;

        /// <summary>
        /// Serializes the instance to a JSON string.
        /// </summary>
        public string ToJson () => JsonUtility.ToJson(this);

        /// <summary>
        /// Deserializes specified JSON string to the instance.
        /// </summary>
        public void OverwriteFromJson (string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }

        /// <summary>
        /// Override instance values from the specified actor.
        /// </summary>
        public void OverwriteFromActor (IActor actor)
        {
            appearance = actor.Appearance;
            visible = actor.Visible;
            position = actor.Position;
            rotation = actor.Rotation;
            scale = actor.Scale;
            tintColor = actor.TintColor;
        }

        /// <summary>
        /// Applies instance values to the specified actor.
        /// </summary>
        public UniTask ApplyToActor (IActor actor)
        {
            actor.Appearance = appearance;
            actor.Visible = visible;
            actor.Position = position;
            actor.Rotation = rotation;
            actor.Scale = scale;
            actor.TintColor = tintColor;
            return UniTask.CompletedTask;
        }
    }

    /// <summary>
    /// Serializable state of a <typeparamref name="TActor"/>.
    /// </summary>
    [System.Serializable]
    public abstract class ActorState<TActor> : ActorState
        where TActor : IActor
    {
        /// <inheritdoc cref="ActorState.OverwriteFromActor(IActor)"/>
        public virtual void OverwriteFromActor (TActor actor)
        {
            base.OverwriteFromActor(actor);
        }

        /// <inheritdoc cref="ActorState.ApplyToActor(IActor)"/>
        public virtual UniTask ApplyToActor (TActor actor)
        {
            return base.ApplyToActor(actor);
        }
    }
}
