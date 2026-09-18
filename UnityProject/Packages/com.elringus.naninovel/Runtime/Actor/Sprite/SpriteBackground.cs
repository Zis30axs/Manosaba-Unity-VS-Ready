using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="IBackgroundActor"/> implementation using <see cref="SpriteActor{TMeta}"/> to represent the actor.
    /// </summary>
    [ActorResources(typeof(Texture2D), true)]
    public class SpriteBackground : SpriteActor<BackgroundMetadata>, IBackgroundActor
    {
        private BackgroundMatcher matcher;

        public SpriteBackground (string id, BackgroundMetadata meta, StandaloneAppearanceLoader<Texture2D> loader)
            : base(id, meta, loader) { }

        public override async UniTask Initialize ()
        {
            await base.Initialize();
            matcher = BackgroundMatcher.CreateFor(ActorMeta, TransitionalRenderer);
        }

        public override void Dispose ()
        {
            base.Dispose();
            matcher?.Stop();
        }
    }
}
