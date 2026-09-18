using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Locale-specific alias for a command.
    /// </summary>
    [Serializable]
    public struct CommandLocalization
    {
        [Tooltip("Identifier (implementation type name) of the command to localize alias for.")]
        public string Id;
        [Tooltip("Locale-specific alias of the command.")]
        public string Alias;
        [Tooltip("Locale-specific documentation summary of the command.")]
        public string Summary;
        [Tooltip("Locale-specific documentation remarks for the command.")]
        public string Remarks;
        [Tooltip("Locale-specific documentation examples for the command.")]
        public string[] Examples;
        [Tooltip("Locale-specific parameters of the command.")]
        public List<ParameterLocalization> Parameters;
    }
}
