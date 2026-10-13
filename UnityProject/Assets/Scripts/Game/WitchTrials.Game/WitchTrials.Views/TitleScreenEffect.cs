using System;
using System.Collections.Generic;
using Naninovel;
using Naninovel.Commands;
using UnityEngine;

namespace WitchTrials.Views
{
    /// <summary>
    /// Rebuilds the title effect's entrance from the title script's observed
    /// 1.05-to-1 scale animation; this is not a recovery of the native method body.
    /// </summary>
    public sealed class TitleScreenEffect : MonoBehaviour, Spawn.IAwaitable, Spawn.IParameterized
    {
        private const float EntranceScale = 1.05f;
        private const float EntranceDuration = 2.7f;
        private const float EpilogueRevealDuration = 2f;
        private static readonly int OpacityId = Shader.PropertyToID("_EpilogueOpacity");

        public float RevealOpacity { get; private set; } = 1f;

        [SerializeField] private Transform _rootTransform;

        private Vector3 _defaultRootScale;
        private Vector3 _defaultRootPosition;
        private Quaternion _defaultRootRotation;
        private bool _rootScaleCached;
        private int _spawnVersion;
        private bool _stationary;
        private readonly Tweener<VectorTween> _scaleTweener = new Tweener<VectorTween>();
        private readonly Tweener<FloatTween> _opacityTweener = new Tweener<FloatTween>();
        private Renderer[] _opacityRenderers;
        private MaterialPropertyBlock _opacityBlock;
        private TransitionalSpriteRenderer _stationaryBackground;
        private Rect _effectCanvas;

        private void Awake()
        {
            if (_rootTransform) CacheRootScale();
            // Async instantiation can activate the prefab before spawn parameters arrive.
            // The epilogue owns its only title-effect spawn; hide it before that first render.
            if (FireworksEpilogueSession.Active) SetRevealOpacity(0f);
        }

        public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
        {
            _stationary = parameters != null && parameters.Count == 1 && parameters[0] == "stationary";
            if (parameters != null && parameters.Count > 0 && !_stationary)
                throw new ArgumentException("TitleScreenEffect accepts only the optional 'stationary' parameter.");
        }

        public async UniTask AwaitSpawn(AsyncToken token = default)
        {
            token.ThrowIfCanceled(this);
            if (!_rootTransform)
                throw new InvalidOperationException("TitleScreenEffect requires its imported root transform.");

            CacheRootScale();
            var version = ++_spawnVersion;
            _scaleTweener.Stop();
            _opacityTweener.Stop();
            RestoreRootScale();

            // The epilogue reveals a fixed CG; keep its light overlay aligned from the first frame.
            // Ordinary title spawns have no parameter and retain their original entrance tween.
            if (_stationary)
            {
                BindStationaryCanvas();
                AlignStationaryCanvas();
                SetRevealOpacity(0f);
                // Start in this frame, but let the following background/audio commands run now.
                // A shader multiplier preserves the imported Animator and particle color curves.
                _opacityTweener.Run(new FloatTween(0f, 1f,
                    new Tween(token.Completed ? 0f : EpilogueRevealDuration), SetRevealOpacity), token, this);
                return;
            }

            _stationaryBackground = null;
            SetRevealOpacity(1f);

            var from = _defaultRootScale * EntranceScale;
            _rootTransform.localScale = from;
            var tween = new VectorTween(from, _defaultRootScale,
                new Tween(token.Completed ? 0f : EntranceDuration, EasingType.EaseOutQuad), SetRootScale);

            try
            {
                await _scaleTweener.RunAwaitable(tween, token, this);
                token.ThrowIfCanceled(this);
            }
            finally
            {
                // A replaced invocation must not stop or reset the newer entrance.
                if (version == _spawnVersion)
                {
                    _scaleTweener.Stop();
                    RestoreRootScale();
                }
            }
        }

        private void CacheRootScale()
        {
            if (_rootScaleCached) return;
            _defaultRootScale = _rootTransform.localScale;
            _defaultRootPosition = _rootTransform.localPosition;
            _defaultRootRotation = _rootTransform.localRotation;
            _rootScaleCached = true;
        }

