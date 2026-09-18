using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Common utilities for layered actors.
    /// </summary>
    public static class LayeredUtilities
    {
        /// <summary>
        /// Returns layer group name based on the specified transform.
        /// </summary>
        public static string BuildGroupName (Transform layerTransform)
        {
            var group = string.Empty;
            var transform = layerTransform.parent;
            while (transform && !transform.TryGetComponent<LayeredActorBehaviour>(out _))
            {
                group = transform.name + (string.IsNullOrEmpty(group) ? string.Empty : $"/{group}");
                transform = transform.parent;
            }
            return group;
        }
    }
}
