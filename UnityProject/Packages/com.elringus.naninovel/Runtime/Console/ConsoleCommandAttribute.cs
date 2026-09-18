using System;

namespace Naninovel
{
    /// <summary>
    /// Registers a public static method with supported argument types as a console command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ConsoleCommandAttribute : Attribute
    {
        /// <summary>
        /// When specified, alias is used instead of method name to reference the command.
        /// </summary>
        public string Alias { get; }

        public ConsoleCommandAttribute (string alias = null)
        {
            Alias = alias;
        }
    }
}
