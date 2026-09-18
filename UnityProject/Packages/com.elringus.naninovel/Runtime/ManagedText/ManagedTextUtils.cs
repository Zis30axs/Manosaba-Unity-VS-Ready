using System.Reflection;
using Naninovel.ManagedText;

namespace Naninovel
{
    public static class ManagedTextUtils
    {
        public const BindingFlags ManagedFieldBindings = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;

        private static readonly InlineManagedTextParser inlineParser = new();
        private static readonly MultilineManagedTextParser multilineParser = new();

        /// <summary>
        /// Parses (de-serializes) specified string of a serialized managed text document;
        /// document format will be resolved automatically based on either document path (when specified) or the text.
        /// </summary>
        /// <param name="text">The document text string to parse.</param>
        /// <param name="documentPath">Used to resolve the format (inline or multiline); when not specified will resolve based on text.</param>
        /// <param name="name">When specified, will include the name to parsing exception messages.</param>
        public static ManagedTextDocument Parse (string text, string documentPath = null, string name = null)
        {
            var multi = string.IsNullOrEmpty(documentPath)
                ? ManagedTextDetector.IsMultiline(text)
                : Configuration.GetOrDefault<ManagedTextConfiguration>().IsMultilineDocument(documentPath);
            return multi ? ParseMultiline(text) : ParseInline(text, name);
        }

        /// <summary>
        /// Parses (de-serializes) specified string of a serialized managed text document in multiline format.
        /// </summary>
        /// <param name="text">The document text string to parse.</param>
        public static ManagedTextDocument ParseMultiline (string text)
        {
            return multilineParser.Parse(text);
        }

        /// <summary>
        /// Parses (de-serializes) specified string of a serialized managed text document in inline format.
        /// </summary>
        /// <param name="text">The document text string to parse.</param>
        /// <param name="name">When specified, will include the name to parsing exception messages.</param>
        public static ManagedTextDocument ParseInline (string text, string name = null)
        {
            try { return inlineParser.Parse(text); }
            catch (InlineManagedTextParser.SyntaxError e) { throw new Error($"Failed to parse '{name}' managed text document: {e.Message}"); }
        }

        /// <summary>
        /// Serializes specified managed text document into text string;
        /// document format will be resolved based on specified document path.
        /// </summary>
        /// <param name="document">The document to serialize into text string.</param>
        /// <param name="documentPath">Used to resolve the format (inline or multiline).</param>
        /// <param name="spacing">Number of line breaks to insert between records.</param>
        public static string Serialize (ManagedTextDocument document, string documentPath, int spacing = 1)
        {
            var multi = Configuration.GetOrDefault<ManagedTextConfiguration>().IsMultilineDocument(documentPath);
            return multi ? SerializeMultiline(document, spacing) : SerializeInline(document, spacing);
        }

        /// <summary>
        /// Serializes specified managed text document into text string in multiline format.
        /// </summary>
        /// <param name="document">The document to serialize into text string.</param>
        /// <param name="spacing">Number of line breaks to insert between records.</param>
        public static string SerializeMultiline (ManagedTextDocument document, int spacing = 1)
        {
            return new MultilineManagedTextSerializer(spacing).Serialize(document);
        }

        /// <summary>
        /// Serializes specified managed text document into text string in inline format.
        /// </summary>
        /// <param name="document">The document to serialize into text string.</param>
        /// <param name="spacing">Number of line breaks to insert between records.</param>
        public static string SerializeInline (ManagedTextDocument document, int spacing = 1)
        {
            return new InlineManagedTextSerializer(spacing).Serialize(document);
        }

        /// <summary>
        /// Returns local resource path of a managed text document containing localization for
        /// scenario script with specified local resource path.
        /// </summary>
        public static string ResolveScriptL10nPath (string scriptPath)
        {
            return $"{ManagedTextPaths.ScriptLocalizationPrefix}/{scriptPath}";
        }
    }
}
