using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Locale-specific alias for a constant backed by an enum.
    /// </summary>
    [Serializable]
    public struct ConstantLocalization
    {
        [Tooltip("Name of the C# enum type baking the constant.")]
        public string TypeName;
        [Tooltip("Locale-specific values to use for the constant.")]
        public List<ConstantValueLocalization> Values;
    }
}
