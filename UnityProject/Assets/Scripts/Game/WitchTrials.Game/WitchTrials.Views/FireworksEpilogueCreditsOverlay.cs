using Naninovel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace WitchTrials.Views
{
    /// <summary>Displays the original poster footer without transcribing names or changing its pixels.</summary>
    public sealed class FireworksEpilogueCreditsOverlay : MonoBehaviour
    {
        public const string ObjectName = "FireworksEpilogueCredits";
        public const string TextureAddress = "Naninovel/FireworksEpilogue/SourcePoster";
        public bool Visible => group && group.alpha > 0 && gameObject.activeInHierarchy;

        private CanvasGroup group;
        private AsyncOperationHandle<Texture2D> textureHandle;
        private readonly Tweener<FloatTween> fade = new Tweener<FloatTween>();

        public static FireworksEpilogueCreditsOverlay Create(AsyncOperationHandle<Texture2D> handle)
        {
            var go = new GameObject(ObjectName, typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(CanvasGroup));
            var overlay = go.AddComponent<FireworksEpilogueCreditsOverlay>();
            overlay.textureHandle = handle;
            overlay.group = go.GetComponent<CanvasGroup>();
            overlay.group.interactable = false;
            overlay.group.blocksRaycasts = false;
            var canvas = go.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 32000;
            var scaler = go.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.matchWidthOrHeight = .5f;

            var imageGo = new GameObject("OriginalPosterFooter", typeof(RectTransform), typeof(RawImage), typeof(AspectRatioFitter));
            imageGo.transform.SetParent(go.transform, false);
            var rect = (RectTransform)imageGo.transform;
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(.5f, 1);
            rect.anchoredPosition = new Vector2(0, -12);
            rect.sizeDelta = new Vector2(-32, 0);
            var raw = imageGo.GetComponent<RawImage>();
            raw.texture = handle.Result;
            raw.raycastTarget = false;
            // Reviewed original: 1260 x 709. The complete black credit strip is y=670..708
            // in top-origin image coordinates; RawImage UVs start at the bottom.
            raw.uvRect = new Rect(0, 0, 1, 39f / 709f);
            var aspect = imageGo.GetComponent<AspectRatioFitter>();
            aspect.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
            aspect.aspectRatio = 1260f / 39f;
            return overlay;
        }

        public UniTask FadeOut(float duration, AsyncToken token)
        {
            return fade.RunAwaitable(new FloatTween(group.alpha, 0, new Tween(duration), value =>
            {
                if (group) group.alpha = value;
            }), token, this);
        }

        private void OnDisable() => fade.Stop();
        private void OnDestroy()
        {
            fade.Stop();
            if (textureHandle.IsValid()) Addressables.Release(textureHandle);
        }
    }
}
