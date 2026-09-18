using System;

namespace Naninovel
{
    /// <summary>
    /// When assigned to a public static method, exposes it as expression function.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class ExpressionFunctionAttribute : Attribute
    {
        public readonly string Alias;

        /// <param name="alias">When specified, used instead of method name to identify the function.</param>
        public ExpressionFunctionAttribute (string alias = null)
        {
            Alias = alias;
        }
    }
}
