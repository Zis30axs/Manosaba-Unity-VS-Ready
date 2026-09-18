using System.Collections.Generic;
using System.Linq;
using Naninovel.FX;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="IBackgroundActor"/> implementation using <see cref="Scene"/> to represent the actor.
    /// </summary>
    /// <remarks>
    /// The implementation requires scene assets associates with the actor appearances to be located at the
    /// scenes path root specified in the actor configuration ('Assets/Scenes' by default); additionally the
    /// scenes have to be added to the build settings. Resource providers are not supported.
    /// </remarks>
    #if UNITY_EDITOR
    [ActorResources(typeof(UnityEditor.SceneAsset), true)]
    #endif
    public class SceneBackground : MonoBehaviourActor<BackgroundMetadata>, IBackgroundActor, Blur.IBlurable
    {
        protected class SceneData
        {
            public Scene Scene;
            public RenderTexture RenderTexture;
            public Camera Camera;
            public readonly HashSet<object> Holders = new();
        }

        public override string Appearance { get => appearance; set => SetAppearance(value); }
        public override bool Visible { get => visible; set => SetVisibility(value); }

        protected virtual TransitionalRenderer TransitionalRenderer { get; private set; }

        private static readonly Dictionary<string, SceneData> loadedScenes = new();
        private static readonly Semaphore loadSemaphore = new(1, 1);

        private BackgroundMatcher matcher;
        private string appearance;
        private bool visible;

        public SceneBackground (string id, BackgroundMetadata meta)
            : base(id, meta) { }

        public override async UniTask Initialize ()
        {
            await base.Initialize();

            TransitionalRenderer = TransitionalRenderer.CreateFor(ActorMeta, GameObject, false);
            matcher = BackgroundMatcher.CreateFor(ActorMeta, TransitionalRenderer);

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
            if (string.IsNullOrEmpty(appearance)) return;
            var data = await GetOrLoadScene(appearance, token.CancellationToken);
            data.Holders.Add(this);
            await TransitionalRenderer.TransitionTo(data.RenderTexture, tween, transition, token);

            if (!string.IsNullOrEmpty(previousAppearance) && previousAppearance != this.appearance &&
                loadedScenes.TryGetValue(previousAppearance, out var previousData) &&
                previousData.Holders.Remove(this) && previousData.Holders.Count == 0)
                UnloadScene(previousAppearance);
        }

        public override async UniTask ChangeVisibility (bool visible, Tween tween, AsyncToken token = default)
        {
            this.visible = visible;
            await TransitionalRenderer.FadeTo(visible ? 1 : 0, tween, token);
        }

        public override void Dispose ()
        {
            base.Dispose();
            matcher?.Stop();
            foreach (var appearance in loadedScenes.Keys.ToArray())
                UnloadUnused(appearance);

            void UnloadUnused (string appearance)
            {
                var data = loadedScenes[appearance];
                data.Holders.Remove(this);
                if (data.Holders.Count == 0) UnloadScene(appearance);
            }
        }

        protected virtual void SetAppearance (string appearance) => ChangeAppearance(appearance, new(0)).Forget();

        protected virtual void SetVisibility (bool visible) => ChangeVisibility(visible, new(0)).Forget();

        protected override Color GetBehaviourTintColor () => TransitionalRenderer.TintColor;

        protected override void SetBehaviourTintColor (Color tintColor)
        {
            if (!Visible) tintColor.a = TransitionalRenderer.TintColor.a;
            TransitionalRenderer.TintColor = tintColor;
        }

        protected virtual async UniTask<SceneData> GetOrLoadScene (string appearance, AsyncToken token)
        {
            // Using lock as it can be invoked multiple times
            // without awaiting when preloading script resources.
            await loadSemaphore.Wait(token.CancellationToken);
            token.ThrowIfCanceled();
            if (loadedScenes.TryGetValue(appearance, out var loadedSceneData))
            {
                loadSemaphore.Release();
                return loadedSceneData;
            }
            var renderTexture = CreateRenderTexture();
            var scene = await LoadScene(appearance, token);
            var camera = FindCameraInScene(scene);
            camera.targetTexture = renderTexture;
            var data = new SceneData { Scene = scene, RenderTexture = renderTexture, Camera = camera };
            loadedScenes[appearance] = data;
            loadSemaphore.Release();
            return data;
        }

        protected virtual RenderTexture CreateRenderTexture ()
        {
            var resolution = Engine.GetConfiguration<CameraConfiguration>().ReferenceResolution;
            var descriptor = new RenderTextureDescriptor(resolution.x, resolution.y, RenderTextureFormat.Default, 16);
            return new(descriptor);
        }

        protected virtual async UniTask<Scene> LoadScene (string appearance, AsyncToken token)
        {
            var scenePath = $"{ActorMeta.ScenePathRoot}/{appearance}.unity";
            await SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            token.ThrowIfCanceled();
            var scene = SceneManager.GetSceneByPath(scenePath);
            if (!scene.isLoaded)
                throw new Error($"Failed loading '{scenePath}' scene for '{Id}' scene background actor. " +
                                $"Make sure the scene asset is located at '{ActorMeta.ScenePathRoot}' and is added to the build settings.");
            return scene;
        }

        protected virtual void UnloadScene (string appearance)
        {
            if (!loadedScenes.Remove(appearance, out var data)) return;
            if (data.Camera) data.Camera.targetTexture = null;
            ObjectUtils.DestroyOrImmediate(data.RenderTexture);
            SceneManager.UnloadSceneAsync(data.Scene);
        }

        protected virtual Camera FindCameraInScene (Scene scene)
        {
            var rootObjects = scene.GetRootGameObjects();
            var camera = default(Camera);
            for (int i = 0; i < rootObjects.Length; i++)
                if (rootObjects[i].TryGetComponent<Camera>(out camera))
                    break;
            if (!camera)
                throw new Error($"Camera is not found inside '{scene.path}' scene of '{Id}' scene background actor. " +
                                $"Make sure a camera component is attached to a root game object of the scene.");
            return camera;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStaticData () => loadedScenes.Clear();
    }
}
