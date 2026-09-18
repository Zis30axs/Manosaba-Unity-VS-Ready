using System;

namespace Naninovel
{
    /// <summary>
    /// When applied to a command parameter, Naninovel won't generate metadata for it.
    /// Used to ignore the parameter by external tools (IDE extension, web editor),
    /// eg prevent it from being in auto-complete list.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class IgnoreParameterAttribute : Attribute
    {
        public readonly string ParameterId;

        /// <param name="paramId">When attribute is applied to a class, specify parameter field name.</param>
        public IgnoreParameterAttribute (string paramId = null)
        {
            ParameterId = paramId;
        }
    }
}
