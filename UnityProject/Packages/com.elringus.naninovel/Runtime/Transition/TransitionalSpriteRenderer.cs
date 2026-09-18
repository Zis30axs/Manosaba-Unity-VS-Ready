using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="TransitionalRenderer"/> implementation, that outputs the result to a quad mesh (sprite).
    /// </summary>
    public class TransitionalSpriteRenderer : TransitionalRenderer
    {
        /// <summary>
        /// Material for rendering the sprite.
        /// </summary>
        public virtual Material SpriteMaterial { get; private set; }
        /// <summary>
        /// Material for rendering depth pass for the sprite.
        /// </summary>
        public virtual Material DepthMaterial { get; private set; }
        /// <summary>
        /// PPU to use when rendering the sprite.
        /// </summary>
        public virtual float PixelsPerUnit { get => pixelsPerUnit; set => SetPPU(value); }
        /// <summary>
        /// Bounds of the rendered sprite.
        /// </summary>
        public virtual Rect Bounds => spriteMesh ? new Rect(spriteMesh.bounds.min, spriteMesh.bounds.size) : default;
        /// <summary>
        /// Whether depth pass is currently enabled.
        /// </summary>
        public virtual bool DepthPassEnabled { get; set; }
        /// <summary>
        /// Current cutoff value of the depth pass.
        /// </summary>
        public virtual float DepthAlphaCutoff { get => DepthMaterial.GetFloat(depthCutoffId); set => DepthMaterial.SetFloat(depthCutoffId, value); }
        public override Vector2 Pivot { get => pivot; set => SetPivot(value); }

        private const string defaultSpriteShaderName = "Hidden/Naninovel/Transparent";
        private const string depthShaderName = "Hidden/Naninovel/DepthMask";
        private static readonly int depthCutoffId = Shader.PropertyToID("_DepthAlphaCutoff");
        private static readonly int opacityId = Shader.PropertyToID("_Opacity");

        private ICameraManager cameraManager;
        private TransitionalMatcher matcher;
        private float referencePPU;
        private TransitionalSpriteBuilder spriteBuilder;
        private Mesh spriteMesh;
        private RenderTexture renderTexture;
        private Vector2 pivot;
        private float pixelsPerUnit;

        /// <inheritdoc cref="TransitionalRenderer.Initialize"/>
        /// <param name="pivot">Pivot (anchors) of the sprite.</param>
        /// <param name="pixelsPerUnit">How many texture pixels correspond to one unit of the sprite geometry.</param>
        public virtual void Initialize (Vector2 pivot, float pixelsPerUnit, bool premultipliedAlpha, AspectMatchMode matchMode,
            float matchRatio, Material customMaterial = default, Material customSpriteMaterial = default)
        {
            base.Initialize(premultipliedAlpha, customMaterial);

            this.pivot = pivot;
            this.pixelsPerUnit = referencePPU = pixelsPerUnit;

            cameraManager = Engine.GetServiceOrErr<ICameraManager>();
            matcher = new(cameraManager, this);
            matcher.MatchMode = matchMode;
            matcher.CustomMatchRatio = matchRatio;

            spriteMesh = gameObject.AddComponent<MeshFilter>().sharedMesh = new();
            spriteMesh.hideFlags = HideFlags.DontSaveInBuild | HideFlags.DontSaveInEditor;
            spriteMesh.name = "Transitional Sprite Mesh";
            spriteMesh.MarkDynamic();
            spriteBuilder = new(spriteMesh);

            SpriteMaterial = customSpriteMaterial ? new(customSpriteMaterial) : new(Shader.Find(defaultSpriteShaderName));
            SpriteMaterial.hideFlags = HideFlags.HideAndDontSave;

            DepthMaterial = new(Shader.Find(depthShaderName));
            DepthMaterial.hideFlags = HideFlags.HideAndDontSave;
        }

        protected virtual void Update ()
        {
            if (!ShouldRender()) return;
            SpriteMaterial.SetFloat(opacityId, TintColor.a);
            PrepareRenderTexture();
            RenderToTexture(renderTexture);
            var matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            Graphics.DrawMesh(spriteMesh, matrix, SpriteMaterial, gameObject.layer);
            if (DepthPassEnabled) Graphics.DrawMesh(spriteMesh, matrix, DepthMaterial, gameObject.layer);
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();
            if (renderTexture) RenderTexture.ReleaseTemporary(renderTexture);
            ObjectUtils.DestroyOrImmediate(SpriteMaterial);
            ObjectUtils.DestroyOrImmediate(DepthMaterial);
        }

        protected virtual Vector2Int GetMeshSize ()
        {
            if (!TransitionTexture) return new(MainTexture.width, MainTexture.height);
            var width = Mathf.Max(TransitionTexture.width, MainTexture.width);
            var height = Mathf.Max(TransitionTexture.height, MainTexture.height);
            return new(width, height);
        }

        protected override (Vector2 offset, Vector2 scale) GetTransitionUVModifiers (Vector2 renderSize, Vector2 textureSize)
        {
            if (!cameraManager.Orthographic || matcher.MatchMode == AspectMatchMode.Disable)
                return base.GetTransitionUVModifiers(renderSize, textureSize);
            var currentSize = renderSize / PixelsPerUnit;
            var sizeAfterTransition = textureSize / (referencePPU / matcher.GetScaleFactor(textureSize / referencePPU));
            var modifier = currentSize / sizeAfterTransition;
            return ((Vector2.one - modifier) * Pivot, modifier);
        }

        private void PrepareRenderTexture ()
        {
            var size = GetMeshSize();
            if (renderTexture && renderTexture.width == size.x && renderTexture.height == size.y) return;
            if (renderTexture) RenderTexture.ReleaseTemporary(renderTexture);
            renderTexture = RenderTexture.GetTemporary(size.x, size.y);
            SpriteMaterial.mainTexture = renderTexture;
            DepthMaterial.mainTexture = renderTexture;
            BuildMesh();
        }

        private void BuildMesh ()
        {
            if (!MainTexture) return;
            spriteBuilder.Build(GetMeshSize(), Pivot, PixelsPerUnit);
        }

        private void SetPivot (Vector2 value)
        {
            if (value == Pivot) return;
            pivot = value;
            BuildMesh();
        }

        private void SetPPU (float value)
        {
            if (Mathf.Approximately(value, PixelsPerUnit)) return;
            pixelsPerUnit = value;
            BuildMesh();
        }
    }
}
