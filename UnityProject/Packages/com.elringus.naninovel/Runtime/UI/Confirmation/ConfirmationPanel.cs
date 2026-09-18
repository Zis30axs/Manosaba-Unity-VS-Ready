using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naninovel.UI
{
    public class ConfirmationPanel : CustomUI, IConfirmationUI, ILocalizableUI
    {
        [Serializable]
        private class OnMessageChangedEvent : UnityEvent<string> { }

        protected virtual Button ConfirmButton => confirmButton;
        protected virtual Button CancelButton => cancelButton;
        protected virtual Button CloseButton => closeButton;
        protected virtual bool? Confirmed { get; private set; }

        [Tooltip("Used to agree on confirmation dialogue.")]
        [SerializeField] private Button confirmButton;
        [Tooltip("Used to cancel on confirmation dialogue.")]
        [SerializeField] private Button cancelButton;
        [Tooltip("Used to close notification dialogue.")]
        [SerializeField] private Button closeButton;
        [SerializeField] private OnMessageChangedEvent onMessageChanged;

        public override UniTask Initialize ()
        {
            BindInput(InputNames.Cancel, Cancel, new() { OnEnd = true });
            return UniTask.CompletedTask;
        }

        public virtual async UniTask<bool> Confirm (string message)
        {
            if (Visible) return false;

            ConfirmButton.gameObject.SetActive(true);
            CancelButton.gameObject.SetActive(true);
            CloseButton.gameObject.SetActive(false);

            SetMessage(message);

            Show();

            while (!Confirmed.HasValue)
                await AsyncUtils.WaitEndOfFrame();

            var result = Confirmed.Value;
            Confirmed = null;

            Hide();

            return result;
        }

        public virtual async UniTask Notify (string message)
        {
            if (Visible) return;

            ConfirmButton.gameObject.SetActive(false);
            CancelButton.gameObject.SetActive(false);
            CloseButton.gameObject.SetActive(true);

            SetMessage(message);

            Show();

            while (!Confirmed.HasValue)
                await AsyncUtils.WaitEndOfFrame();

            Confirmed = null;

            Hide();
        }

        public override UniTask HandleLocalizationChanged (LocaleChangedArgs _)
        {
            SetMessage(""); // In case new locale's font misses glyphs to render message from the old locale.
            return base.HandleLocalizationChanged(_);
        }

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(ConfirmButton, CancelButton, CloseButton);
        }

        protected override void OnEnable ()
        {
            base.OnEnable();

            ConfirmButton.onClick.AddListener(Confirm);
            CancelButton.onClick.AddListener(Cancel);
            CloseButton.onClick.AddListener(Cancel);
        }

        protected override void OnDisable ()
        {
            base.OnDisable();

            ConfirmButton.onClick.RemoveListener(Confirm);
            CancelButton.onClick.RemoveListener(Cancel);
            CloseButton.onClick.RemoveListener(Cancel);
        }

        protected virtual void Confirm ()
        {
            if (!Visible) return;
            Confirmed = true;
        }

        protected virtual void Cancel ()
        {
            if (!Visible) return;
            Confirmed = false;
        }

        protected virtual void SetMessage (string value)
        {
            onMessageChanged?.Invoke(value);
        }
    }
}
