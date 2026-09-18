using UnityEngine;

namespace Naninovel
{
    public class TransitionalMaterial
    {
        /// <summary>
        /// Current main texture.
        /// </summary>
        public Texture MainTexture { get => Object.mainTexture; set => Object.mainTexture = value; }
        /// <summary>
        /// UV offset of the main texture.
        /// </summary>
        public virtual Vector2 MainTextureOffset { get => Object.mainTextureOffset; set => Object.mainTextureOffset = value; }
        /// <summary>
        /// UV scale of the main texture.
        /// </summary>
        public virtual Vector2 MainTextureScale { get => Object.mainTextureScale; set => Object.mainTextureScale = value; }
        /// <summary>
        /// Current texture that is used to transition from <see cref="MainTexture"/>.
        /// </summary>
        public Texture TransitionTexture { get => Object.GetTexture(transitionTexId); set => Object.SetTexture(transitionTexId, value); }
        /// <summary>
        /// UV offset of the transition texture.
        /// </summary>
        public virtual Vector2 TransitionTextureOffset { get => Object.GetTextureOffset(transitionTexId); set => Object.SetTextureOffset(transitionTexId, value); }
        /// <summary>
        /// UV scale of the transition texture.
        /// </summary>
        public virtual Vector2 TransitionTextureScale { get => Object.GetTextureScale(transitionTexId); set => Object.SetTextureScale(transitionTexId, value); }
        /// <summary>
        /// Texture used in a custom dissolve transition type.
        /// </summary>
        public Texture DissolveTexture { get => Object.GetTexture(dissolveTexId); set => Object.SetTexture(dissolveTexId, value); }
        /// <summary>
        /// Name of the current transition type.
        /// </summary>
        public string TransitionName { get => TransitionUtils.GetEnabled(Object); set => TransitionUtils.EnableKeyword(Object, value); }
        /// <summary>
        /// Current transition progress between <see cref="MainTexture"/> and <see cref="TransitionTexture"/>, in 0.0 to 1.0 range.
        /// </summary>
        public float TransitionProgress { get => Object.GetFloat(transitionProgressId); set => Object.SetFloat(transitionProgressId, value); }
        /// <summary>
        /// Parameters of the current transition.
        /// </summary>
        public Vector4 TransitionParams { get => Object.GetVector(transitionParamsId); set => Object.SetVector(transitionParamsId, value); }
        /// <summary>
        /// Current random seed used in some transition types.
        /// </summary>
        public Vector2 RandomSeed { get => Object.GetVector(randomSeedId); set => Object.SetVector(randomSeedId, value); }
        /// <summary>
        /// Current tint color.
        /// </summary>
        public Color TintColor { get => Object.GetColor(tintColorId); set => Object.SetColor(tintColorId, value); }
        /// <summary>
        /// Current alpha component of <see cref="TintColor"/>.
        /// </summary>
        public float Opacity { get => Object.GetColor(tintColorId).a; set => SetOpacity(value); }
        /// <summary>
        /// Whether main texture is flipped by X-axis.
        /// </summary>
        public bool FlipMain { get => Mathf.Approximately(Object.GetFloat(flipMainId), 1); set => Object.SetFloat(flipMainId, value ? 1 : 0); }
        /// <summary>
        /// The underlying Unity material object.
        /// </summary>
        public Material Object { get; }

        private const string defaultShaderName = "Naninovel/TransitionalTexture";
        private const string cloudsTexturePath = "Textures/Clouds";
        private const string premultipliedAlphaKey = "PREMULTIPLIED_ALPHA";
        private static readonly int mainTexId = Shader.PropertyToID("_MainTex");
        private static readonly int transitionTexId = Shader.PropertyToID("_TransitionTex");
        private static readonly int cloudsTexId = Shader.PropertyToID("_CloudsTex");
        private static readonly int dissolveTexId = Shader.PropertyToID("_DissolveTex");
        private static readonly int transitionProgressId = Shader.PropertyToID("_TransitionProgress");
        private static readonly int transitionParamsId = Shader.PropertyToID("_TransitionParams");
        private static readonly int randomSeedId = Shader.PropertyToID("_RandomSeed");
        private static readonly int tintColorId = Shader.PropertyToID("_TintColor");
        private static readonly int flipMainId = Shader.PropertyToID("_FlipMainX");

        private static Texture2D sharedCloudsTexture;

        public TransitionalMaterial (bool premultipliedAlpha, Material custom = default, HideFlags hideFlags = HideFlags.HideAndDontSave)
        {
            Object = custom ? new(custom) : new(Shader.Find(defaultShaderName));
            if (!sharedCloudsTexture)
                sharedCloudsTexture = Engine.LoadInternalResource<Texture2D>(cloudsTexturePath);
            if (premultipliedAlpha)
                Object.EnableKeyword(premultipliedAlphaKey);
            Object.SetTexture(cloudsTexId, sharedCloudsTexture);
            Object.hideFlags = hideFlags;
        }

        /// <summary>
        /// Regenerate current value of <see cref="RandomSeed"/>.
        /// </summary>
        public void UpdateRandomSeed ()
        {
            var sinTime = Mathf.Sin(Engine.Time.UnscaledTime);
            var cosTime = Mathf.Cos(Engine.Time.UnscaledTime);
            RandomSeed = new(Mathf.Abs(sinTime), Mathf.Abs(cosTime));
        }

        private void SetOpacity (float value)
        {
            var color = TintColor;
            color.a = value;
            TintColor = color;
        }
    }
}
