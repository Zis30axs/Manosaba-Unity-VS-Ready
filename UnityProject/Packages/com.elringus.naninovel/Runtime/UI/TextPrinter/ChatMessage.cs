using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ChatMessage : ScriptableUIBehaviour
    {
        [System.Serializable]
        private class MessageTextChangedEvent : UnityEvent<string> { }

        public virtual string MessageText { get => messageText; set => SetMessageText(value); }
        public virtual Color MessageColor { get => messageFrameImage.color; set => messageFrameImage.color = value; }
        public virtual string ActorNameText { get => actorNamePanel.Text; set => actorNamePanel.Text = value; }
        public virtual Color ActorNameTextColor { get => actorNamePanel.TextColor; set => actorNamePanel.TextColor = value; }
        public virtual Texture AvatarTexture { get => avatarImage.texture; set => SetAvatarTexture(value); }

        protected virtual AuthorNamePanel ActorNamePanel => actorNamePanel;
        protected virtual Image MessageFrameImage => messageFrameImage;
        protected virtual RawImage AvatarImage => avatarImage;
        protected virtual bool Typing { get; private set; }

        [SerializeField] private AuthorNamePanel actorNamePanel;
        [SerializeField] private Image messageFrameImage;
        [SerializeField] private RawImage avatarImage;
        [Tooltip("Invoked when the message text is changed.")]
        [SerializeField] private MessageTextChangedEvent onMessageTextChanged;
        [SerializeField] private UnityEvent onStartTyping;
        [SerializeField] private UnityEvent onStopTyping;

        private string messageText;

        public virtual void SetIsTyping (bool typing)
        {
            if (typing == Typing) return;
            Typing = typing;
            if (Typing) onStartTyping?.Invoke();
            else onStopTyping?.Invoke();
        }

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(actorNamePanel, messageFrameImage, avatarImage);
        }

        protected virtual void SetMessageText (string text)
        {
            messageText = text;
            onMessageTextChanged?.Invoke(text);
        }

        protected virtual void SetAvatarTexture (Texture texture)
        {
            avatarImage.texture = texture;
            avatarImage.gameObject.SetActive(texture);
        }
    }
}
