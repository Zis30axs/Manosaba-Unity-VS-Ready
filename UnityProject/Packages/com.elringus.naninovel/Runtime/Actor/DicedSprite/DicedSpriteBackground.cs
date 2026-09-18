#if SPRITE_DICING_AVAILABLE

using SpriteDicing;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="IBackgroundActor"/> implementation using "SpriteDicing" extension to represent the actor.
    /// </summary>
    [ActorResources(typeof(DicedSpriteAtlas), false)]
    public class DicedSpriteBackground : DicedSpriteActor<BackgroundMetadata>, IBackgroundActor
    {
        private BackgroundMatcher matcher;

        public DicedSpriteBackground (string id, BackgroundMetadata meta, EmbeddedAppearanceLoader<DicedSpriteAtlas> loader)
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

#endif
