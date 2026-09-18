using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Locale-specific alias for an expression function.
    /// </summary>
    [Serializable]
    public struct FunctionLocalization
    {
        [Tooltip("Name of the C# method baking the expression function.")]
        public string MethodName;
        [Tooltip("Locale-specific alias to use for the function.")]
        public string Alias;
    }
}
