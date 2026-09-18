#if SPRITE_DICING_AVAILABLE

using SpriteDicing;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="ICharacterActor"/> implementation using "SpriteDicing" extension to represent the actor.
    /// </summary>
    [ActorResources(typeof(DicedSpriteAtlas), false)]
    public class DicedSpriteCharacter : DicedSpriteActor<CharacterMetadata>, ICharacterActor
    {
        public CharacterLookDirection LookDirection
        {
            get => TransitionalRenderer.GetLookDirection(ActorMeta.BakedLookDirection);
            set => TransitionalRenderer.SetLookDirection(value, ActorMeta.BakedLookDirection);
        }

        public DicedSpriteCharacter (string id, CharacterMetadata meta, EmbeddedAppearanceLoader<DicedSpriteAtlas> loader)
            : base(id, meta, loader) { }

        public UniTask ChangeLookDirection (CharacterLookDirection lookDirection, Tween tween, AsyncToken token = default)
        {
            return TransitionalRenderer.ChangeLookDirection(lookDirection,
                ActorMeta.BakedLookDirection, tween, token);
        }
    }
}

#endif
