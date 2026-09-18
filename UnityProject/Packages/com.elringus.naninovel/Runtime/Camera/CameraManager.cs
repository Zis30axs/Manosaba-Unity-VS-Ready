using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Naninovel.UI;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="ICameraManager"/>
    /// <remarks>Initialization order lowered, so the user could see something while waiting for the engine initialization.</remarks>
    [InitializeAtRuntime(-1)]
    public class CameraManager : ICameraManager, IStatefulService<GameStateMap>, IStatefulService<SettingsStateMap>
    {
        [Serializable]
        public class Settings
        {
            public int QualityLevel = -1;
        }

        [Serializable]
        public class GameState
        {
            public Vector3 Offset = Vector3.zero;
            public Quaternion Rotation = Quaternion.identity;
            public float Zoom;
            public bool Orthographic = true;
            public CameraLookState LookMode;
            public CameraComponentState[] CameraComponents;
            public bool RenderUI = true;
        }

        public virtual CameraConfiguration Configuration { get; }
        public virtual Camera Camera { get; protected set; }
        public virtual Camera UICamera { get; protected set; }
        public virtual bool RenderUI { get => GetRenderUI(); set => SetRenderUI(value); }
        public virtual Vector3 Offset { get => offset; set => SetOffset(value); }
        public virtual Quaternion Rotation { get => rotation; set => SetRotation(value); }
        public virtual float Zoom { get => zoom; set => SetZoom(value); }
        public virtual bool Orthographic { get => Camera.orthographic; set => SetOrthographic(value); }
        public virtual int QualityLevel { get => QualitySettings.GetQualityLevel(); set => QualitySettings.SetQualityLevel(value, true); }

        protected virtual CameraLookController LookController { get; private set; }

        private readonly IInputManager inputManager;
        private readonly IEngineBehaviour engineBehaviour;
        private readonly RenderTexture thumbnailRenderTexture;
        private readonly List<MonoBehaviour> cameraComponentsCache = new();
        private readonly Tweener<VectorTween> offsetTweener = new();
        private readonly Tweener<VectorTween> rotationTweener = new();
        private readonly Tweener<FloatTween> zoomTweener = new();
        private IReadOnlyCollection<CameraComponentState> initialComponentState;
        private GameObject serviceObject;
        private Transform lookContainer;
        private Vector3 offset = Vector3.zero;
        private Quaternion rotation = Quaternion.identity;
        private float zoom;
        private float initialOrthoSize, initialFOV;
        private int uiLayer;

        public CameraManager (CameraConfiguration config, IInputManager inputManager, IEngineBehaviour engineBehaviour)
        {
            Configuration = config;
            this.inputManager = inputManager;
            this.engineBehaviour = engineBehaviour;

            thumbnailRenderTexture = new(config.ThumbnailResolution.x, config.ThumbnailResolution.y, 24);
        }

        public virtual async UniTask InitializeService ()
        {
            uiLayer = Engine.GetConfiguration<UIConfiguration>().ObjectsLayer;
            serviceObject = Engine.CreateObject(nameof(CameraManager));
            lookContainer = Engine.CreateObject("MainCameraLookContainer", parent: serviceObject.transform).transform;
            lookContainer.position = Configuration.InitialPosition;
            Camera = await InitializeMainCamera(Configuration, lookContainer, uiLayer);
            initialComponentState = GetComponentState(Camera);
            initialOrthoSize = Camera.orthographicSize;
            initialFOV = Camera.fieldOfView;
            if (Configuration.UseUICamera)
                UICamera = await InitializeUICamera(Configuration, serviceObject.transform, uiLayer);
            LookController = new(Camera.transform, inputManager.GetCameraLookX(), inputManager.GetCameraLookY());
            engineBehaviour.OnBehaviourUpdate += LookController.Update;
        }

        public virtual void ResetService ()
        {
            LookController.Enabled = false;
            Offset = Vector3.zero;
            Rotation = Quaternion.identity;
            Zoom = 0f;
            Orthographic = !Configuration.CustomCameraPrefab || Configuration.CustomCameraPrefab.orthographic;
            ApplyComponentState(Camera, initialComponentState);
        }

        public virtual void DestroyService ()
        {
            if (engineBehaviour != null)
                engineBehaviour.OnBehaviourUpdate -= LookController.Update;

            ObjectUtils.DestroyOrImmediate(thumbnailRenderTexture);
            ObjectUtils.DestroyOrImmediate(serviceObject);
        }

        public virtual void SaveServiceState (SettingsStateMap stateMap)
        {
            var settings = new Settings {
                QualityLevel = QualityLevel
            };
            stateMap.SetState(settings);
        }

        public virtual UniTask LoadServiceState (SettingsStateMap stateMap)
        {
            var settings = stateMap.GetState<Settings>() ?? new Settings();
            if (settings.QualityLevel >= 0 && settings.QualityLevel != QualityLevel)
                QualityLevel = settings.QualityLevel;

            return UniTask.CompletedTask;
        }

        public virtual void SaveServiceState (GameStateMap stateMap)
        {
            var gameState = new GameState {
                Offset = Offset,
                Rotation = Rotation,
                Zoom = Zoom,
                Orthographic = Orthographic,
                LookMode = LookController.GetState(),
                RenderUI = RenderUI,
                CameraComponents = GetComponentState(Camera)
            };
            stateMap.SetState(gameState);
        }

        public virtual UniTask LoadServiceState (GameStateMap stateMap)
        {
            var state = stateMap.GetState<GameState>();
            if (state is null)
            {
                ResetService();
                return UniTask.CompletedTask;
            }

            Offset = state.Offset;
            Rotation = state.Rotation;
            Zoom = state.Zoom;
            Orthographic = state.Orthographic;
            RenderUI = state.RenderUI;
            SetLookMode(state.LookMode.Enabled, state.LookMode.Zone, state.LookMode.Speed, state.LookMode.Gravity);
            ApplyComponentState(Camera, state.CameraComponents);
            return UniTask.CompletedTask;
        }

        public virtual void SetLookMode (bool enabled, Vector2 lookZone, Vector2 lookSpeed, bool gravity)
        {
            LookController.LookZone = lookZone;
            LookController.LookSpeed = lookSpeed;
            LookController.Gravity = gravity;
            LookController.Enabled = enabled;
        }

        public virtual Texture2D CaptureThumbnail ()
        {
            if (Configuration.HideUIInThumbnails)
                RenderUI = false;

            using var _ = ListPool<IManagedUI>.Rent(out var uis);
            Engine.GetServiceOrErr<IUIManager>().GetManagedUIs(uis);
            using var __ = ListPool<Canvas>.Rent(out var disabledCanvases);
            foreach (var ui in uis)
                if (ui is CustomUI { HideInThumbnail: true, TopmostCanvas: { enabled: true } canvas })
                {
                    canvas.enabled = false;
                    disabledCanvases.Add(canvas);
                }

            var initialRenderTexture = Camera.targetTexture;

            #if URP_AVAILABLE
            CaptureUI();
            CaptureMain();
            #else
            CaptureMain();
            CaptureUI();
            #endif

            var thumbnail = thumbnailRenderTexture.ToTexture2D();

            foreach (var canvas in disabledCanvases)
                canvas.enabled = true;

            if (Configuration.HideUIInThumbnails)
                RenderUI = true;

            return thumbnail;

            void CaptureMain ()
            {
                Camera.targetTexture = thumbnailRenderTexture;
                ForceTransitionalSpritesUpdate();
                Camera.Render();
                Camera.targetTexture = initialRenderTexture;

                void ForceTransitionalSpritesUpdate ()
                {
                    var updateMethod = typeof(TransitionalSpriteRenderer).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (updateMethod is null) throw new Error("Failed to locate 'Update' method of transitional sprite renderer.");
                    var sprites = UnityEngine.Object.FindObjectsByType<TransitionalSpriteRenderer>(FindObjectsSortMode.None);
                    foreach (var sprite in sprites)
                        updateMethod.Invoke(sprite, null);
                }
            }

            void CaptureUI ()
            {
                if (!RenderUI || !Configuration.UseUICamera) return;
                initialRenderTexture = UICamera.targetTexture;
                UICamera.targetTexture = thumbnailRenderTexture;
                UICamera.Render();
                UICamera.targetTexture = initialRenderTexture;
            }
        }

        public virtual async UniTask ChangeOffset (Vector3 offset, Tween tween, AsyncToken token = default)
        {
            CompleteOffsetTween();

            if (tween.Duration > 0)
            {
                this.offset = offset;
                var tweenValue = new VectorTween(GetCameraOffset(), offset, tween, SetCameraOffset);
                await offsetTweener.RunAwaitable(tweenValue, token, Camera);
            }
            else Offset = offset;
        }

        public virtual async UniTask ChangeRotation (Quaternion rotation, Tween tween, AsyncToken token = default)
        {
            CompleteRotationTween();

            if (tween.Duration > 0)
            {
                this.rotation = rotation;
                var tweenValue = new VectorTween(GetCameraRotation().ClampedEulerAngles(), rotation.ClampedEulerAngles(), tween, SetCameraRotation);
                await rotationTweener.RunAwaitable(tweenValue, token, Camera);
            }
            else Rotation = rotation;
        }

        public virtual async UniTask ChangeZoom (float zoom, Tween tween, AsyncToken token = default)
        {
            CompleteZoomTween();

            if (tween.Duration > 0)
            {
                this.zoom = zoom;
                var tweenValue = new FloatTween(GetCameraZoom(), zoom, tween, SetCameraZoom);
                await zoomTweener.RunAwaitable(tweenValue, token, Camera);
            }
            else Zoom = zoom;
        }

        protected virtual async UniTask<Camera> InitializeMainCamera (CameraConfiguration config, Transform parent, int uiLayer)
        {
            if (config.CustomCameraPrefab)
            {
                var customCamera = await Engine.Instantiate(config.CustomCameraPrefab, parent: parent);
                customCamera.transform.localPosition = Vector3.zero; // Position is controlled via look container.
                return customCamera;
            }

            var camera = Engine.CreateObject<Camera>("MainCamera", parent: parent);
            camera.transform.localPosition = Vector3.zero;
            camera.depth = 0;
            camera.backgroundColor = new Color32(25, 25, 25, 255);
            camera.orthographic = true;
            camera.orthographicSize = config.SceneRect.height / 2;
            camera.fieldOfView = 60f;
            camera.useOcclusionCulling = false;
            if (!config.UseUICamera)
                camera.allowHDR = false; // Otherwise text artifacts appear when printing.
            if (Engine.Configuration.OverrideObjectsLayer) // When culling is enabled, render only the engine object and UI (when not using UI camera) layers.
                camera.cullingMask = config.UseUICamera ? 1 << Engine.Configuration.ObjectsLayer : (1 << Engine.Configuration.ObjectsLayer) | (1 << uiLayer);
            else if (config.UseUICamera) camera.cullingMask = ~(1 << uiLayer);
            return camera;
        }

        protected virtual async UniTask<Camera> InitializeUICamera (CameraConfiguration config, Transform parent, int uiLayer)
        {
            if (config.CustomUICameraPrefab)
            {
                var customCamera = await Engine.Instantiate(config.CustomUICameraPrefab, parent: parent);
                customCamera.transform.position = config.InitialPosition;
                ConfigureUICameraForURP(customCamera);
                return customCamera;
            }

            var camera = Engine.CreateObject<Camera>("UICamera", parent: parent);
            camera.depth = 1;
            camera.orthographic = true;
            camera.allowHDR = false; // Otherwise text artifacts appear when printing.
            camera.cullingMask = 1 << uiLayer;
            camera.clearFlags = CameraClearFlags.Depth;
            camera.useOcclusionCulling = false;
            camera.transform.position = config.InitialPosition;
            ConfigureUICameraForURP(camera);
            return camera;
        }

        protected virtual void ConfigureUICameraForURP (Camera camera)
        {
            #if URP_AVAILABLE
            if (!UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline) return;
            var uiData = UnityEngine.Rendering.Universal.CameraExtensions.GetUniversalAdditionalCameraData(camera);
            uiData.renderType = UnityEngine.Rendering.Universal.CameraRenderType.Overlay;
            var mainData = UnityEngine.Rendering.Universal.CameraExtensions.GetUniversalAdditionalCameraData(Camera);
            mainData.cameraStack.Add(camera);
            #endif
        }

        protected virtual CameraComponentState[] GetComponentState (Camera camera)
        {
            camera.GetComponents(cameraComponentsCache);
            // Why zero? Camera is not a MonoBehaviour, so don't count it; others are considered custom effect.
            if (cameraComponentsCache.Count == 0) return Array.Empty<CameraComponentState>();
            return cameraComponentsCache.Select(c => new CameraComponentState(c)).ToArray();
        }

        protected virtual void ApplyComponentState (Camera camera, IReadOnlyCollection<CameraComponentState> state)
        {
            if (state is null) return;
            foreach (var compState in state)
                if (camera.GetComponent(compState.TypeName) is MonoBehaviour component)
                    component.enabled = compState.Enabled;
        }

        protected virtual bool GetRenderUI ()
        {
            if (Configuration.UseUICamera) return UICamera.enabled;
            return MaskUtils.GetLayer(Camera.cullingMask, uiLayer);
        }

        protected virtual void SetRenderUI (bool value)
        {
            if (Configuration.UseUICamera) UICamera.enabled = value;
            else Camera.cullingMask = MaskUtils.SetLayer(Camera.cullingMask, uiLayer, value);
        }

        protected virtual void SetOffset (Vector3 value)
        {
            CompleteOffsetTween();
            offset = value;
            SetCameraOffset(value);
        }

        protected virtual void SetRotation (Quaternion value)
        {
            CompleteRotationTween();
            rotation = value;
            SetCameraRotation(value);
        }

        protected virtual void SetZoom (float value)
        {
            CompleteZoomTween();
            zoom = value;
            SetCameraZoom(value);
        }

        protected virtual void SetOrthographic (bool value)
        {
            Camera.orthographic = value;
            Zoom = Zoom;
        }

        protected virtual void SetCameraOffset (Vector3 offset)
        {
            lookContainer.position = Configuration.InitialPosition + offset;
        }

        protected virtual Vector3 GetCameraOffset ()
        {
            return lookContainer.position - Configuration.InitialPosition;
        }

        protected virtual void SetCameraRotation (Quaternion rotation)
        {
            lookContainer.rotation = rotation;
        }

        protected virtual void SetCameraRotation (Vector3 rotation)
        {
            lookContainer.rotation = Quaternion.Euler(rotation);
        }

        protected virtual Quaternion GetCameraRotation ()
        {
            return lookContainer.rotation;
        }

        protected virtual void SetCameraZoom (float zoom)
        {
            if (Orthographic) Camera.orthographicSize = initialOrthoSize * (1f - Mathf.Clamp(zoom, 0, .99f));
            else Camera.fieldOfView = Mathf.Lerp(5f, initialFOV, 1f - zoom);
        }

        protected virtual float GetCameraZoom ()
        {
            if (Orthographic) return Mathf.Clamp(1f - Camera.orthographicSize / initialOrthoSize, 0, .99f);
            return Mathf.Clamp(1f - Mathf.InverseLerp(5f, initialFOV, Camera.fieldOfView), 0, .99f);
        }

        private void CompleteOffsetTween ()
        {
            if (offsetTweener.Running)
                offsetTweener.CompleteInstantly();
        }

        private void CompleteRotationTween ()
        {
            if (rotationTweener.Running)
                rotationTweener.CompleteInstantly();
        }

        private void CompleteZoomTween ()
        {
            if (zoomTweener.Running)
                zoomTweener.CompleteInstantly();
        }
    }
}
