using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    /// <summary>
    /// Represents image of the current text message author (character) avatar.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class AuthorImage : ScriptableUIBehaviour
    {
        private ImageCrossfader crossfader;

        /// <summary>
        /// Cross-fades current image's texture with the specified one over <see cref="ScriptableUIBehaviour.FadeTime"/>.
        /// When null is specified, will hide the image instead.
        /// </summary>
        public virtual UniTask ChangeTexture (Texture texture)
        {
            return crossfader?.Crossfade(texture, FadeTime) ?? UniTask.CompletedTask;
        }

        protected override void Awake ()
        {
            base.Awake();

            if (TryGetComponent<RawImage>(out var image))
                crossfader = new(image);
        }

        protected override void OnDestroy ()
        {
            crossfader?.Dispose();
            base.OnDestroy();
        }
    }
}
