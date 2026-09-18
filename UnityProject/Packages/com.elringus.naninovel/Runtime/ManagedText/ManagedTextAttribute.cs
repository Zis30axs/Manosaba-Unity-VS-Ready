using System;

namespace Naninovel
{
    /// <summary>
    /// When applied to a static string field, the field will be assigned
    /// by <see cref="ITextManager"/> service based on the managed text documents.
    /// The property will also be included to the generated managed text documents.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ManagedTextAttribute : Attribute
    {
        /// <summary>
        /// Local resource path of the generated text document.
        /// </summary>
        public string DocumentPath { get; }

        /// <param name="documentPath">Local resource path of the generated text document.</param>
        public ManagedTextAttribute (string documentPath = ManagedTextPaths.Default)
        {
            DocumentPath = documentPath;
        }
    }
}
