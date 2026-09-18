using System;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel.UI
{
    /// <summary>
    /// Represents a <see cref="ToastUI"/> appearance.
    /// </summary>
    public class ToastAppearance : MonoBehaviour
    {
        [Serializable]
        private class TextChangedEvent : UnityEvent<string> { }

        [SerializeField] private TextChangedEvent onTextChanged;
        [SerializeField] private UnityEvent onSelected;
        [SerializeField] private UnityEvent onDeselected;

        public virtual void SetText (string text) => onTextChanged?.Invoke(text);

        public virtual void SetSelected (bool selected)
        {
            gameObject.SetActive(selected);
            if (selected) onSelected?.Invoke();
            else onDeselected?.Invoke();
        }
    }
}
