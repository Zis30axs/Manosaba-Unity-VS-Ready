using System.Linq;
using UnityEngine;

namespace Naninovel
{
    [EditInProjectSettings]
    public class ManagedTextConfiguration : Configuration
    {
        /// <summary>
        /// Default managed text document resource loader path prefix.
        /// </summary>
        public const string DefaultPathPrefix = "Text";

        [Tooltip("Configuration of the resource loader used with the managed text documents.")]
        public ResourceLoaderConfiguration Loader = new() { PathPrefix = DefaultPathPrefix };
        [Tooltip("Local resource paths of the managed text documents for which to use multiline format.")]
        public string[] MultilineDocuments = { $"{ManagedTextPaths.ScriptLocalizationPrefix}/*", ManagedTextPaths.Tips };

        /// <summary>
        /// Checks whether managed text document with specified local resource path should be formatted
        /// in multiline format, in accordance with <see cref="MultilineDocuments"/> configuration.
        /// </summary>
        public virtual bool IsMultilineDocument (string documentPath)
        {
            return !string.IsNullOrEmpty(documentPath) && MultilineDocuments.Any(
                c => c.EndsWithFast("*")
                    ? documentPath.StartsWithFast(c.GetBeforeLast("*"))
                    : c == documentPath
            );
        }
    }
}
