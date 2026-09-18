using System;
using Naninovel.Metadata;

namespace Naninovel
{
    /// <summary>
    /// Can be applied to a string command parameter to indicate that the value should be treated as expression,
    /// even when it's not wrapped in curly braces. Additionally, indicates that the expression is an assignment
    /// expression, ie assigns or otherwise mutates custom variables, eg `foo=exp;bar=exp`.
    /// Used by bridging service to provide the context for external tools (IDE extension, web editor, etc).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AssignmentContextAttribute : ParameterContextAttribute
    {
        public AssignmentContextAttribute (int index = -1, string paramId = null)
            : base(ValueContextType.Expression, Constants.Assignment, index, paramId) { }
    }
}
