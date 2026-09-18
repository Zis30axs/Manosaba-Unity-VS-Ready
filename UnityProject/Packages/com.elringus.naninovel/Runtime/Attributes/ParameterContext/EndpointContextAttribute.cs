using System;
using Naninovel.Metadata;

namespace Naninovel
{
    /// <summary>
    /// Applied to <see cref="NamedStringParameter"/> or string function parameter to associate it with a navigation endpoint
    /// (script path and label, eg path parameter of goto command).
    /// Used by bridging service to provide the context for external tools (IDE extension, web editor, etc).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class EndpointContextAttribute : ParameterContextAttribute
    {
        public EndpointContextAttribute (string paramId = null) : base(ValueContextType.Endpoint, null, -1, paramId) { }
    }
}
