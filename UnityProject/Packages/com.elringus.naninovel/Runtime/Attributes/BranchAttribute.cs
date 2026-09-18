using System;
using Naninovel.Metadata;

namespace Naninovel
{
    /// <summary>
    /// When applied to a <see cref="Command"/> implementation, indicates that the command execution
    /// causes script playback flow branching. Used by the external tools (IDE extension, web editor),
    /// doesn't affect command behaviour at runtime.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class BranchAttribute : Attribute
    {
        public readonly BranchTraits Traits;
        public readonly string SwitchRoot;
        public readonly string Endpoint;

        /// <param name="traits">Nature of branching caused by the command execution.</param>
        /// <param name="switchRoot">Indicates that the command is a part of a switch block, which starts at the command with the specified ID.</param>
        /// <param name="endpoint">When <see cref="BranchTraits.Endpoint"/>, specifies the expression to resolve the endpoint.</param>
        public BranchAttribute (BranchTraits traits, string switchRoot = null, string endpoint = null)
        {
            Traits = traits;
            SwitchRoot = switchRoot;
            Endpoint = endpoint;
        }
    }
}
