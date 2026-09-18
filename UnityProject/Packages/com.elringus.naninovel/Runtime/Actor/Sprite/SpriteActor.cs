using System.Linq;
using Naninovel.FX;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="MonoBehaviourActor{TMeta}"/> using <see cref="TransitionalSpriteRenderer"/> to represent appearance of the actor.
    /// </summary>
    public abstract class SpriteActor<TMeta> : MonoBehaviourActor<TMeta>, Blur.IBlurable
        where TMeta : OrthoActorMetadata
    {
        public override string Appearance { get => appearance; set => SetAppearance(value); }
        public override bool Visible { get => visible; set => SetVisibility(value); }

        protected virtual StandaloneAppearanceLoader<Texture2D> AppearanceLoader { get; }
        protected virtual TransitionalRenderer TransitionalRenderer { get; private set; }

        private string appearance;
        private bool visible;
        private string defaultAppearancePath;
        private Resource<Texture2D> defaultAppearance;

        protected SpriteActor (string id, TMeta meta, StandaloneAppearanceLoader<Texture2D> loader)
            : base(id, meta)
        {
            AppearanceLoader = loader;
        }

        public override async UniTask Initialize ()
        {
            await base.Initialize();

            AppearanceLoader.OnLocalized += HandleAppearanceLocalized;
            TransitionalRenderer = TransitionalRenderer.CreateFor(ActorMeta, GameObject, false);
            SetVisibility(false);
        }

        public virtual UniTask Blur (float intensity, Tween tween, AsyncToken token = default)
        {
            return TransitionalRenderer.Blur(intensity, tween, token);
        }

        public override async UniTask ChangeAppearance (string appearance, Tween tween,
            Transition? transition = default, AsyncToken token = default)
        {
            var previousAppearance = this.appearance;
            this.appearance = appearance;

            var textureResource = string.IsNullOrWhiteSpace(appearance)
                ? await LoadDefaultAppearance(token)
                : await LoadAppearance(appearance, token);

            // Happens when the appearance was changed multiple times concurrently, in which case discarding the stale appearance.
            if (this.appearance != appearance)
            {
                if (!string.IsNullOrWhiteSpace(appearance))
                    AppearanceLoader?.Release(appearance, this);
            }
            else await TransitionalRenderer.TransitionTo(textureResource, tween, transition, token);

            if (!string.IsNullOrEmpty(previousAppearance) && previousAppearance != appearance && previousAppearance != defaultAppearancePath)
                AppearanceLoader?.Release(previousAppearance, this);
        }

        public override async UniTask ChangeVisibility (bool visible, Tween tween, AsyncToken token = default)
        {
            this.visible = visible;

            // When appearance is not set (and default one is not preloaded for some reason, eg when using dynamic parameters) 
            // and revealing the actor — attempt to load default appearance texture.
            if (!Visible && visible && string.IsNullOrWhiteSpace(Appearance) && !AppearanceLoader.IsLoaded(defaultAppearance?.Path))
                await ChangeAppearance(null, new(0), token: token);

            await TransitionalRenderer.FadeTo(visible ? TintColor.a : 0, tween, token);
        }

        public override void Dispose ()
        {
            base.Dispose();

            if (AppearanceLoader != null)
            {
                AppearanceLoader.OnLocalized -= HandleAppearanceLocalized;
                AppearanceLoader.ReleaseAll(this);
            }
        }

        protected virtual void SetAppearance (string appearance) => ChangeAppearance(appearance, new(0)).Forget();

        protected virtual void SetVisibility (bool visible) => ChangeVisibility(visible, new(0)).Forget();

        protected override Color GetBehaviourTintColor () => TransitionalRenderer.TintColor;

        protected override void SetBehaviourTintColor (Color tintColor)
        {
            if (!Visible) // Handle visibility-controlled alpha of the tint color.
                tintColor.a = TransitionalRenderer.TintColor.a;
            TransitionalRenderer.TintColor = tintColor;
        }

        protected virtual async UniTask<Resource<Texture2D>> LoadAppearance (string appearance, AsyncToken token)
        {
            var texture = await AppearanceLoader.LoadOrErr(appearance, this);
            token.ThrowIfCanceled(GameObject);
            ApplyTextureSettings(texture);
            return texture;
        }

        protected virtual async UniTask<Resource<Texture2D>> LoadDefaultAppearance (AsyncToken token)
        {
            if (defaultAppearance != null && defaultAppearance.Valid) return defaultAppearance;

            defaultAppearancePath = await LocateDefaultAppearance(token);
            if (!string.IsNullOrEmpty(defaultAppearancePath))
            {
                defaultAppearance = await AppearanceLoader.LoadOrErr(defaultAppearancePath, this);
                token.ThrowIfCanceled(GameObject);
            }
            else defaultAppearance = new(null, Engine.LoadInternalResource<Texture2D>("Textures/UnknownActor"));

            ApplyTextureSettings(defaultAppearance);

            if (!TransitionalRenderer.MainTexture)
                TransitionalRenderer.MainTexture = defaultAppearance;

            return defaultAppearance;
        }

        protected virtual async UniTask<string> LocateDefaultAppearance (AsyncToken token)
        {
            var texturePaths = (await AppearanceLoader.Locate(string.Empty))?.ToList();
            token.ThrowIfCanceled(GameObject);
            if (texturePaths != null && texturePaths.Count > 0)
            {
                // First, look for an appearance with a name, equal to actor's ID.
                if (texturePaths.Any(t => t.EqualsFast(Id)))
                    return texturePaths.First(t => t.EqualsFast(Id));
                // Then, try a 'Default' appearance.
                if (texturePaths.Any(t => t.EqualsFast("Default")))
                    return texturePaths.First(t => t.EqualsFast("Default"));
                // Finally, fallback to a first defined appearance.
                return texturePaths.FirstOrDefault();
            }
            return null;
        }

        protected virtual void ApplyTextureSettings (Texture2D texture)
        {
            if (texture && texture.wrapMode != TextureWrapMode.Clamp)
                texture.wrapMode = TextureWrapMode.Clamp;
        }

        protected virtual void HandleAppearanceLocalized (Resource<Texture2D> resource)
        {
            if (Appearance == AppearanceLoader.GetLocalPath(resource))
                Appearance = Appearance;
        }
    }
}
