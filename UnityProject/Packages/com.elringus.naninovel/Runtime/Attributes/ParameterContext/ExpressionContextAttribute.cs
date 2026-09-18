using System;
using Naninovel.Metadata;

namespace Naninovel
{
    /// <summary>
    /// Can be applied to a string command parameter to indicate that the value should be treated as expression,
    /// even when it's not wrapped in curly braces.
    /// Used by the bridging service to provide the context for external tools (IDE extension, web editor, etc).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = true)]
    public class ExpressionContextAttribute : ParameterContextAttribute
    {
        public ExpressionContextAttribute (int index = -1, string paramId = null)
            : base(ValueContextType.Expression, null, index, paramId) { }
    }
}
