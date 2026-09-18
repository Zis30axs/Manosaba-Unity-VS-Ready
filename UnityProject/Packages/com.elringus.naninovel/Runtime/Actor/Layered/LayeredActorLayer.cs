using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// When attached to a game object inside layered actor prefab
    /// signals the game object should be considered a layer.
    /// (only used in camera renderer mode at the moment)
    /// </summary>
    public class LayeredActorLayer : MonoBehaviour
    {
        [SerializeField] private IntUnityEvent onLayerHeld;
        [SerializeField] private IntUnityEvent onLayerReleased;

        public void NotifyLayerHeld (int cameraCullingLayer)
        {
            onLayerHeld?.Invoke(cameraCullingLayer);
        }

        public void NotifyLayerReleased (int cameraCullingLayer)
        {
            onLayerReleased?.Invoke(cameraCullingLayer);
        }
    }
}
