using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Serializable state of <see cref="ICharacterActor"/>.
    /// </summary>
    [System.Serializable]
    public class CharacterState : ActorState<ICharacterActor>
    {
        /// <inheritdoc cref="ICharacterActor.LookDirection"/>
        public CharacterLookDirection LookDirection => lookDirection;

        [SerializeField] private CharacterLookDirection lookDirection;

        public override void OverwriteFromActor (ICharacterActor actor)
        {
            base.OverwriteFromActor(actor);

            lookDirection = actor.LookDirection;
        }

        public override UniTask ApplyToActor (ICharacterActor actor)
        {
            base.ApplyToActor(actor);

            actor.LookDirection = lookDirection;
            
            return UniTask.CompletedTask;
        }
    }
}
