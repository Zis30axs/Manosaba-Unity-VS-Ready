using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    public class VariableInputPanel : CustomUI, IVariableInputUI
    {
        [Serializable]
        public new class GameState
        {
            public string VariableName;
            public CustomVariableValueType ValueType;
            public LocalizableText SummaryText;
            public string InputFieldText;
            public bool PlayOnSubmit;
        }

        protected virtual LocalizableText Summary { get; private set; }
        protected virtual TMP_InputField InputField => inputField;
        protected virtual Button SubmitButton => submitButton;
        protected virtual bool ActivateOnShow => activateOnShow;
        protected virtual bool SubmitOnInput => submitOnInput;
        protected virtual GameObject SummaryContainer => summaryContainer;

        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button submitButton;
        [Tooltip("Whether to automatically select and activate input field when the UI is shown.")]
        [SerializeField] private bool activateOnShow = true;
        [Tooltip("Whether to attempt submit input field value when a `Submit` input is activated.")]
        [SerializeField] private bool submitOnInput = true;
        [Tooltip("When assigned, the game object will be de-/activated based on whether summary is assigned.")]
        [SerializeField] private GameObject summaryContainer;
        [SerializeField] private StringUnityEvent onSummaryChanged;
        [SerializeField] private StringUnityEvent onPredefinedValueChanged;

        private IScriptPlayer player;
        private ICustomVariableManager vars;
        private IStateManager state;
        private IInputSampler submitInput;
        private string variableName;
        private CustomVariableValueType valueType;
        private bool playOnSubmit;

        public virtual void Show (string variableName, CustomVariableValueType valueType,
            LocalizableText summary, LocalizableText predefinedValue, bool playOnSubmit)
        {
            this.variableName = variableName;
            this.valueType = valueType;
            this.playOnSubmit = playOnSubmit;
            SetSummary(summary);
            SetPredefinedValue(predefinedValue);
            SetInputValidation(valueType);

            Show();

            if (ActivateOnShow)
            {
                InputField.Select();
                InputField.ActivateInputField();
            }
        }

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(InputField, SubmitButton);

            player = Engine.GetServiceOrErr<IScriptPlayer>();
            vars = Engine.GetServiceOrErr<ICustomVariableManager>();
            state = Engine.GetServiceOrErr<IStateManager>();
            submitInput = Engine.GetServiceOrErr<IInputManager>().GetSubmit();

            SubmitButton.interactable = false;
        }

        protected override void OnEnable ()
        {
            base.OnEnable();

            SubmitButton.onClick.AddListener(HandleSubmit);
            InputField.onValueChanged.AddListener(HandleInputChanged);

            if (submitInput != null && SubmitOnInput)
                submitInput.OnStart += HandleSubmit;
        }

        protected override void OnDisable ()
        {
            base.OnDisable();

            SubmitButton.onClick.RemoveListener(HandleSubmit);
            InputField.onValueChanged.RemoveListener(HandleInputChanged);

            if (submitInput != null && SubmitOnInput)
                submitInput.OnStart -= HandleSubmit;
        }

        protected override void SerializeState (GameStateMap stateMap)
        {
            base.SerializeState(stateMap);

            var state = new GameState {
                VariableName = variableName,
                ValueType = valueType,
                SummaryText = Summary,
                InputFieldText = InputField.text,
                PlayOnSubmit = playOnSubmit
            };
            stateMap.SetState(state);
        }

        protected override async UniTask DeserializeState (GameStateMap stateMap)
        {
            await base.DeserializeState(stateMap);

            var state = stateMap.GetState<GameState>();
            if (state is null)
            {
                InputField.text = "";
                SetSummary(LocalizableText.Empty);
                return;
            }

            variableName = state.VariableName;
            valueType = state.ValueType;
            await state.SummaryText.Load(this);
            SetSummary(state.SummaryText);
            InputField.text = state.InputFieldText;
            playOnSubmit = state.PlayOnSubmit;
        }

        protected virtual void SetSummary (LocalizableText value)
        {
            Summary = Summary.Juggle(value, this);
            onSummaryChanged?.Invoke(value);
            if (SummaryContainer)
                SummaryContainer.SetActive(!value.IsEmpty);
        }

        protected virtual void SetPredefinedValue (LocalizableText value)
        {
            onPredefinedValueChanged?.Invoke(value);
        }

        protected virtual void SetInputValidation (CustomVariableValueType type)
        {
            if (type == CustomVariableValueType.Numeric)
                InputField.characterValidation = TMP_InputField.CharacterValidation.Decimal;
            else InputField.characterValidation = TMP_InputField.CharacterValidation.None;
        }

        protected virtual void HandleInputChanged (string text)
        {
            SubmitButton.interactable = !string.IsNullOrWhiteSpace(text);
        }

        protected virtual void HandleSubmit ()
        {
            if (!Visible || string.IsNullOrWhiteSpace(InputField.text)) return;

            state.PeekRollbackStack()?.AllowPlayerRollback();

            var value = ParseValue(InputField.text);
            vars.SetVariableValue(variableName, value);

            ClearFocus();
            Hide();

            if (playOnSubmit) ContinuePlayback();
        }

        protected virtual CustomVariableValue ParseValue (string inputText)
        {
            if (valueType == CustomVariableValueType.Numeric)
                return ParseUtils.TryInvariantFloat(inputText, out var num) ? new CustomVariableValue(num)
                    : throw new Error($"Incorrect input: '{inputText}' is not a number.");
            if (valueType == CustomVariableValueType.Boolean)
                return bool.TryParse(inputText, out var boo) ? new CustomVariableValue(boo)
                    : throw new Error($"Incorrect input: '{inputText}' is not a boolean.");
            return new(inputText);
        }

        protected virtual void ContinuePlayback ()
        {
            if (player.Playlist == null) throw new Error("Failed to continue playback on variable input submit: invalid playlist.");
            var nextIndex = player.Playlist.MoveAt(player.PlayedIndex);
            if (nextIndex >= 0) player.Resume(nextIndex);
        }
    }
}
