using UnityEngine;
using UnityEngine.Events;

namespace Naninovel
{
    /// <summary>
    /// Allows listening for events when screen aspect ratio goes below or above specified threshold.
    /// </summary>
    public class ReactToAspectRatio : MonoBehaviour
    {
        [System.Serializable]
        private class ThresholdReachedEvent : UnityEvent<bool> { }

        [Tooltip("When scene aspect ratio (width divided by height) goes above or below the value, the event will be invoked.")]
        [SerializeField] private float aspectThreshold = 1f;
        [Tooltip("How frequently update the values, in seconds."), Range(0f, 1f)]
        [SerializeField] private float updateDelay = .5f;
        [Tooltip("Invoked when scene aspect ratio (width divided by height) is changed and become either equal or above (true) or below (false) specified threshold.")]
        [SerializeField] private ThresholdReachedEvent onThresholdReached;

        private AspectMonitor aspectMonitor;

        private void Awake ()
        {
            aspectMonitor = new();
            aspectMonitor.OnChanged += HandleAspectChanged;
            aspectMonitor.Start(updateDelay, this);
        }

        private void OnDestroy ()
        {
            if (aspectMonitor != null)
            {
                aspectMonitor.OnChanged -= HandleAspectChanged;
                aspectMonitor.Stop();
            }
        }

        private void HandleAspectChanged (AspectMonitor monitor)
        {
            onThresholdReached?.Invoke(monitor.CurrentAspect >= aspectThreshold);
        }
    }
}
