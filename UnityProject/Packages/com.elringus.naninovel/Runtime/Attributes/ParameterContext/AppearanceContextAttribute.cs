using System;
using Naninovel.Metadata;

namespace Naninovel
{
    /// <summary>
    /// Can be applied to a command or expression function parameter to associate appearance records.
    /// Command or function is expected to also has a parameter with <see cref="ActorContextAttribute"/>.
    /// Used by the bridging service to provide the context for external tools (IDE extension, web editor, etc).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class AppearanceContextAttribute : ParameterContextAttribute
    {
        /// <param name="actorId">When value of actor context parameter is not found, will use this default one.</param>
        public AppearanceContextAttribute (int index = -1, string paramId = null, string actorId = null)
            : base(ValueContextType.Appearance, actorId, index, paramId) { }
    }
}
