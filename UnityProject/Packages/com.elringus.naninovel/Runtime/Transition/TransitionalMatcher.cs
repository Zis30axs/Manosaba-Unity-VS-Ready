using UnityEngine;

namespace Naninovel
{
    public class TransitionalMatcher : CameraMatcher
    {
        public Vector2 ReferenceSize { get; set; }
        public float Scale { get; set; }

        public TransitionalMatcher (ICameraManager cameraManager, Object target)
            : base(cameraManager, target) { }

        public float GetScaleFactor (Vector2 contentSize)
        {
            TryGetCameraSize(out var cameraSize);
            return EvaluateScaleFactor(cameraSize, contentSize);
        }

        protected override bool TryGetReferenceSize (out Vector2 referenceSize)
        {
            referenceSize = ReferenceSize;
            return true;
        }

        protected override void ApplyScale (float scaleFactor)
        {
            Scale = scaleFactor;
        }
    }
}
