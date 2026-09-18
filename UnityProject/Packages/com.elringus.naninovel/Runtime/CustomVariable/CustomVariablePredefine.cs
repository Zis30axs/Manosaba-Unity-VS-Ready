using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A script expression evaluated to a pre-defined value for a custom script variable.
    /// </summary>
    [Serializable]
    public struct CustomVariablePredefine : IEquatable<CustomVariablePredefine>
    {
        /// <summary>
        /// Name of the custom variable to pre-define.
        /// </summary>
        public string Name => name;
        /// <summary>
        /// A script expression which evaluation result would be assigned to the pre-defined variable.
        /// </summary>
        public string Expression => value;

        [SerializeField] private string name;
        [SerializeField] private string value;

        public CustomVariablePredefine (string name, string expression)
        {
            this.name = name;
            value = expression;
        }

        public bool Equals (CustomVariablePredefine other)
        {
            return name == other.name && value == other.value;
        }

        public override bool Equals (object obj)
        {
            return obj is CustomVariablePredefine other && Equals(other);
        }

        public override int GetHashCode ()
        {
            unchecked { return (name.GetHashCode() * 397) ^ value.GetHashCode(); }
        }
    }
}
