using System;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    /// <summary>
    /// Allows cross-fading <see cref="RawImage"/> textures.
    /// </summary>
    public class ImageCrossfader : IDisposable
    {
        private const string shaderName = "Naninovel/TransitionalUI";
        private static readonly int transitionTexId = Shader.PropertyToID("_TransitionTex");
        private static readonly int transitionProgressId = Shader.PropertyToID("_TransitionProgress");

        private Texture mainTexture { get => image.texture; set => image.texture = value ? value : Texture2D.blackTexture; }
        private Texture transitionTexture { get => material.GetTexture(transitionTexId); set => material.SetTexture(transitionTexId, value); }
        private float transitionProgress { get => material.GetFloat(transitionProgressId); set => material.SetFloat(transitionProgressId, value); }

        private readonly RawImage image;
        private readonly Material material;
        private readonly Tweener<FloatTween> tweener = new();

        public ImageCrossfader (RawImage image)
        {
            this.image = image;
            material = new(Shader.Find(shaderName));
            material.hideFlags = HideFlags.DontSaveInBuild | HideFlags.DontSaveInEditor;
            mainTexture = transitionTexture = null;
            image.material = material;
        }

        public void Dispose ()
        {
            ObjectUtils.DestroyOrImmediate(material);
        }

        public async UniTask Crossfade (Texture texture, float duration)
        {
            if (tweener.Running)
            {
                if (texture == transitionTexture) return;
                tweener.CompleteInstantly();
            }

            transitionTexture = texture;
            var tween = new FloatTween(transitionProgress, 1, new(duration), value => transitionProgress = value);
            await tweener.RunAwaitable(tween, target: material);

            if (material && image)
            {
                mainTexture = transitionTexture;
                transitionProgress = 0;
            }
        }
    }
}
