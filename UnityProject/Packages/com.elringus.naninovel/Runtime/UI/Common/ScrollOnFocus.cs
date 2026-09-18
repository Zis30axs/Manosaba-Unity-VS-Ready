using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Naninovel.UI
{
    /// <summary>
    /// Scrolls parent scrollbar to the specified content when the element gets focus (is selected).
    /// Work only in gamepad input mode.
    /// </summary>
    public class ScrollOnFocus : MonoBehaviour, ISelectHandler
    {
        [Tooltip("Content to focus.")]
        [SerializeField] private RectTransform content;

        private ScrollRect rect;

        public virtual void OnSelect (BaseEventData eventData)
        {
            if (Engine.GetServiceOrErr<IInputManager>().InputMode != InputMode.Gamepad) return;
            if (!rect) rect = GetComponentInParent<ScrollRect>();
            if (!rect) throw new Error("Failed to find scroll rect in parents.");
            if (!rect.Contains(content)) rect.ScrollTo(content);
        }

        protected virtual void Awake ()
        {
            this.AssertRequiredObjects(content);
        }
    }
}
