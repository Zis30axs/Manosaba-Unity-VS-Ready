using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="ICharacterActor"/> implementation using <see cref="LayeredActorBehaviour"/> to represent the actor.
    /// </summary>
    [ActorResources(typeof(LayeredCharacterBehaviour), false)]
    public class LayeredCharacter : LayeredActor<LayeredCharacterBehaviour, CharacterMetadata>, ICharacterActor, Commands.LipSync.IReceiver
    {
        public CharacterLookDirection LookDirection
        {
            get => TransitionalRenderer.GetLookDirection(ActorMeta.BakedLookDirection);
            set => TransitionalRenderer.SetLookDirection(value, ActorMeta.BakedLookDirection);
        }

        private CharacterLipSyncer lipSyncer;

        public LayeredCharacter (string id, CharacterMetadata meta, EmbeddedAppearanceLoader<GameObject> loader)
            : base(id, meta, loader) { }

        public override async UniTask Initialize ()
        {
            await base.Initialize();

            lipSyncer = new(Id, Behaviour.NotifyIsSpeakingChanged);
        }

        public UniTask ChangeLookDirection (CharacterLookDirection lookDirection, Tween tween, AsyncToken token = default)
        {
            return TransitionalRenderer.ChangeLookDirection(lookDirection,
                ActorMeta.BakedLookDirection, tween, token);
        }

        public override void Dispose ()
        {
            base.Dispose();

            lipSyncer?.Dispose();
        }

        public void AllowLipSync (bool active) => lipSyncer.SyncAllowed = active;
    }
}
