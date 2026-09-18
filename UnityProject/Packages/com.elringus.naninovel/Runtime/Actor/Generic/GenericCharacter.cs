using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="ICharacterActor"/> implementation using <see cref="GenericCharacterBehaviour"/> to represent the actor.
    /// </summary>
    /// <remarks>
    /// Resource prefab should have a <see cref="GenericCharacterBehaviour"/> component attached to the root object.
    /// Appearance and other property changes are routed via the events of <see cref="GenericCharacterBehaviour"/> component.
    /// </remarks>
    [ActorResources(typeof(GenericCharacterBehaviour), false)]
    public class GenericCharacter : GenericActor<GenericCharacterBehaviour, CharacterMetadata>, ICharacterActor, Commands.LipSync.IReceiver
    {
        public CharacterLookDirection LookDirection { get => lookDirection; set => SetLookDirection(value); }

        private CharacterLipSyncer lipSyncer;
        private CharacterLookDirection lookDirection;

        public GenericCharacter (string id, CharacterMetadata meta, EmbeddedAppearanceLoader<GameObject> loader)
            : base(id, meta, loader) { }

        public override async UniTask Initialize ()
        {
            await base.Initialize();

            lipSyncer = new(Id, Behaviour.NotifyIsSpeakingChanged);
        }

        public override void Dispose ()
        {
            base.Dispose();

            lipSyncer?.Dispose();
        }

        public void AllowLipSync (bool active) => lipSyncer.SyncAllowed = active;

        public async UniTask ChangeLookDirection (CharacterLookDirection lookDirection, Tween tween, AsyncToken token = default)
        {
            this.lookDirection = lookDirection;

            Behaviour.NotifyLookDirectionChanged(lookDirection);

            if (Behaviour.TransformByLookDirection)
            {
                var rotation = LookDirectionToRotation(lookDirection);
                await ChangeRotation(rotation, tween, token);
            }
        }

        protected virtual void SetLookDirection (CharacterLookDirection lookDirection)
        {
            this.lookDirection = lookDirection;

            Behaviour.NotifyLookDirectionChanged(lookDirection);

            if (Behaviour.TransformByLookDirection)
            {
                var rotation = LookDirectionToRotation(lookDirection);
                SetBehaviourRotation(rotation);
            }
        }

        protected virtual Quaternion LookDirectionToRotation (CharacterLookDirection lookDirection)
        {
            var yAngle = lookDirection switch {
                CharacterLookDirection.Center => 0,
                CharacterLookDirection.Left => Behaviour.LookDeltaAngle,
                CharacterLookDirection.Right => -Behaviour.LookDeltaAngle,
                _ => 0f
            };
            var currentRotation = Rotation.eulerAngles;
            return Quaternion.Euler(currentRotation.x, yAngle, currentRotation.z);
        }
    }
}
