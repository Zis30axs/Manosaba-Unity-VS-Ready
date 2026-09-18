#if SPRITE_DICING_AVAILABLE

using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel.FX;
using SpriteDicing;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="MonoBehaviourActor{TMeta}"/> using "SpriteDicing" extension to represent the actor.
    /// </summary>
    public abstract class DicedSpriteActor<TMeta> : MonoBehaviourActor<TMeta>, Blur.IBlurable
        where TMeta : OrthoActorMetadata
    {
        public override string Appearance { get => appearance; set => SetAppearance(value); }
        public override bool Visible { get => visible; set => SetVisibility(value); }

        protected virtual TransitionalRenderer TransitionalRenderer { get; private set; }

        private readonly OrthoActorMetadata meta;
        private readonly Material renderMaterial;
        private readonly Mesh renderMesh;
        private readonly List<Vector3> vertices = new();
        private readonly EmbeddedAppearanceLoader<DicedSpriteAtlas> atlasLoader;
        private RenderTexture appearanceTexture;
        private string appearance;
        private bool visible;

        protected DicedSpriteActor (string id, TMeta meta, EmbeddedAppearanceLoader<DicedSpriteAtlas> loader)
            : base(id, meta)
        {
            this.meta = meta;
            atlasLoader = loader;

            renderMaterial = new(Shader.Find("Sprites/Default"));
            renderMaterial.hideFlags = HideFlags.HideAndDontSave;

            renderMesh = new();
            renderMesh.hideFlags = HideFlags.HideAndDontSave;
            renderMesh.name = $"{id} Mesh";
            renderMesh.MarkDynamic();
        }

        public virtual UniTask Blur (float intensity, Tween tween, AsyncToken token = default)
        {
            return TransitionalRenderer.Blur(intensity, tween, token);
        }

        public override async UniTask Initialize ()
        {
            await base.Initialize();

            TransitionalRenderer = TransitionalRenderer.CreateFor(ActorMeta, GameObject, true);
            SetVisibility(false);
        }

        public override async UniTask ChangeAppearance (string appearance, Tween tween, Transition? transition = default, AsyncToken token = default)
        {
            this.appearance = appearance;
            var atlas = await GetOrLoadAtlas(token);
            if (this.appearance != appearance) return;
            var sprite = string.IsNullOrEmpty(appearance) ? GetDefaultSprite(atlas) : GetSprite(appearance, atlas);
            RebuildRenderMesh(sprite);
            var renderTexture = RenderToTexture(sprite);
            await TransitionalRenderer.TransitionTo(renderTexture, tween, transition, token);
            if (appearanceTexture) RenderTexture.ReleaseTemporary(appearanceTexture);
            appearanceTexture = renderTexture;
        }

        public override async UniTask ChangeVisibility (bool visible, Tween tween, AsyncToken token = default)
        {
            if (!Visible && visible && string.IsNullOrWhiteSpace(Appearance))
                await ChangeAppearance(null, tween, token: token);

            this.visible = visible;

            await TransitionalRenderer.FadeTo(visible ? TintColor.a : 0, tween, token);
        }

        public override void Dispose ()
        {
            if (appearanceTexture) RenderTexture.ReleaseTemporary(appearanceTexture);
            ObjectUtils.DestroyOrImmediate(renderMaterial);
            ObjectUtils.DestroyOrImmediate(renderMesh);

            atlasLoader?.ReleaseAll(this);

            base.Dispose();
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

        protected virtual Sprite GetDefaultSprite (DicedSpriteAtlas atlas)
        {
            var defaultSprite = atlas.Sprites.FirstOrDefault(s => s.name.EndsWith("Default", StringComparison.OrdinalIgnoreCase));
            return defaultSprite ? defaultSprite : atlas.Sprites.First();
        }

        protected virtual Sprite GetSprite (string appearance, DicedSpriteAtlas atlas)
        {
            // In case user stored source sprites in folders, the diced sprites will have dots in their names.
            var spriteName = appearance.Replace("/", ".");
            var dicedSprite = atlas.GetSprite(spriteName);
            if (dicedSprite is null) throw new Error($"Failed to get '{spriteName}' diced sprite for '{Id}' actor.");
            return dicedSprite;
        }

        protected virtual async UniTask<DicedSpriteAtlas> GetOrLoadAtlas (AsyncToken token)
        {
            if (atlasLoader.IsLoaded(Id)) return atlasLoader.GetLoaded(Id);
            var atlasResource = await atlasLoader.LoadOrErr(Id, this);
            token.ThrowIfCanceled();
            if (atlasResource.Object.Sprites.Count == 0)
                throw new Error($"'{Id}' diced sprite atlas is empty. Add at least one sprite and rebuild the atlas.");
            return atlasResource;
        }

        protected virtual void RebuildRenderMesh (Sprite dicedSprite)
        {
            vertices.Clear();
            foreach (var vertex in dicedSprite.vertices)
                vertices.Add(vertex);
            renderMesh.Clear();
            renderMesh.SetVertices(vertices);
            renderMesh.SetUVs(0, dicedSprite.uv);
            renderMesh.SetTriangles(dicedSprite.triangles, 0);
        }

        protected virtual RenderTexture RenderToTexture (Sprite dicedSprite)
        {
            var spriteRect = GetSpriteRect(dicedSprite);
            var renderTexture = GetRenderTexture(spriteRect, meta.PixelsPerUnit);
            var pivot = dicedSprite.pivot / spriteRect.size / dicedSprite.pixelsPerUnit;
            var drawPos = spriteRect.size * pivot - spriteRect.size / 2;
            var halfSize = spriteRect.size / 2f;
            var orthoMatrix = Matrix4x4.Ortho(-halfSize.x, halfSize.x, -halfSize.y, halfSize.y, 0f, 100f);
            Graphics.SetRenderTarget(renderTexture);
            GL.Clear(true, true, Color.clear);
            GL.PushMatrix();
            GL.LoadProjectionMatrix(orthoMatrix);
            renderMaterial.mainTexture = dicedSprite.texture;
            renderMaterial.SetPass(0);
            Graphics.DrawMeshNow(renderMesh, drawPos, Quaternion.identity);
            GL.PopMatrix();
            return renderTexture;
        }

        private static RenderTexture GetRenderTexture (Rect spriteRect, float ppu)
        {
            var renderWidth = Mathf.CeilToInt(spriteRect.width * ppu);
            var renderHeight = Mathf.CeilToInt(spriteRect.height * ppu);
            return RenderTexture.GetTemporary(renderWidth, renderHeight);
        }

        private static Rect GetSpriteRect (Sprite sprite)
        {
            var minVertPos = new Vector2(sprite.vertices.Min(v => v.x), sprite.vertices.Min(v => v.y));
            var maxVertPos = new Vector2(sprite.vertices.Max(v => v.x), sprite.vertices.Max(v => v.y));
            var spriteSizeX = Mathf.Abs(maxVertPos.x - minVertPos.x);
            var spriteSizeY = Mathf.Abs(maxVertPos.y - minVertPos.y);
            var spriteSize = new Vector2(spriteSizeX, spriteSizeY);
            return new(minVertPos, spriteSize);
        }
    }
}

#endif
