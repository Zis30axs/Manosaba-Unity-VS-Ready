using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    public class CGViewerPanel : ScriptableButton
    {
        protected virtual string ShaderName { get; } = "Naninovel/TransitionalUI";
        protected virtual RawImage ContentImage => contentImage;
        protected virtual float CrossfadeDuration => crossfadeDuration;
        protected virtual Queue<Texture2D> TextureQueue { get; } = new();
        protected virtual ImageCrossfader Crossfader { get; private set; }

        [Tooltip("The image where the assigned CGs will be shown.")]
        [SerializeField] private RawImage contentImage;
        [Tooltip("When multiple CGs assigned, controls crossfade duration, in seconds.")]
        [SerializeField] private float crossfadeDuration = .3f;

        public virtual void Show (IEnumerable<Texture2D> textures)
        {
            contentImage.texture = null;
            EnqueueTextures(textures);
            ShowNextTexture(0);
            base.Show();
        }

        public virtual void ShowNextOrHide ()
        {
            if (TextureQueue.Count > 0)
                ShowNextTexture(CrossfadeDuration);
            else Hide();
        }

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(ContentImage);
            Crossfader = new(ContentImage);
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();
            Crossfader?.Dispose();
        }

        protected override void OnButtonClick ()
        {
            ShowNextOrHide();
        }

        protected override void HandleVisibilityChanged (bool visible)
        {
            base.HandleVisibilityChanged(visible);
            if (visible) EventUtils.Select(gameObject);
            else Engine.GetServiceOrErr<IUIManager>().FocusTop();
        }

        protected virtual void EnqueueTextures (IEnumerable<Texture2D> textures)
        {
            TextureQueue.Clear();
            foreach (var texture in textures)
                if (texture)
                    TextureQueue.Enqueue(texture);
        }

        protected virtual void ShowNextTexture (float duration)
        {
            var texture = TextureQueue.Dequeue();
            Crossfader.Crossfade(texture, duration).Forget();
        }
    }
}
