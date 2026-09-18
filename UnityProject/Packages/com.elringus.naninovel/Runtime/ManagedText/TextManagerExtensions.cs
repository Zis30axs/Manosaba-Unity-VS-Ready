using JetBrains.Annotations;
using Naninovel.ManagedText;

namespace Naninovel
{
    /// <summary>
    /// Provides extension methods for <see cref="ITextManager"/>.
    /// </summary>
    public static class TextManagerExtensions
    {
        /// <summary>
        /// Returns record with specified key inside managed text document with specified local resource path;
        /// throws when record or document doesn't exist or the associated resource is not loaded.
        /// </summary>
        /// <exception cref="Error">Thrown when requested document doesn't exist or is not loaded.</exception>
        public static ManagedTextDocument GetDocumentOrErr (this ITextManager manager, string documentPath)
        {
            return manager.GetDocument(documentPath) ??
                   throw new Error($"Failed to get '{documentPath}' managed text document: document doesn't exist or the associated resource is not loaded.");
        }

        /// <summary>
        /// Returns record with specified key inside managed text document with specified local resource path;
        /// returns null when record or document doesn't exist or the associated resource is not loaded.
        /// </summary>
        public static ManagedTextRecord? GetRecord (this ITextManager manager, string key, string documentPath)
        {
            if (manager.GetDocument(documentPath) is not { } doc || !doc.TryGet(key, out var record))
                return null;
            return record;
        }

        /// <summary>
        /// Attempts to retrieve record with specified key inside managed text document with specified local resource path;
        /// returns false when record or document doesn't exist or the associated resource is not loaded.
        /// </summary>
        public static bool TryGetRecord (this ITextManager manager, string key, string documentPath, out ManagedTextRecord record)
        {
            record = default;
            return manager.GetDocument(documentPath) is { } doc && doc.TryGet(key, out record);
        }

        /// <summary>
        /// Returns value of a record with specified key inside managed text document with specified local resource path;
        /// returns null when record or document doesn't exist or the associated resource is not loaded.
        /// </summary>
        [CanBeNull]
        public static string GetRecordValue (this ITextManager manager, string key, string documentPath)
        {
            return manager.TryGetRecord(key, documentPath, out var record) ? record.Value : null;
        }
    }
}
