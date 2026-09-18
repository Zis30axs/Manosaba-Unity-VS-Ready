using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [RequireComponent(typeof(Button))]
    public class ChoiceHandlerButton : ScriptableButton
    {
        [Serializable] private class SummaryTextChangedEvent : UnityEvent<string> { }
        [Serializable] private class OnLockEvent : UnityEvent<bool> { }

        /// <summary>
        /// Invoked when the choice summary text is changed.
        /// </summary>
        public event Action<string> OnSummaryTextChanged;
        /// <summary>
        /// Invoked when lock status is changed: true when locked/disabled and vice-versa.
        /// </summary>
        public event Action<bool> OnLock;

        public ChoiceState ChoiceState { get; private set; }

        [Tooltip("Invoked when the choice summary text is changed.")]
        [SerializeField] private SummaryTextChangedEvent onSummaryTextChanged;
        [Tooltip("Invoked when lock status is changed: true when locked/disabled and vice-versa.")]
        [SerializeField] private OnLockEvent onLock;

        public virtual void Initialize (ChoiceState choiceState)
        {
            ChoiceState = choiceState;

            OnSummaryTextChanged?.Invoke(choiceState.Summary);
            onSummaryTextChanged?.Invoke(choiceState.Summary);

            OnLock?.Invoke(choiceState.Locked);
            onLock?.Invoke(choiceState.Locked);
        }

        public virtual void HandleLockChanged (bool locked)
        {
            SetInteractable(!locked);
        }
    }
}
