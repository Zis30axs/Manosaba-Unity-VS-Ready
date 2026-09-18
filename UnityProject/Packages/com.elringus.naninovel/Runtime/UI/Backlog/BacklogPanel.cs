using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    public class BacklogPanel : CustomUI, IBacklogUI, ILocalizableUI
    {
        [Serializable]
        public new class GameState
        {
            public List<BacklogMessage> Messages;
        }

        protected virtual BacklogMessageUI LastMessage => messages.Last?.Value;
        protected virtual RectTransform MessagesContainer => messagesContainer;
        protected virtual ScrollRect ScrollRect => scrollRect;
        protected virtual BacklogMessageUI MessagePrefab => messagePrefab;
        protected virtual int Capacity => Mathf.Max(1, capacity);
        protected virtual int SaveCapacity => Mathf.Max(0, saveCapacity);
        protected virtual bool AddChoices => addChoices;
        protected virtual bool AllowReplayVoice => allowReplayVoice;
        protected virtual bool AllowRollback => allowRollback;
        protected virtual string ChoiceSeparator => choiceSeparator;

        [SerializeField] private RectTransform messagesContainer;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private BacklogMessageUI messagePrefab;
        [Tooltip("How many messages should the backlog keep.")]
        [SerializeField] private int capacity = 300;
        [Tooltip("How many messages should the backlog keep when saving the game.")]
        [SerializeField] private int saveCapacity = 30;
        [Tooltip("Whether to add choices summary to the log.")]
        [SerializeField] private bool addChoices = true;
        [Tooltip("Template to use for selected choice summary. " + choiceTemplateLiteral + " will be replaced with the actual choice summary.")]
        [SerializeField] private string selectedChoiceTemplate = $"    <b>{choiceTemplateLiteral}</b>";
        [Tooltip("Template to use for other (not selected) choice summary. " + choiceTemplateLiteral + " will be replaced with the actual choice summary.")]
        [SerializeField] private string otherChoiceTemplate = $"    <color=#ffffff88>{choiceTemplateLiteral}</color>";
        [Tooltip("String added between consequent choices.")]
        [SerializeField] private string choiceSeparator = "<br>";
        [Tooltip("Whether to allow replaying voices associated with the backlogged messages.")]
        [SerializeField] private bool allowReplayVoice = true;
        [Tooltip("Whether to allow rolling back to playback spots associated with the backlogged messages.")]
        [SerializeField] private bool allowRollback = true;

        private const string choiceTemplateLiteral = "%SUMMARY%";

        private readonly LinkedList<BacklogMessageUI> messages = new();
        private readonly Stack<BacklogMessageUI> messagesPool = new();
        private readonly List<LocalizableText> formatPool = new();
        private IStateManager stateManager;

        public override UniTask Initialize ()
        {
            BindInput(InputNames.ShowBacklog, Show, new() { WhenHidden = true });
            BindInput(InputNames.Cancel, Hide, new() { OnEnd = true });
            return UniTask.CompletedTask;
        }

        public virtual void AddMessage (LocalizableText text, string actorId = null, PlaybackSpot? rollbackSpot = null, string voicePath = null)
        {
            var voices = AllowReplayVoice && !string.IsNullOrEmpty(voicePath) ? new[] { voicePath } : null;
            SpawnMessage(new(text, actorId, ProcessRollbackSpot(rollbackSpot), voices));
        }

        public virtual void AppendMessage (LocalizableText text, string voicePath = null)
        {
            if (!LastMessage) return;
            LastMessage.Append(text, AllowReplayVoice ? voicePath : null);
        }

        public virtual void AddChoice (IReadOnlyList<BacklogChoice> choices)
        {
            if (AddChoices) SpawnMessage(new(FormatChoices(choices)));
        }

        public virtual void Clear ()
        {
            ClearMessages();
        }

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(messagesContainer, scrollRect, messagePrefab);

            stateManager = Engine.GetService<IStateManager>();
        }

        protected virtual void SpawnMessage (BacklogMessage message)
        {
            var messageUI = default(BacklogMessageUI);

            if (messages.Count >= Capacity)
            {
                messageUI = messages.First.Value;
                messageUI.gameObject.SetActive(true);
                messageUI.transform.SetSiblingIndex(MessagesContainer.childCount - 1);
                messages.RemoveFirst();
            }
            else
            {
                if (messagesPool.Count > 0)
                {
                    messageUI = messagesPool.Pop();
                    messageUI.gameObject.SetActive(true);
                    messageUI.transform.SetSiblingIndex(MessagesContainer.childCount - 1);
                }
                else messageUI = Instantiate(MessagePrefab, MessagesContainer, false);
            }
            messages.AddLast(messageUI);

            messageUI.Initialize(message);
        }

        protected override void HandleVisibilityChanged (bool visible)
        {
            base.HandleVisibilityChanged(visible);

            MessagesContainer.gameObject.SetActive(visible);
            if (visible) ScrollToBottom();
        }

        protected override void SerializeState (GameStateMap stateMap)
        {
            base.SerializeState(stateMap);
            var state = new GameState {
                Messages = messages.TakeLast(SaveCapacity).Select(m => m.GetState()).ToList()
            };
            stateMap.SetState(state);
        }

        protected override async UniTask DeserializeState (GameStateMap stateMap)
        {
            await base.DeserializeState(stateMap);

            using var _ = SetPool<LocalizableText>.Rent(out var deferRelease);
            ClearMessages(deferRelease);

            var state = stateMap.GetState<GameState>();
            if (state?.Messages != null)
            {
                using var __ = ListPool<UniTask>.Rent(out var tasks);
                foreach (var message in state.Messages)
                    tasks.Add(DeserializeMessage(message));
                await UniTask.WhenAll(tasks);
            }

            foreach (var deferred in deferRelease)
                deferred.Release(this);
        }

        protected virtual async UniTask DeserializeMessage (BacklogMessage message)
        {
            await message.Text.Load(); // held by messageUI on init
            SpawnMessage(message);
        }

        protected virtual void ClearMessages (ISet<LocalizableText> deferRelease = null)
        {
            foreach (var message in messages)
            {
                if (deferRelease != null)
                {
                    message.Text.Hold(this);
                    deferRelease.Add(message.Text);
                }
                message.Clear();
                message.gameObject.SetActive(false);
                messagesPool.Push(message);
            }
            messages.Clear();
        }

        protected virtual PlaybackSpot ProcessRollbackSpot (PlaybackSpot? spot)
        {
            if (!AllowRollback || !spot.HasValue || spot == PlaybackSpot.Invalid)
                return PlaybackSpot.Invalid;

            // Otherwise stored spots not associated with player input
            // won't serialize (eg, printed messages with [skipInput]).
            if (stateManager.PeekRollbackStack()?.PlaybackSpot == spot)
                stateManager.PeekRollbackStack()?.ForceSerialize();

            return spot.Value;
        }

        protected virtual LocalizableText FormatChoices (IReadOnlyList<BacklogChoice> choices)
        {
            formatPool.Clear();
            foreach (var choice in choices)
                formatPool.Add(FormatChoice(choice));
            return LocalizableText.Join(ChoiceSeparator, formatPool);
        }

        protected virtual LocalizableText FormatChoice (BacklogChoice choice)
        {
            return choice.Selected
                ? LocalizableText.FromTemplate(selectedChoiceTemplate, choiceTemplateLiteral, choice.Summary)
                : LocalizableText.FromTemplate(otherChoiceTemplate, choiceTemplateLiteral, choice.Summary);
        }

        protected virtual async void ScrollToBottom ()
        {
            // Wait a frame and force rebuild layout before setting scroll position,
            // otherwise it's ignoring recently added messages.
            await AsyncUtils.WaitEndOfFrame();
            LayoutRebuilder.ForceRebuildLayoutImmediate(ScrollRect.content);
            ScrollRect.verticalNormalizedPosition = 0;
        }

        protected override GameObject FindFocusObject ()
        {
            var message = messages.Last;
            while (message != null)
            {
                if (message.Value.GetComponentInChildren<Selectable>() is { } selectable)
                    return selectable.gameObject;
                message = message.Previous;
            }
            return null;
        }
    }
}
