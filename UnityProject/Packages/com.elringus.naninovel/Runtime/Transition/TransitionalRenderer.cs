using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Allows rendering a texture with <see cref="TransitionalMaterial"/> and transition to another texture with a set of configurable visual effects.
    /// </summary>
    public abstract class TransitionalRenderer : MonoBehaviour
    {
        /// <summary>
        /// Material for rendering the texture transition.
        /// </summary>
        public virtual TransitionalMaterial TextureMaterial { get; private set; }
        /// <summary>
        /// Current transition mode data.
        /// </summary>
        public virtual Transition Transition
        {
            get => new(TextureMaterial.TransitionName, TextureMaterial.TransitionParams, TextureMaterial.DissolveTexture);
            set
            {
                TextureMaterial.TransitionName = value.Name;
                TextureMaterial.TransitionParams = value.Parameters;
                TextureMaterial.DissolveTexture = value.DissolveTexture;
            }
        }
        /// <inheritdoc cref="TransitionalMaterial.MainTexture"/>
        public virtual Texture MainTexture { get => TextureMaterial.MainTexture; set => TextureMaterial.MainTexture = value; }
        /// <inheritdoc cref="TransitionalMaterial.TransitionTexture"/>
        public virtual Texture TransitionTexture { get => TextureMaterial.TransitionTexture; set => TextureMaterial.TransitionTexture = value; }
        /// <inheritdoc cref="TransitionalMaterial.TransitionProgress"/>
        public virtual float TransitionProgress { get => TextureMaterial.TransitionProgress; set => TextureMaterial.TransitionProgress = value; }
        /// <inheritdoc cref="TransitionalMaterial.TintColor"/>
        public virtual Color TintColor { get => TextureMaterial.TintColor; set => TextureMaterial.TintColor = value; }
        /// <inheritdoc cref="TransitionalMaterial.Opacity"/>
        public virtual float Opacity { get => TextureMaterial.Opacity; set => TextureMaterial.Opacity = value; }
        /// <summary>
        /// Whether to flip the content by X-axis.
        /// </summary>
        public virtual bool FlipX { get; set; }
        /// <summary>
        /// Whether to flip the content by Y-axis.
        /// </summary>
        public virtual bool FlipY { get; set; }
        /// <summary>
        /// Intensity of the gaussian blur effect to apply for the rendered target.
        /// </summary>
        public virtual float BlurIntensity { get; set; }
        /// <summary>
        /// Pivot of the textures inside render rectangle.
        /// </summary>
        public virtual Vector2 Pivot { get; set; } = new(.5f, .5f);

        private readonly Tweener<FloatTween> transitionTweener = new();
        private readonly Tweener<ColorTween> colorTweener = new();
        private readonly Tweener<FloatTween> fadeTweener = new();
        private readonly Tweener<FloatTween> blurTweener = new();

        private BlurFilter blurFilter;
        private float opacityLastFrame;

        /// <summary>
        /// Adds a transitional renderer component for the specified actor.
        /// </summary>
        /// <param name="premultipliedAlpha">Whether the content has already been rendered and has RGB multiplied by opacity.</param>
        public static TransitionalRenderer CreateFor (OrthoActorMetadata actorMetadata, GameObject actorObject, bool premultipliedAlpha)
        {
            if (actorMetadata.RenderTexture)
            {
                actorMetadata.RenderTexture.Clear();
                var textureRenderer = actorObject.AddComponent<TransitionalTextureRenderer>();
                textureRenderer.Initialize(premultipliedAlpha, actorMetadata.CustomTextureMaterial);
                textureRenderer.RenderTexture = actorMetadata.RenderTexture;
                textureRenderer.RenderRectangle = actorMetadata.RenderRectangle;
                return textureRenderer;
            }
            var spriteRenderer = actorObject.AddComponent<TransitionalSpriteRenderer>();
            var (matchMode, matchRatio) = actorMetadata is BackgroundMetadata backMeta
                ? (backMeta.MatchMode, backMeta.CustomMatchRatio)
                : (AspectMatchMode.Disable, 0);
            spriteRenderer.Initialize(actorMetadata.Pivot, actorMetadata.PixelsPerUnit, premultipliedAlpha, matchMode, matchRatio,
                actorMetadata.CustomTextureMaterial, actorMetadata.CustomSpriteMaterial);
            spriteRenderer.DepthPassEnabled = actorMetadata.EnableDepthPass;
            spriteRenderer.DepthAlphaCutoff = actorMetadata.DepthAlphaCutoff;
            return spriteRenderer;
        }

        /// <summary>
        /// Performs transition from <see cref="TransitionalMaterial.MainTexture"/> to the specified texture using specified animation tween.
        /// </summary>
        /// <param name="texture">Texture to transition into.</param>
        /// <param name="tween">Tween animation properties.</param>
        /// <param name="transition">Type of the transition effect to use.</param>
        public virtual async UniTask TransitionTo (Texture texture, Tween tween, Transition? transition = default, AsyncToken token = default)
        {
            if (transitionTweener.Running)
            {
                transitionTweener.CompleteInstantly();
                await AsyncUtils.WaitEndOfFrame(); // Materials are updated later in render loop, so wait before further modifications.
                token.ThrowIfCanceled(TextureMaterial.Object);
            }

            if (transition.HasValue)
                Transition = transition.Value;

            TransitionProgress = 0;

            if (tween.Duration <= 0)
            {
                MainTexture = texture;
                return;
            }

            if (!MainTexture) MainTexture = texture;
            TransitionTexture = texture;
            TextureMaterial.UpdateRandomSeed();
            var tweenValue = new FloatTween(0, 1, tween, value => TransitionProgress = value);
            await transitionTweener.RunAwaitable(tweenValue, token, TextureMaterial.Object);
            MainTexture = texture;
            TransitionProgress = 0;
            TransitionTexture = null;
        }

        /// <summary>
        /// Tints current texture to the specified color using specified animation tween.
        /// </summary>
        /// <param name="color">Color of the tint.</param>
        /// <param name="tween">Tween animation properties.</param>
        public virtual async UniTask TintTo (Color color, Tween tween, AsyncToken token = default)
        {
            if (colorTweener.Running) colorTweener.CompleteInstantly();

            if (tween.Duration <= 0)
            {
                TintColor = color;
                return;
            }

            var tweenValue = new ColorTween(TintColor, color, tween, ColorTweenMode.All, value => TintColor = value);
            await colorTweener.RunAwaitable(tweenValue, token, TextureMaterial.Object);
        }

        /// <summary>
        /// Same as tint, but applies only to the alpha component of the color.
        /// </summary>
        public virtual async UniTask FadeTo (float opacity, Tween tween, AsyncToken token = default)
        {
            if (fadeTweener.Running) fadeTweener.CompleteInstantly();

            if (tween.Duration <= 0)
            {
                Opacity = opacity;
                return;
            }

            var tweenValue = new FloatTween(Opacity, opacity, tween, value => Opacity = value);
            await fadeTweener.RunAwaitable(tweenValue, token, TextureMaterial.Object);
        }

        public virtual async UniTask FadeOut (Tween tween, AsyncToken token = default)
        {
            await FadeTo(0, tween, token);
        }

        public virtual async UniTask FadeIn (Tween tween, AsyncToken token = default)
        {
            await FadeTo(1, tween, token);
        }

        public virtual async UniTask Blur (float intensity, Tween tween, AsyncToken token = default)
        {
            if (blurTweener.Running) blurTweener.CompleteInstantly();

            if (tween.Duration <= 0)
            {
                BlurIntensity = intensity;
                return;
            }

            var tweenValue = new FloatTween(BlurIntensity, intensity, tween, value => BlurIntensity = value);
            await blurTweener.RunAwaitable(tweenValue, token, this);
        }

        public virtual CharacterLookDirection GetLookDirection (CharacterLookDirection bakedDirection) => bakedDirection switch {
            CharacterLookDirection.Center => CharacterLookDirection.Center,
            CharacterLookDirection.Left => FlipX ? CharacterLookDirection.Right : CharacterLookDirection.Left,
            CharacterLookDirection.Right => FlipX ? CharacterLookDirection.Left : CharacterLookDirection.Right,
            _ => default
        };

        public virtual void SetLookDirection (CharacterLookDirection direction, CharacterLookDirection bakedDirection)
        {
            if (bakedDirection == CharacterLookDirection.Center) return;
            if (direction == CharacterLookDirection.Center)
            {
                FlipX = false;
                return;
            }
            if (direction != GetLookDirection(bakedDirection)) FlipX = !FlipX;
        }

        public virtual async UniTask ChangeLookDirection (CharacterLookDirection direction, CharacterLookDirection bakedDirection,
            Tween tween, AsyncToken token = default)
        {
            var prevValue = GetLookDirection(bakedDirection);
            SetLookDirection(direction, bakedDirection);
            if (prevValue != GetLookDirection(bakedDirection) && tween.Duration > 0)
                await DoFlipX(tween, token);
        }

        /// <summary>
        /// Prepares the underlying systems for render.
        /// </summary>
        /// <param name="customMaterial">Material to use for rendering; will use a default one when not specified.</param>
        /// <param name="premultipliedAlpha">Whether the content has already been rendered and has RGB multiplied by opacity.</param>
        protected virtual void Initialize (bool premultipliedAlpha, Material customMaterial = default)
        {
            TextureMaterial = new(premultipliedAlpha, customMaterial);
            blurFilter = new(2, true);
        }

        protected virtual async UniTask DoFlipX (Tween tween, AsyncToken token = default)
        {
            if (tween.Duration <= 0) return;
            TextureMaterial.FlipMain = true;
            if (transitionTweener.Running)
                while (transitionTweener.Running && token.EnsureNotCanceledOrCompleted(this))
                    await AsyncUtils.WaitEndOfFrame();
            else await TransitionTo(MainTexture, tween, token: token);
            TextureMaterial.FlipMain = false;
        }

        protected virtual void OnDestroy ()
        {
            ObjectUtils.DestroyOrImmediate(TextureMaterial?.Object);
            blurFilter?.Dispose();
        }

        protected virtual bool ShouldRender ()
        {
            return TextureMaterial?.Object && MainTexture && opacityLastFrame > 0;
        }

        /// <summary>
        /// Renders <see cref="TextureMaterial"/> to the specified render texture.
        /// </summary>
        /// <param name="texture">The render target.</param>
        protected virtual void RenderToTexture (RenderTexture texture)
        {
            var renderSize = new Vector2(texture.width, texture.height);
            FitUVs(renderSize);
            DrawQuad(texture, renderSize);
            if (BlurIntensity > 0) blurFilter.BlurTexture(texture, BlurIntensity);
        }

        protected virtual void FitUVs (Vector2 renderSize)
        {
            var mainSize = new Vector2(MainTexture.width, MainTexture.height);
            (TextureMaterial.MainTextureOffset, TextureMaterial.MainTextureScale) = GetMainUVModifiers(renderSize, mainSize);
            if (!TransitionTexture) return;
            var transitionSize = new Vector2(TransitionTexture.width, TransitionTexture.height);
            (TextureMaterial.TransitionTextureOffset, TextureMaterial.TransitionTextureScale) = GetTransitionUVModifiers(renderSize, transitionSize);
        }

        protected virtual (Vector2 offset, Vector2 scale) GetMainUVModifiers (Vector2 renderSize, Vector2 textureSize)
        {
            if (renderSize == textureSize) return (Vector2.zero, Vector2.one);
            if (renderSize.x < textureSize.x || renderSize.y < textureSize.y)
                renderSize /= textureSize.x > textureSize.y ? renderSize.x / textureSize.x : renderSize.y / textureSize.y;
            var offset = (textureSize - renderSize) / textureSize * Pivot;
            var scale = renderSize / textureSize;
            return (offset, scale);
        }

        protected virtual (Vector2 offset, Vector2 scale) GetTransitionUVModifiers (Vector2 renderSize, Vector2 textureSize)
        {
            return GetMainUVModifiers(renderSize, textureSize);
        }

        protected virtual void DrawQuad (RenderTexture target, Vector2 size)
        {
            Graphics.SetRenderTarget(target);
            TextureMaterial.Object.SetPass(0);
            GL.Clear(true, true, Color.clear);
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, size.x, 0, size.y);
            GL.Begin(GL.QUADS);
            GL.TexCoord2(FlipX ? 1 : 0, FlipY ? 1 : 0);
            GL.Vertex3(0, 0, 0);
            GL.TexCoord2(FlipX ? 0 : 1, FlipY ? 1 : 0);
            GL.Vertex3(size.x, 0, 0);
            GL.TexCoord2(FlipX ? 0 : 1, FlipY ? 0 : 1);
            GL.Vertex3(size.x, size.y, 0);
            GL.TexCoord2(FlipX ? 1 : 0, FlipY ? 0 : 1);
            GL.Vertex3(0, size.y, 0);
            GL.End();
            GL.PopMatrix();
        }

        private void LateUpdate () => opacityLastFrame = Opacity;
    }
}
