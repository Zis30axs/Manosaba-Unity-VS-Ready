using System;
using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Manages custom script variables.
    /// </summary>
    /// <remarks>
    /// Variable names are case-insensitive.
    /// </remarks>
    public interface ICustomVariableManager : IEngineService<CustomVariablesConfiguration>
    {
        /// <summary>
        /// Invoked when a custom variable is created, removed or has value changed.
        /// </summary>
        event Action<CustomVariableUpdatedArgs> OnVariableUpdated;

        /// <summary>
        /// The custom variables currently managed by the service.
        /// </summary>
        IReadOnlyCollection<CustomVariable> Variables { get; }

        /// <summary>
        /// Checks whether a variable with the specified name exists.
        /// </summary>
        bool VariableExists (string name);
        /// <summary>
        /// Attempts to retrieve value of a variable with the specified name. 
        /// Throws when variable with specified name doesn't exist.
        /// </summary>
        CustomVariableValue GetVariableValue (string name);
        /// <summary>
        /// Sets value of a variable with the specified name.
        /// When variable with specified name doesn't exist, will create a new one.
        /// When name starts with <see cref="Compiler.GlobalVariablePrefix"/>,
        /// created variable will have global scope.
        /// </summary>
        void SetVariableValue (string name, CustomVariableValue value);
        /// <summary>
        /// Resets variable with specified name to the initial pre-defined value
        /// specified in the service configuration.
        /// When variable with specified name has no pre-defined value, removes it.
        /// Throws when variable with specified name doesn't exist.
        /// </summary>
        void ResetVariable (string name);
        /// <summary>
        /// Resets all existing variables to the initial pre-defined value
        /// specified in the service configuration or removes them in case
        /// no pre-defined value is assigned. Will only affect variables
        /// of lifetime scope, when specified.
        /// </summary>
        void ResetAllVariables (CustomVariableScope? scope = null);
    }
}
