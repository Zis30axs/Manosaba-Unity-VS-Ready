using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    public class SceneTransitionUI : CustomUI, ISceneTransitionUI
    {
        protected RawImage Image => image;

        [SerializeField] private RawImage image;

        private readonly Tweener<FloatTween> transitionTweener = new();
        private ICameraManager cameraManager;
        private TransitionalMaterial material;
        private RenderTexture sceneTexture;

        public virtual async UniTask CaptureScene ()
        {
            if (sceneTexture) RenderTexture.ReleaseTemporary(sceneTexture);
            sceneTexture = RenderTexture.GetTemporary(cameraManager.Camera.scaledPixelWidth, cameraManager.Camera.scaledPixelHeight, 16);

            await AsyncUtils.WaitEndOfFrame();
            var initialRenderTexture = cameraManager.Camera.targetTexture;
            cameraManager.Camera.targetTexture = sceneTexture;
            cameraManager.Camera.Render();
            cameraManager.Camera.targetTexture = initialRenderTexture;

            material.TransitionProgress = 0;
            material.MainTexture = sceneTexture;
            image.texture = sceneTexture;
            SetVisibility(true);
        }

        public virtual async UniTask Transition (Transition transition, Tween tween, AsyncToken token = default)
        {
            if (transitionTweener.Running)
                transitionTweener.CompleteInstantly();

            material.UpdateRandomSeed();
            material.TransitionProgress = 0;
            material.TransitionName = transition.Name;
            material.TransitionParams = transition.Parameters;
            if (transition.DissolveTexture)
                material.DissolveTexture = transition.DissolveTexture;

            var transitionTexture = RenderTexture.GetTemporary(cameraManager.Camera.scaledPixelWidth, cameraManager.Camera.scaledPixelHeight, 16);
            var initialRenderTexture = cameraManager.Camera.targetTexture;
            cameraManager.Camera.targetTexture = transitionTexture;
            material.TransitionTexture = transitionTexture;

            var tweenValue = new FloatTween(material.TransitionProgress, 1, tween, value => material.TransitionProgress = value);
            try { await transitionTweener.RunAwaitable(tweenValue, token, material.Object); }
            catch (AsyncOperationCanceledException)
            {
                // Try restore camera target texture before cancellation, otherwise it'll mess when rolling back.
                if (cameraManager != null && cameraManager.Camera)
                    cameraManager.Camera.targetTexture = initialRenderTexture;
                RenderTexture.ReleaseTemporary(transitionTexture);
                throw new AsyncOperationCanceledException(token);
            }

            cameraManager.Camera.targetTexture = initialRenderTexture;
            SetVisibility(false);
            RenderTexture.ReleaseTemporary(transitionTexture);

            // In case of rollbacks, revert to the original scene texture.
            material.TransitionProgress = 0;
        }

        protected override void Awake ()
        {
            base.Awake();

            this.AssertRequiredObjects(image);

            cameraManager = Engine.GetServiceOrErr<ICameraManager>();
            material = new(true);
            image.material = material.Object;
        }

        protected override void OnDestroy ()
        {
            if (sceneTexture)
                RenderTexture.ReleaseTemporary(sceneTexture);
            ObjectUtils.DestroyOrImmediate(material?.Object);

            base.OnDestroy();
        }
    }
}
