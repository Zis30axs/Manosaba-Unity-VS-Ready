using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to represent an actor on scene.
    /// </summary>
    public interface IActor
    {
        /// <summary>
        /// Unique identifier of the actor. 
        /// </summary>
        string Id { get; }
        /// <summary>
        /// Appearance of the actor. 
        /// </summary>
        string Appearance { get; set; }
        /// <summary>
        /// Whether the actor is currently visible on scene.
        /// </summary>
        bool Visible { get; set; }
        /// <summary>
        /// Position of the actor.
        /// </summary>
        Vector3 Position { get; set; }
        /// <summary>
        /// Rotation of the actor.
        /// </summary>
        Quaternion Rotation { get; set; }
        /// <summary>
        /// Scale of the actor.
        /// </summary>
        Vector3 Scale { get; set; }
        /// <summary>
        /// Tint color of the actor.
        /// </summary>
        Color TintColor { get; set; }

        /// <summary>
        /// Allows to perform an async initialization routine.
        /// Invoked once by <see cref="IActorManager"/> after actor is constructed.
        /// </summary>
        UniTask Initialize ();

        /// <summary>
        /// Changes <see cref="Appearance"/> over specified time using specified animation tween and transition effect.
        /// </summary>
        UniTask ChangeAppearance (string appearance, Tween tween, Transition? transition = default, AsyncToken token = default);
        /// <summary>
        /// Changes <see cref="Visible"/> over specified time using specified animation tween.
        /// </summary>
        UniTask ChangeVisibility (bool visible, Tween tween, AsyncToken token = default);
        /// <summary>
        /// Changes <see cref="Position"/> over specified time using specified animation tween.
        /// </summary>
        UniTask ChangePosition (Vector3 position, Tween tween, AsyncToken token = default);
        /// <summary>
        /// Changes <see cref="Rotation"/> over specified time using specified animation tween.
        /// </summary>
        UniTask ChangeRotation (Quaternion rotation, Tween tween, AsyncToken token = default);
        /// <summary>
        /// Changes <see cref="Scale"/> factor over specified time using specified animation tween.
        /// </summary>
        UniTask ChangeScale (Vector3 scale, Tween tween, AsyncToken token = default);
        /// <summary>
        /// Changes <see cref="TintColor"/> over specified time using specified animation tween.
        /// </summary>
        UniTask ChangeTintColor (Color tintColor, Tween tween, AsyncToken token = default);
    }
}
