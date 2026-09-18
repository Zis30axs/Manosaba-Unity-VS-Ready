using UnityEngine;

namespace Naninovel.FX
{
    /// <summary>
    /// Shakes the main Naninovel render camera.
    /// </summary>
    public class ShakeCamera : ShakeTransform
    {
        protected override Transform GetShakenTransform ()
        {
            var cameraManager = Engine.GetServiceOrErr<ICameraManager>().Camera;
            if (!cameraManager || !cameraManager.transform.parent) return null;
            return cameraManager.transform.parent;
        }
    }
}
