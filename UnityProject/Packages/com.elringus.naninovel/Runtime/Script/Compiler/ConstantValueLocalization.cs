using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Locale-specific alias for a constant value backed by an enum value.
    /// </summary>
    [Serializable]
    public struct ConstantValueLocalization
    {
        [Tooltip("Name of the C# enum value baking the constant value.")]
        public string Value;
        [Tooltip("Locale-specific alias to use for the value.")]
        public string Alias;
    }
}
