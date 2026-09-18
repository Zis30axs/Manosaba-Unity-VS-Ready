using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Locale-specific alias for a command parameter.
    /// </summary>
    [Serializable]
    public struct ParameterLocalization
    {
        [Tooltip("Identifier (field name) of the parameter to localize alias for.")]
        public string Id;
        [Tooltip("Locale-specific alias of the parameter.")]
        public string Alias;
        [Tooltip("Locale-specific documentation summary of the parameter.")]
        public string Summary;
    }
}
