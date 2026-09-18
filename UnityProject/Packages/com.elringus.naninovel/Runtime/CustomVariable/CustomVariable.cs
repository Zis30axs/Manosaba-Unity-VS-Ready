using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Custom script variable.
    /// </summary>
    [Serializable]
    public struct CustomVariable : IEquatable<CustomVariable>
    {
        /// <summary>
        /// Name of the variable (case-insensitive).
        /// </summary>
        public string Name => name;
        /// <summary>
        /// Scope of the variable lifetime.
        /// </summary>
        public CustomVariableScope Scope => scope;
        /// <summary>
        /// Value of the variable.
        /// </summary>
        public CustomVariableValue Value => value;

        [SerializeField] private string name;
        [SerializeField] private CustomVariableScope scope;
        [SerializeField] private CustomVariableValue value;

        public CustomVariable (string name, CustomVariableScope scope, CustomVariableValue value)
        {
            this.name = name;
            this.scope = scope;
            this.value = value;
        }

        public bool Equals (CustomVariable other)
        {
            return string.Equals(name, other.name, StringComparison.OrdinalIgnoreCase) &&
                   scope == other.scope &&
                   value.Equals(other.value);
        }

        public override bool Equals (object obj)
        {
            return obj is CustomVariable other && Equals(other);
        }

        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = StringComparer.OrdinalIgnoreCase.GetHashCode(name);
                hashCode = (hashCode * 397) ^ (int)scope;
                hashCode = (hashCode * 397) ^ value.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator == (CustomVariable left, CustomVariable right)
        {
            return left.Equals(right);
        }

        public static bool operator != (CustomVariable left, CustomVariable right)
        {
            return !left.Equals(right);
        }
    }
}
