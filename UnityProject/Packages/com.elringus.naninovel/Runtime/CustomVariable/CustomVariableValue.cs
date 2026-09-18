using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Value of a custom script variable.
    /// </summary>
    [Serializable]
    public struct CustomVariableValue : IEquatable<CustomVariableValue>
    {
        /// <summary>
        /// Type of the variable value.
        /// </summary>
        public CustomVariableValueType Type => type;
        /// <summary>
        /// When <see cref="Type"/> is string, returns the value, throws otherwise.
        /// </summary>
        public string String => Type == CustomVariableValueType.String ? stringValue : throw new Error("Custom variable is not a string.");
        /// <summary>
        /// When <see cref="Type"/> is numeric, returns the value, throws otherwise.
        /// </summary>
        public float Number => Type == CustomVariableValueType.Numeric ? numericValue : throw new Error("Custom variable is not a number.");
        /// <summary>
        /// When <see cref="Type"/> is boolean, returns the value, throws otherwise.
        /// </summary>
        public bool Boolean => Type == CustomVariableValueType.Boolean ? booleanValue : throw new Error("Custom Variable is not a boolean.");

        [SerializeField] private CustomVariableValueType type;
        [SerializeField] private string stringValue;
        [SerializeField] private float numericValue;
        [SerializeField] private bool booleanValue;

        public CustomVariableValue (string value)
        {
            type = CustomVariableValueType.String;
            stringValue = value;
            numericValue = default;
            booleanValue = default;
        }

        public CustomVariableValue (float value)
        {
            type = CustomVariableValueType.Numeric;
            stringValue = default;
            numericValue = value;
            booleanValue = default;
        }

        public CustomVariableValue (bool value)
        {
            type = CustomVariableValueType.Boolean;
            stringValue = default;
            numericValue = default;
            booleanValue = value;
        }

        public bool Equals (CustomVariableValue other)
        {
            return type == other.type &&
                   stringValue == other.stringValue &&
                   numericValue.Equals(other.numericValue) &&
                   booleanValue == other.booleanValue;
        }

        public override bool Equals (object obj)
        {
            return obj is CustomVariableValue other && Equals(other);
        }

        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = (int)type;
                hashCode = (hashCode * 397) ^ (stringValue != null ? stringValue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ numericValue.GetHashCode();
                hashCode = (hashCode * 397) ^ booleanValue.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator == (CustomVariableValue left, CustomVariableValue right)
        {
            return left.Equals(right);
        }

        public static bool operator != (CustomVariableValue left, CustomVariableValue right)
        {
            return !left.Equals(right);
        }
    }
}
