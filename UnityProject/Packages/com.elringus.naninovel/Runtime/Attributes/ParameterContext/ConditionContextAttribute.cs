using System;
using Naninovel.Metadata;

namespace Naninovel
{
    /// <summary>
    /// Can be applied to a string command parameter to indicate that the value should be treated as expression,
    /// even when it's not wrapped in curly braces. Additionally, indicates that the parameter value should be
    /// treated as a boolean expression, which evaluation result indicates whether the command should execute.
    /// Used by bridging service to provide the context for external tools (IDE extension, web editor, etc).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ConditionContextAttribute : ParameterContextAttribute
    {
        public ConditionContextAttribute (int index = -1, string paramId = null)
            : base(ValueContextType.Expression, Constants.Condition, index, paramId) { }
    }
}
