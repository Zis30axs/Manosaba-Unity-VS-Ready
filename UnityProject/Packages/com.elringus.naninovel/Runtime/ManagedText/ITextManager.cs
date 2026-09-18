using JetBrains.Annotations;
using Naninovel.ManagedText;

namespace Naninovel
{
    /// <summary>
    /// Provides managed text records and manages fields with <see cref="ManagedTextAttribute"/>.
    /// </summary>
    /// <remarks>
    /// Make sure to preload documents via <see cref="DocumentLoader"/> before attempting
    /// to access associated records.
    /// </remarks>
    public interface ITextManager : IEngineService<ManagedTextConfiguration>
    {
        /// <summary>
        /// Manages resources associated with the text documents.
        /// </summary>
        IResourceLoader DocumentLoader { get; }

        /// <summary>
        /// Returns managed text document with specified local resource path;
        /// returns null when document with specified path doesn't exist or the resource is not loaded.
        /// </summary>
        [CanBeNull] ManagedTextDocument GetDocument (string documentPath);
    }
}
