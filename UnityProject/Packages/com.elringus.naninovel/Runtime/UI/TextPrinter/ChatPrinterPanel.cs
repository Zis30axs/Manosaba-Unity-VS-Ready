using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    /// <summary>
    /// A <see cref="UITextPrinterPanel"/> implementation for a chat-style printer.
    /// </summary>
    public class ChatPrinterPanel : UITextPrinterPanel, ILocalizableUI
    {
        public override float RevealProgress { get => revealProgress; set => SetRevealProgress(value); }
        public override string Appearance { get; set; }

        protected virtual ScrollRect ScrollRect => scrollRect;
        protected virtual RectTransform MessagesContainer => messagesContainer;
        protected virtual ChatMessage MessagePrototype => messagePrototype;
        protected virtual ScriptableUIBehaviour InputIndicator => inputIndicator;
        protected virtual float RevealDelayModifier => revealDelayModifier;
        protected virtual string ChoiceHandlerId => choiceHandlerId;
        protected virtual RectTransform ChoiceHandlerContainer => choiceHandlerContainer;

        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform messagesContainer;
        [SerializeField] private ChatMessage messagePrototype;
        [SerializeField] private ScriptableUIBehaviour inputIndicator;
        [SerializeField] private float revealDelayModifier = 3f;
        [Tooltip("Associated choice handler actor ID to embed inside the printer; implementation is expected to be MonoBehaviourActor-derived.")]
        [SerializeField] private string choiceHandlerId = "ChatReply";
        [SerializeField] private RectTransform choiceHandlerContainer;

        private readonly Stack<ChatMessage> chatMessages = new();
        private ICharacterManager characterManager;
        private IChoiceHandlerManager choiceManager;
        private float revealProgress;

        public override async UniTask Initialize ()
        {
            await base.Initialize();
            await EmbedChoiceHandler();
        }

        public override void SetMessages (IReadOnlyList<PrintedMessage> messages)
        {
            Messages.ReplaceWith(messages);
            DestroyAllMessages();
            foreach (var message in messages)
                SpawnChatMessage(message);
            ScrollToBottom();
        }

        public override void AddMessage (PrintedMessage message)
        {
            Messages.Add(message);
            SpawnChatMessage(message);
            ScrollToBottom();
        }

        public override void AppendText (LocalizableText text)
        {
            if (Messages.Count == 0)
            {
                AddMessage(new(text));
                return;
            }
            ObjectUtils.DestroyOrImmediate(chatMessages.Pop().gameObject);
            Messages[^1] = new(Messages[^1].Text + text, Messages[^1].Author ?? default);
            AddMessage(Messages[^1]);
        }

        public override async UniTask RevealMessages (float delay, AsyncToken token)
        {
            if (chatMessages.Count == 0) return;

            var message = chatMessages.Peek();
            RevealProgress = 0;

            if (delay > 0)
            {
                var revealDuration = message.MessageText.Count(char.IsLetterOrDigit) * delay * revealDelayModifier;
                var revealStartTime = Engine.Time.Time;
                var revealFinishTime = revealStartTime + revealDuration;
                while (revealFinishTime > Engine.Time.Time && chatMessages.Count > 0 && chatMessages.Peek() == message)
                {
                    RevealProgress = (Engine.Time.Time - revealStartTime) / revealDuration;
                    await AsyncUtils.WaitEndOfFrame(token);
                    if (token.Completed) break;
                }
            }

            RevealProgress = 1f;
        }

        public override void SetWaitForInputIndicatorVisible (bool visible)
        {
            if (visible) inputIndicator.Show();
            else inputIndicator.Hide();
        }

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(scrollRect, messagesContainer, messagePrototype, inputIndicator);

            characterManager = Engine.GetServiceOrErr<ICharacterManager>();
            choiceManager = Engine.GetServiceOrErr<IChoiceHandlerManager>();
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();

            if (choiceManager.ActorExists(choiceHandlerId))
                choiceManager.RemoveActor(choiceHandlerId);
        }

        protected virtual async UniTask EmbedChoiceHandler ()
        {
            if (string.IsNullOrEmpty(ChoiceHandlerId) || !ChoiceHandlerContainer) return;
            var handler = await choiceManager.GetOrAddActor(ChoiceHandlerId) as MonoBehaviourActor<ChoiceHandlerMetadata>;
            if (handler is null || !handler.GameObject) throw new Error($"Choice handler '{ChoiceHandlerId}' is not derived from MonoBehaviourActor or destroyed.");
            var rectTrs = handler.GameObject.GetComponentInChildren<RectTransform>();
            if (!rectTrs) throw new Error($"Choice handler '{ChoiceHandlerId}' is missing RectTransform component.");
            rectTrs.SetParent(ChoiceHandlerContainer, false);
            var ui = ChoiceHandlerContainer.GetComponentInChildren<IManagedUI>();
            if (ui is null) throw new Error($"Choice handler '{ChoiceHandlerId}' is missing IManagedUI component.");
            ui.OnVisibilityChanged += HandleChoiceVisibilityChanged;
        }

        protected virtual void HandleChoiceVisibilityChanged (bool visible)
        {
            ChoiceHandlerContainer.gameObject.SetActive(visible);
            ScrollToBottom();
        }

        protected virtual ChatMessage SpawnChatMessage (PrintedMessage message)
        {
            var chatMessage = Instantiate(messagePrototype, messagesContainer, false);
            chatMessage.MessageText = FormatMessage(message);

            if (message.Author?.Id is { } authorId)
            {
                chatMessage.ActorNameText = message.Author is { Label: { IsEmpty: false } label } ? label : characterManager.GetAuthorName(authorId);
                chatMessage.AvatarTexture = CharacterManager.GetAvatarTextureFor(authorId);

                var meta = characterManager.Configuration.GetMetadataOrDefault(authorId);
                if (meta.UseCharacterColor)
                {
                    chatMessage.MessageColor = meta.MessageColor;
                    chatMessage.ActorNameTextColor = meta.NameColor;
                }
            }
            else
            {
                chatMessage.ActorNameText = string.Empty;
                chatMessage.AvatarTexture = null;
            }

            chatMessage.Visible = true;
            chatMessages.Push(chatMessage);
            return chatMessage;
        }

        protected virtual void SetRevealProgress (float ratio)
        {
            revealProgress = ratio;
            if (chatMessages.Count > 0)
                chatMessages.Peek().SetIsTyping(ratio <= .99f);
        }

        protected virtual void DestroyAllMessages ()
        {
            while (chatMessages.Count > 0)
                ObjectUtils.DestroyOrImmediate(chatMessages.Pop().gameObject);
        }

        protected virtual async void ScrollToBottom ()
        {
            // Wait a frame and force rebuild layout before setting scroll position,
            // otherwise it's ignoring recently added messages.
            await AsyncUtils.DelayFrame(1);
            if (!scrollRect) return;
            LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
            scrollRect.verticalNormalizedPosition = 0;
        }
    }
}
