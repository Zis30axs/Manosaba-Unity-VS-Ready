using System;
using System.Threading;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Allows scaling an arbitrary value to match current camera aspect ratio.
    /// </summary>
    public abstract class CameraMatcher : IDisposable
    {
        /// <summary>
        /// Current match mode.
        /// </summary>
        public AspectMatchMode MatchMode { get; set; }
        /// <summary>
        /// Current match ratio to use in case <see cref="AspectMatchMode.Custom"/> mode is active.
        /// </summary>
        public float CustomMatchRatio { get; set; }
        /// <summary>
        /// Whether the matcher is currently working.
        /// </summary>
        public bool Matching => cts != null;

        private readonly UnityEngine.Object target;
        private readonly ICameraManager cameraManager;

        private CancellationTokenSource cts;
        private Vector2 previousCameraSize;
        private Vector2 previousReferenceSize;

        protected CameraMatcher (ICameraManager cameraManager, UnityEngine.Object target)
        {
            this.cameraManager = cameraManager;
            this.target = target;
        }

        /// <summary>
        /// Begins the matching process.
        /// </summary>
        public virtual void Start ()
        {
            if (Matching) return;
            if (!target) throw new Error("Match target is not valid.");
            cts = new();
            MatchInLoop(cts.Token).Forget();
        }

        /// <summary>
        /// Stops the matching process.
        /// </summary>
        public virtual void Stop ()
        {
            if (!Matching) return;
            cts.Cancel();
            cts.Dispose();
            cts = null;
        }

        public void Dispose ()
        {
            cts?.Dispose();
        }

        /// <summary>
        /// Whether matching should be performed at this time.
        /// </summary>
        protected virtual bool IsMatchRequired (Vector2 cameraSize, Vector2 referenceSize)
        {
            return cameraSize != previousCameraSize || referenceSize != previousReferenceSize;
        }

        /// <summary>
        /// Attempts to evaluate current reference sizes for matching.
        /// Returns false in case it's not possible to evaluate the size.
        /// </summary>
        protected abstract bool TryGetReferenceSize (out Vector2 referenceSize);

        /// <summary>
        /// Attempts to evaluate current camera size for matching.
        /// Returns false in case it's not possible to evaluate the size.
        /// </summary>
        protected virtual bool TryGetCameraSize (out Vector2 cameraSize)
        {
            cameraSize = default;
            if (!cameraManager?.Camera || !cameraManager.Camera.orthographic) return false;
            var cameraHeight = cameraManager.Configuration.SceneRect.height;
            var cameraWidth = cameraHeight * cameraManager.Camera.aspect;
            cameraSize = new(cameraWidth, cameraHeight);
            return true;
        }

        /// <summary>
        /// Invoked when reference size should be scaled to match current camera size.
        /// </summary>
        /// <param name="scaleFactor">Modifier to apply for the reference size.</param>
        protected abstract void ApplyScale (float scaleFactor);

        /// <summary>
        /// Performs the matching for the specified sizes.
        /// </summary>
        protected virtual void Match (Vector2 cameraSize, Vector2 referenceSize)
        {
            var scaleFactor = EvaluateScaleFactor(cameraSize, referenceSize);
            ApplyScale(scaleFactor);
            previousCameraSize = cameraSize;
            previousReferenceSize = referenceSize;
        }

        /// <summary>
        /// Calculates scale factor for the specified sizes.
        /// </summary>
        protected virtual float EvaluateScaleFactor (Vector2 cameraSize, Vector2 contentSize)
        {
            return AspectMatcher.Match(MatchMode, cameraSize, contentSize, CustomMatchRatio);
        }

        private async UniTaskVoid MatchInLoop (CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested && target)
            {
                if (TryGetCameraSize(out var cameraSize) &&
                    TryGetReferenceSize(out var referenceSize) &&
                    IsMatchRequired(cameraSize, referenceSize))
                    Match(cameraSize, referenceSize);
                await AsyncUtils.WaitEndOfFrame();
            }
        }
    }
}
