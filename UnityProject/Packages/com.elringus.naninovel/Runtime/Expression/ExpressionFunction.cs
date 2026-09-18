using System.Reflection;

namespace Naninovel
{
    /// <summary>
    /// Metadata of a C# method associated with an expression function.
    /// </summary>
    public readonly struct ExpressionFunction
    {
        /// <summary>
        /// Identifier of the function. Could be either method name
        /// or alias, when specified via <see cref="ExpressionFunctionAttribute"/>.
        /// </summary>
        public readonly string Id;
        /// <summary>
        /// Underlying C# method of the function.
        /// </summary>
        public readonly MethodInfo Method;
        /// <summary>
        /// Optional documentation, when specified or null.
        /// </summary>
        public readonly string Summary;
        /// <summary>
        /// Optional remarks, when specified or null.
        /// </summary>
        public readonly string Remarks;
        /// <summary>
        /// Optional usage examples, when specified or null.
        /// </summary>
        public readonly string[] Examples;

        public ExpressionFunction (string id, MethodInfo method,
            string summary = null, string remarks = null, string[] examples = null)
        {
            Id = id;
            Method = method;
            Summary = summary;
            Remarks = remarks;
            Examples = examples;
        }
    }
}
