using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel
{
    /// <summary>
    /// Allows to listen for events when value of a custom state variable with specific name is changed.
    /// </summary>
    public class CustomVariableTrigger : MonoBehaviour
    {
        [Serializable] private class StringChangedEvent : UnityEvent<string> { }
        [Serializable] private class NumericChangedEvent : UnityEvent<float> { }
        [Serializable] private class IntegerChangedEvent : UnityEvent<int> { }
        [Serializable] private class BooleanChangedEvent : UnityEvent<bool> { }
        [Serializable] private class VariableRemovedEvent : UnityEvent { }

        /// <summary>
        /// Name of a custom state variable to listen for.
        /// </summary>
        public virtual string CustomVariableName { get => customVariableName; set => customVariableName = value; }
        /// <summary>
        /// Attempts to retrieve current value of the listened variable.
        /// Returns null when variable doesn't exist or variable manager service is not available.
        /// </summary>
        public virtual CustomVariableValue? CustomVariableValue => GetCurrentValueOrNull();

        [Tooltip("Name of a custom state variable to listen for.")]
        [SerializeField] private string customVariableName;
        [Tooltip("Whether to broadcast the change events on component start (given variable exists at that point).")]
        [SerializeField] private bool triggerOnStart = true;
        [Tooltip("Invoked when value of a custom variable with specified name is changed. Invoked even when the value type is not string, in which case the value is converted to string.")]
        [SerializeField] private StringChangedEvent onStringChanged;
        [Tooltip("Invoked when value of a custom variable with specified name is changed and the value is a number (both float and integer).")]
        [SerializeField] private NumericChangedEvent onNumericChanged;
        [Tooltip("Invoked when value of a custom variable with specified name is changed and the value is an integer number.")]
        [SerializeField] private IntegerChangedEvent onIntegerChanged;
        [Tooltip("Invoked when value of a custom variable with specified name is changed and the value is a boolean.")]
        [SerializeField] private BooleanChangedEvent onBooleanChanged;
        [Tooltip("Invoked when variable with specified name is removed.")]
        [SerializeField] private VariableRemovedEvent onRemoved;

        private ICustomVariableManager variableManager;
        private IStateManager stateManager;

        protected virtual void Awake ()
        {
            variableManager = Engine.GetServiceOrErr<ICustomVariableManager>();
            stateManager = Engine.GetServiceOrErr<IStateManager>();
        }

        protected virtual void OnEnable ()
        {
            variableManager.OnVariableUpdated += HandleVariableUpdated;
            stateManager.AddOnGameDeserializeTask(HandleGameDeserialized);
        }

        protected virtual void OnDisable ()
        {
            if (variableManager != null)
                variableManager.OnVariableUpdated -= HandleVariableUpdated;
            stateManager?.RemoveOnGameDeserializeTask(HandleGameDeserialized);
        }

        protected virtual void Start ()
        {
            if (!triggerOnStart) return;
            var value = CustomVariableValue;
            if (value.HasValue) NotifyValueChanged(value);
        }

        protected virtual void HandleVariableUpdated (CustomVariableUpdatedArgs args)
        {
            if (args.Name.EqualsFastIgnoreCase(CustomVariableName))
                NotifyValueChanged(args.Value);
        }

        protected virtual UniTask HandleGameDeserialized (GameStateMap state)
        {
            NotifyValueChanged(CustomVariableValue);
            return UniTask.CompletedTask;
        }

        protected virtual void NotifyValueChanged (CustomVariableValue? value)
        {
            if (value == null)
            {
                onRemoved?.Invoke();
                return;
            }

            var v = value.Value;
            if (v.Type == CustomVariableValueType.String)
            {
                onStringChanged?.Invoke(v.String);
            }
            else if (v.Type == CustomVariableValueType.Numeric)
            {
                onNumericChanged?.Invoke(v.Number);
                onStringChanged?.Invoke(v.Number.ToString(CultureInfo.InvariantCulture));
                if (IsInteger(v.Number)) onIntegerChanged?.Invoke((int)v.Number);
            }
            else
            {
                onBooleanChanged?.Invoke(v.Boolean);
                onStringChanged?.Invoke(v.Boolean.ToString(CultureInfo.InvariantCulture));
            }
        }

        protected virtual CustomVariableValue? GetCurrentValueOrNull ()
        {
            if (variableManager == null) return null;
            if (!variableManager.VariableExists(CustomVariableName)) return null;
            return variableManager.GetVariableValue(CustomVariableName);
        }

        protected virtual bool IsInteger (float value)
        {
            const float tolerance = 1e-6f;
            return Mathf.Abs(value % 1) < tolerance && value > int.MinValue && value < int.MaxValue;
        }
    }
}
