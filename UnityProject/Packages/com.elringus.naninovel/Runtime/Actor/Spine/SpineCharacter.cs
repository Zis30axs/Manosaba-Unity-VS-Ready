#if NANINOVEL_ENABLE_SPINE
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="ICharacterActor"/> implementation using <see cref="SpineController"/> to represent the actor.
    /// </summary>
    [ActorResources(typeof(SpineController), false)]
    public class SpineCharacter : SpineActor<CharacterMetadata>, ICharacterActor, LipSync.IReceiver
    {
        public CharacterLookDirection LookDirection
        {
            get => TransitionalRenderer.GetLookDirection(ActorMeta.BakedLookDirection);
            set => TransitionalRenderer.SetLookDirection(value, ActorMeta.BakedLookDirection);
        }

        protected virtual CharacterLipSyncer LipSyncer { get; private set; }

        public SpineCharacter (string id, CharacterMetadata meta, EmbeddedAppearanceLoader<GameObject> loader)
            : base(id, meta, loader) { }

        public override async UniTask Initialize ()
        {
            await base.Initialize();
            LipSyncer = new CharacterLipSyncer(Id, Controller.ChangeIsSpeaking);
        }

        public override void Dispose ()
        {
            LipSyncer.Dispose();
            base.Dispose();
        }

        public virtual UniTask ChangeLookDirection (CharacterLookDirection lookDirection, Tween tween, AsyncToken token = default)
        {
            return TransitionalRenderer.ChangeLookDirection(lookDirection, ActorMeta.BakedLookDirection, tween, token);
        }

        public virtual void AllowLipSync (bool active) => LipSyncer.SyncAllowed = active;
    }
}
#endif
