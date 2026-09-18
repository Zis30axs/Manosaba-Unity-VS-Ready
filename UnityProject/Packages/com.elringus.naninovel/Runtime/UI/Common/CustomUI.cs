using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    /// <summary>
    /// Base implementation of <see cref="IManagedUI"/> for building custom UIs.
    /// </summary>
    public class CustomUI : ScriptableUIBehaviour, IManagedUI
    {
        [Serializable]
        public class GameState
        {
            public bool Visible;
        }

        [Serializable]
        public class FontChangeConfiguration
        {
            [Tooltip("The container game object with text components, which should be affected by font changes.")]
            public GameObject Object;
            [Tooltip("Whether to affect container children game objects; when disabled only text component on the specified container object will be affected.")]
            public bool IncludeChildren;
            [Tooltip("Whether to allow changing font of the text component.")]
            public bool AllowFontChange = true;
            [Tooltip("Whether to allow changing font size of the text component.")]
            public bool AllowFontSizeChange = true;
            [Tooltip("Sizes list should contain actual font sizes to apply for text component. Each element in the list corresponds to font size dropdown list index: Small -> 0, Default -> 1, Large -> 2, Extra Large -> 3 (can be changed via SettingsUI). Default value will be ignored and font size initially set in the prefab will be used instead.")]
            public FontSizes FontSizes;
            [NonSerialized]
            public List<TMP_Text> Components = new();
            [NonSerialized]
            public Dictionary<TMP_Text, int> DefaultSizes = new();
            [NonSerialized]
            public Dictionary<TMP_Text, TMP_FontAsset> DefaultFonts = new();
        }

        public struct SelectableData
        {
            public Selectable Selectable;
            public Navigation Navigation;
        }

        /// <summary>
        /// Optional <see cref="CustomUI.BindInput(string,System.Action,System.Nullable{Naninovel.UI.CustomUI.BindInputOptions})"/> configuration.
        /// </summary>
        public struct BindInputOptions
        {
            /// <summary>
            /// Whether to invoke bound handler when input ends activation (key released);
            /// by default invoked when input starts activation (key pressed).
            /// </summary>
            public bool OnEnd;
            /// <summary>
            /// Whether to invoke bound handler while the UI is hidden;
            /// by default invoked only when the UI is visible.
            /// </summary>
            public bool WhenHidden;
            /// <summary>
            /// Whether to invoke bound handler while the UI is blocked by a modal;
            /// by default not invoked when a modal UI is active, unless the UI itself is the modal.
            /// </summary>
            public bool WhenBlocked;
        }

        /// <summary>
        /// Optional <see cref="CustomUI.BindInput(string,System.Action{float},System.Nullable{Naninovel.UI.CustomUI.BindInputChangeOptions})"/> configuration.
        /// </summary>
        public struct BindInputChangeOptions
        {
            /// <inheritdoc cref="BindInputOptions.WhenHidden"/>
            public bool WhenHidden;
            /// <inheritdoc cref="BindInputOptions.WhenBlocked"/>
            public bool WhenBlocked;
        }

        public override GameObject FocusObject { get => base.FocusObject ? base.FocusObject : FindFocusObject(); set => base.FocusObject = value; }
        public virtual bool HideOnLoad => hideOnLoad;
        public virtual bool HideInThumbnail => hideInThumbnail;
        public virtual bool SaveVisibilityState => saveVisibilityState;
        public virtual bool BlockInputWhenVisible => blockInputWhenVisible;
        public virtual bool AdaptToInputMode => adaptToInputMode;
        public virtual bool ModalUI => modalUI;
        public virtual string ModalGroup => modalGroup;
        public virtual IReadOnlyList<SelectableData> Selectables => selectables;

        protected virtual List<FontChangeConfiguration> FontChangeConfigurations => fontChangeConfiguration;
        protected virtual string[] AllowedSamplers => allowedSamplers;
        protected virtual GameObject ButtonControls => buttonControls;
        protected virtual GameObject ControlsLegend => controlsLegend;

        [Tooltip("Whether to automatically hide the UI when loading game or resetting state.")]
        [SerializeField] private bool hideOnLoad = true;
        [Tooltip("Whether to hide the UI when capturing thumbnail for save-load slots.")]
        [SerializeField] private bool hideInThumbnail;
        [Tooltip("Whether to preserve visibility of the UI when saving/loading game.")]
        [SerializeField] private bool saveVisibilityState = true;
        [Tooltip("Whether to halt user input processing while the UI is visible. Will also exit auto read and skip script player modes when the UI becomes visible.")]
        [SerializeField] private bool blockInputWhenVisible;
        [Tooltip("Whether to modify the UI based on current input mode (mouse and keyboard, gamepad, touch, etc). Will take control of focus mode and navigation of the underlying selectables.")]
        [SerializeField] private bool adaptToInputMode = true;
        [Tooltip("Which input samplers should still be allowed in case the input is blocked while the UI is visible.")]
        [SerializeField] private string[] allowedSamplers;
        [Tooltip("Whether to make all the other managed UIs not interactable while the UI is visible.")]
        [SerializeField] private bool modalUI;
        [Tooltip("When assigned, will not yield the UI from being modal when another UI is made modal with the same group. Assign `*` to never yield the UI from being modal.")]
        [SerializeField] private string modalGroup;
        [Tooltip("Control buttons associated with the UI. Will be hidden when input mode is gamepad.")]
        [SerializeField] private GameObject buttonControls;
        [Tooltip("Labels indicating controls associated with the UI. Will be hidden when input mode is not gamepad.")]
        [SerializeField] private GameObject controlsLegend;
        [Tooltip("Setup which game objects should be affected by font and text size changes (set in game settings).")]
        [SerializeField] private List<FontChangeConfiguration> fontChangeConfiguration;

        private readonly List<SelectableData> selectables = new();
        private IStateManager stateManager;
        private IInputManager inputManager;
        private IUIManager uiManager;
        private IScriptPlayer scriptPlayer;

        public virtual UniTask Initialize () => UniTask.CompletedTask;

        public virtual void SetFont (TMP_FontAsset font)
            => FontChanger.ChangeFont(font, FontChangeConfigurations);

        public virtual void SetFontSize (int dropdownIndex)
            => FontChanger.ChangeFontSize(dropdownIndex, FontChangeConfigurations);

        /// <remarks>
        /// Default implementation is naive using <see cref="SerializeState"/> followed by
        /// <see cref="DeserializeState"/> forcing reinitialization with the current state.
        /// In cases when such re-serialization contain lots of unrelated operations,
        /// consider overriding the method for more granular behaviour.
        /// </remarks>
        public virtual UniTask HandleLocalizationChanged (LocaleChangedArgs _)
        {
            var map = new GameStateMap();
            SerializeState(map);
            return DeserializeState(map);
        }

        protected override void Awake ()
        {
            stateManager = Engine.GetServiceOrErr<IStateManager>();
            inputManager = Engine.GetServiceOrErr<IInputManager>();
            uiManager = Engine.GetServiceOrErr<IUIManager>();
            scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();

            base.Awake();

            InitializeFontChangeConfiguration();
        }

        protected override void OnEnable ()
        {
            base.OnEnable();

            if (HideOnLoad)
            {
                stateManager.OnGameLoadStarted += HandleGameLoadStarted;
                stateManager.OnResetStarted += Hide;
            }

            stateManager.AddOnGameSerializeTask(SerializeState);
            stateManager.AddOnGameDeserializeTask(DeserializeState);

            if (BlockInputWhenVisible)
                inputManager.AddBlockingUI(this, AllowedSamplers);

            if (AdaptToInputMode)
            {
                foreach (var selectable in GetComponentsInChildren<Selectable>(true))
                    selectables.Add(new() { Selectable = selectable, Navigation = selectable.navigation });
                inputManager.OnInputModeChanged += HandleInputModeChanged;
                HandleInputModeChanged(inputManager.InputMode);
            }
        }

        protected override void OnDisable ()
        {
            base.OnDisable();

            if (HideOnLoad && stateManager != null)
            {
                stateManager.OnGameLoadStarted -= HandleGameLoadStarted;
                stateManager.OnResetStarted -= Hide;
            }

            if (stateManager != null)
            {
                stateManager.RemoveOnGameSerializeTask(SerializeState);
                stateManager.RemoveOnGameDeserializeTask(DeserializeState);
            }

            if (BlockInputWhenVisible)
                inputManager?.RemoveBlockingUI(this);

            if (inputManager != null)
                inputManager.OnInputModeChanged -= HandleInputModeChanged;

            selectables.Clear();
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();
            selectables?.Clear();
            FontChangeConfigurations?.Clear();
        }

        protected virtual void SerializeState (GameStateMap stateMap)
        {
            if (SaveVisibilityState)
            {
                var state = new GameState {
                    Visible = Visible
                };
                stateMap.SetState(state, name);
            }
        }

        protected virtual UniTask DeserializeState (GameStateMap stateMap)
        {
            if (SaveVisibilityState)
            {
                var state = stateMap.GetState<GameState>(name);
                if (state is null) return UniTask.CompletedTask;
                Visible = state.Visible;
            }
            return UniTask.CompletedTask;
        }

        protected override void HandleVisibilityChanged (bool visible)
        {
            base.HandleVisibilityChanged(visible);

            if (ModalUI)
            {
                if (visible) uiManager?.AddModalUI(this);
                else uiManager?.RemoveModalUI(this);
            }

            if (BlockInputWhenVisible)
            {
                if (scriptPlayer.SkipActive && !(AllowedSamplers?.Contains(InputNames.Skip) ?? false))
                    scriptPlayer.SetSkipEnabled(false);
                if (scriptPlayer.AutoPlayActive && !(AllowedSamplers?.Contains(InputNames.AutoPlay) ?? false))
                    scriptPlayer.SetAutoPlayEnabled(false);
            }

            if (AdaptToInputMode && !visible && (ModalUI || IsFocusInside()))
                uiManager.FocusTop();
        }

        protected override void SetVisibilityOnAwake ()
        {
            // Visible on awake UIs are shown by UI manager after engine initialization.
            if (!Engine.Initialized) SetVisibility(false);
            else base.SetVisibilityOnAwake();
        }

        protected virtual void InitializeFontChangeConfiguration () =>
            FontChanger.InitializeConfiguration(FontChangeConfigurations);

        protected virtual void HandleInputModeChanged (InputMode mode)
        {
            if (ButtonControls) ButtonControls.SetActive(mode != InputMode.Gamepad);
            if (ControlsLegend) ControlsLegend.SetActive(mode == InputMode.Gamepad);
            foreach (var selectable in Selectables)
                selectable.Selectable.navigation = mode == InputMode.Gamepad
                    ? selectable.Navigation
                    : new() { mode = Navigation.Mode.None };
            FocusModeType = mode == InputMode.Gamepad ? FocusMode.Visibility : FocusMode.Navigation;
            if (Visible && mode == InputMode.Gamepad && FocusObject)
                EventUtils.Select(FocusObject);
        }

        protected virtual bool IsFocusInside ()
        {
            if (!EventUtils.Selected) return false;
            foreach (var selectable in selectables)
                if (selectable.Selectable && EventUtils.Selected == selectable.Selectable.gameObject)
                    return true;
            return false;
        }

        protected virtual GameObject FindFocusObject ()
        {
            foreach (var sel in selectables)
                if (sel.Navigation.mode != Navigation.Mode.None && sel.Selectable.gameObject.activeInHierarchy)
                    return sel.Selectable.gameObject;
            return null;
        }

        /// <summary>
        /// Bounds specified handler to an input with the specified name to be invoked while the UI is enabled, visible and
        /// not blocked by a modal (or is the modal itself). The conditions can be configured via the options parameter.
        /// </summary>
        protected virtual void BindInput (string inputName, Action handler, BindInputOptions? options = null)
        {
            if (!inputManager.TryGetSampler(inputName, out var input)) return;
            if (!options.HasValue || !options.Value.OnEnd) input.OnStart += HandleInput;
            else input.OnEnd += HandleInput;

            void HandleInput ()
            {
                if (ShouldHandleBoundInput(options?.WhenHidden ?? false, options?.WhenBlocked ?? false))
                    handler();
            }
        }

        /// <inheritdoc cref="BindInput(string,System.Action,System.Nullable{Naninovel.UI.CustomUI.BindInputOptions})"/>
        protected virtual void BindInput (string inputName, Action<float> handler, BindInputChangeOptions? options = null)
        {
            if (inputManager.TryGetSampler(inputName, out var input))
                input.OnChange += HandleInputChange;

            void HandleInputChange (float value)
            {
                if (ShouldHandleBoundInput(options?.WhenHidden ?? false, options?.WhenBlocked ?? false))
                    handler(value);
            }
        }

        protected bool ShouldHandleBoundInput (bool whenHidden, bool whenBlocked)
        {
            if (!isActiveAndEnabled) return false;
            if (!whenHidden && !Visible) return false;
            if (whenBlocked) return true;
            return !uiManager.AnyModal || uiManager.IsActiveModalUI(this);
        }

        private void HandleGameLoadStarted (GameSaveLoadArgs args) => Hide();
    }
}