        private void BindStationaryCanvas()
        {
            var actor = Engine.GetServiceOrErr<IBackgroundManager>().GetActor("Stills") as MonoBehaviourActor<BackgroundMetadata>;
            _stationaryBackground = actor?.GameObject.GetComponentInChildren<TransitionalSpriteRenderer>();
            SpriteRenderer canvas = null;
            foreach (var renderer in _rootTransform.GetComponentsInChildren<SpriteRenderer>(true))
                if (renderer.name == "Title_ED3_Fog") { canvas = renderer; break; }
            if (!_stationaryBackground || !canvas || !canvas.sprite)
                throw new InvalidOperationException("Stationary TitleScreenEffect requires the Stills actor and the imported ED3 canvas Sprite.");

            // This source Sprite supplies the complete CG rectangle; character/particle bounds
            // cannot define the canvas. Use rect/pivot/PPU, never the cropped visual bounds.
            var sprite = canvas.sprite;
            var sourceMin = -sprite.pivot / sprite.pixelsPerUnit;
            var sourceMax = (sprite.rect.size - sprite.pivot) / sprite.pixelsPerUnit;
            var min = _rootTransform.InverseTransformPoint(canvas.transform.TransformPoint(sourceMin));
            var max = _rootTransform.InverseTransformPoint(canvas.transform.TransformPoint(sourceMax));
            _effectCanvas = Rect.MinMaxRect(min.x, min.y, max.x, max.y);
            if (_effectCanvas.width <= 0f || _effectCanvas.height <= 0f)
                throw new InvalidOperationException("The imported ED3 effect canvas is invalid.");
        }

        private void AlignStationaryCanvas()
        {
            if (!_stationaryBackground || !_rootTransform) return;
            var bounds = _stationaryBackground.Bounds;
            if (bounds.width <= 0f || bounds.height <= 0f)
                throw new InvalidOperationException("The Stills actor has no rendered canvas for the epilogue effect.");
            // The epilogue builder verifies that both input textures are 2048 x 1024.
            // Their transition uses one stable quad; follow its actual geometry, without
            // compensating for stale texture UVs from another stage of the render loop.
            var toParent = _rootTransform.parent.worldToLocalMatrix * _stationaryBackground.transform.localToWorldMatrix;
            var right = toParent.MultiplyVector(Vector3.right * bounds.width);
            var up = toParent.MultiplyVector(Vector3.up * bounds.height);
            var rotation = Quaternion.LookRotation(Vector3.Cross(right, up), up);
            var scale = new Vector3(right.magnitude / _effectCanvas.width,
                up.magnitude / _effectCanvas.height, _defaultRootScale.z);
            _rootTransform.localScale = scale;
            _rootTransform.localRotation = rotation;
            _rootTransform.localPosition = toParent.MultiplyPoint3x4(bounds.center) -
                rotation * Vector3.Scale(_effectCanvas.center, scale);
        }

        private void LateUpdate()
        {
            // Follow actor placement and camera matching, including a window resize.
            if (_stationary) AlignStationaryCanvas();
        }

        private void SetRootScale(Vector3 scale)
        {
            if (_rootTransform) _rootTransform.localScale = scale;
        }

        private void RestoreRootScale()
        {
            if (_rootScaleCached && _rootTransform)
            {
                _rootTransform.localScale = _defaultRootScale;
                _rootTransform.localPosition = _defaultRootPosition;
                _rootTransform.localRotation = _defaultRootRotation;
            }
        }

        private void SetRevealOpacity(float opacity)
        {
            RevealOpacity = Mathf.Clamp01(opacity);
            if (_opacityRenderers == null) _opacityRenderers = GetComponentsInChildren<Renderer>(true);
            if (_opacityBlock == null) _opacityBlock = new MaterialPropertyBlock();
            foreach (var renderer in _opacityRenderers)
            {
                if (!renderer) continue;
                renderer.GetPropertyBlock(_opacityBlock);
                _opacityBlock.SetFloat(OpacityId, RevealOpacity);
                renderer.SetPropertyBlock(_opacityBlock);
            }
        }

        private void OnDisable()
        {
            _stationary = false;
            _stationaryBackground = null;
            ++_spawnVersion;
            _scaleTweener.Stop();
            _opacityTweener.Stop();
            SetRevealOpacity(1f);
            RestoreRootScale();
        }
    }
}
