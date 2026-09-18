using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel
{
    [EditInProjectSettings]
    public class CustomVariablesConfiguration : Configuration
    {
        [Tooltip("The list of variables to initialize by default. Global variables (names starting with `g_`) are initialized on first application start, and others on each state reset.")]
        public List<CustomVariablePredefine> PredefinedVariables = new();

        /// <summary>
        /// Checks whether specified custom variable name has <see cref="Compiler.GlobalVariablePrefix"/> prefix.
        /// </summary>
        public static bool HasGlobalPrefix (string name)
        {
            return name.StartsWith(Compiler.GlobalVariablePrefix, StringComparison.OrdinalIgnoreCase);
        }
    }
}
