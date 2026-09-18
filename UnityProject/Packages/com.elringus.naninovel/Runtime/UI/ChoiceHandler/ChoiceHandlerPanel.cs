using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel.UI
{
    /// <summary>
    /// Represents a view for choosing between a set of choices.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class ChoiceHandlerPanel : CustomUI, IManagedUI, ILocalizableUI
    {
        [Serializable]
        public new class GameState
        {
            // Saving buttons separately from handler actor choices, as they're destroyed dependently.
            public List<ChoiceState> Buttons = new();
        }

        /// <summary>
        /// Invoked when one of active choices are chosen.
        /// </summary>
        public event Action<ChoiceState> OnChoice;

        protected virtual List<ChoiceHandlerButton> ChoiceButtons { get; } = new();
        protected virtual RectTransform ButtonsContainer => buttonsContainer;
        protected virtual ChoiceHandlerButton DefaultButtonPrefab => defaultButtonPrefab;
        protected virtual bool FocusChoiceButtons => focusChoiceButtons;

        [Tooltip("Container that will hold spawned choice buttons.")]
        [SerializeField] private RectTransform buttonsContainer;
        [Tooltip("Button prototype to use by default.")]
        [SerializeField] private ChoiceHandlerButton defaultButtonPrefab;
        [Tooltip("Whether to focus added choice buttons for keyboard and gamepad control.")]
        [SerializeField] private bool focusChoiceButtons = true;

        private IResourceLoader<GameObject> customButtonLoader;
        private bool removeAllButtonsPending;

        UniTask IManagedUI.ChangeVisibility (bool visible, float? duration, AsyncToken token)
        {
            Engine.Err("@showUI and @hideUI commands can't be used with choice handlers; use @show/hide commands instead");
            return UniTask.CompletedTask;
        }

        public virtual void AddChoiceButton (ChoiceState choice)
        {
            if (removeAllButtonsPending)
            {
                removeAllButtonsPending = false;
                RemoveAllChoiceButtons();
            }

            if (ChoiceButtons.Any(b => b.ChoiceState.Id == choice.Id)) return; // Could happen on rollback.

            var choicePrefab = string.IsNullOrWhiteSpace(choice.ButtonPath)
                ? defaultButtonPrefab
                : LoadCustomButtonPrefab(choice.ButtonPath);
            var choiceButton = Instantiate(choicePrefab, buttonsContainer, false);
            choiceButton.Initialize(choice);
            choiceButton.Show();
            choiceButton.OnButtonClicked += () => OnChoice?.Invoke(choice);

            if (choice.OverwriteButtonPosition)
                choiceButton.transform.localPosition = choice.ButtonPosition;

            ChoiceButtons.Add(choiceButton);

            if (ShouldFocusAddedButton(choiceButton))
                FocusAddedButton(choiceButton);
        }

        public virtual void RemoveChoiceButton (string id)
        {
            var buttons = ChoiceButtons.FindAll(c => c.ChoiceState.Id == id);
            if (buttons.Count == 0) return;

            foreach (var button in buttons)
            {
                if (button) Destroy(button.gameObject);
                ChoiceButtons.Remove(button);
            }
        }

        /// <summary>
        /// Will remove the buttons before the next <see cref="AddChoiceButton(ChoiceState)"/> call.
        /// </summary>
        public virtual void RemoveAllChoiceButtonsDelayed ()
        {
            ChoiceButtons?.ForEach(HideIfValid);
            removeAllButtonsPending = true;

            void HideIfValid (ChoiceHandlerButton button)
            {
                if (button) button.Hide();
            }
        }

        public virtual void RemoveAllChoiceButtons ()
        {
            for (int i = 0; i < ChoiceButtons.Count; i++)
                Destroy(ChoiceButtons[i].gameObject);
            ChoiceButtons.Clear();
        }

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(defaultButtonPrefab, buttonsContainer);

            customButtonLoader = Engine.GetServiceOrErr<IChoiceHandlerManager>().ChoiceButtonLoader;
        }

        protected virtual ChoiceHandlerButton LoadCustomButtonPrefab (string path)
        {
            var resource = customButtonLoader.GetLoaded(path) ?? Resource<GameObject>.Invalid;
            if (resource.Valid && resource.Object.TryGetComponent<ChoiceHandlerButton>(out var b2)) return b2;
            if (Resources.Load<ChoiceHandlerButton>(path) is { } b1 && b1) return b1;
            throw new Error($"Failed to add custom '{path}' choice button. Make sure the button prefab is stored in a 'Resources' folder " +
                            "or custom loader in choices configuration is set up correctly. " +
                            "Be aware, that when using custom loader, dynamic path values (with expressions) are not supported.");
        }

        protected virtual bool ShouldFocusAddedButton (ChoiceHandlerButton _)
        {
            return FocusChoiceButtons &&
                   ChoiceButtons.Count == 1; // Focus only the first added button.
        }

        protected override GameObject FindFocusObject ()
        {
            return ChoiceButtons.FirstOrDefault(c => c.Interactable)?.gameObject;
        }

        protected virtual void FocusAddedButton (ChoiceHandlerButton button)
        {
            switch (FocusModeType)
            {
                case FocusMode.Visibility:
                    FocusButtonDelayed().Forget();
                    break;
                case FocusMode.Navigation:
                    FocusOnNavigation = button.gameObject;
                    break;
            }

            async UniTaskVoid FocusButtonDelayed ()
            {
                // Delay focus to prevent button activation when submit key was pressed this frame
                // (eg, when showing choices after user pressed enter to activate continue input)
                await AsyncUtils.WaitEndOfFrame();
                if (this && button)
                    EventUtils.Select(button.gameObject);
            }
        }

        public override async UniTask HandleLocalizationChanged (LocaleChangedArgs _)
        {
            await UniTask.WhenAll(ChoiceButtons.Select(LocalizeChoice));

            async UniTask LocalizeChoice (ChoiceHandlerButton button)
            {
                await button.ChoiceState.Summary.Load(); // held by the choice handler actor
                button.Initialize(button.ChoiceState);
            }
        }

        protected override void SerializeState (GameStateMap stateMap)
        {
            base.SerializeState(stateMap);

            var state = new GameState {
                // Don't save removeAllButtonsPending, as it'll result in summary choice text resolve error on load.
                Buttons = removeAllButtonsPending ? new() : ChoiceButtons.Select(b => b.ChoiceState).ToList()
            };
            stateMap.SetState(state, name);
        }

        protected override async UniTask DeserializeState (GameStateMap stateMap)
        {
            await base.DeserializeState(stateMap);

            var state = stateMap.GetState<GameState>(name);
            if (state is null) return;

            var existingButtonIds = ChoiceButtons.Select(b => b.ChoiceState.Id).ToList();
            foreach (var buttonId in existingButtonIds)
                if (state.Buttons.All(s => s.Id != buttonId))
                    RemoveChoiceButton(buttonId);

            foreach (var buttonState in state.Buttons)
                if (ChoiceButtons.All(b => b.ChoiceState != buttonState))
                    AddChoiceButton(buttonState);
        }
    }
}
