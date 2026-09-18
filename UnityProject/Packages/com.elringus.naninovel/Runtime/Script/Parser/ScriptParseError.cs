using System;

namespace Naninovel
{
    /// <summary>
    /// Represent a <see cref="ScriptLine"/> parse error.
    /// </summary>
    public readonly struct ScriptParseError : IEquatable<ScriptParseError>
    {
        /// <summary>
        /// Index of the line in naninovel script.
        /// </summary>
        public readonly int LineIndex;
        /// <summary>
        /// Number of the line in naninovel script (index + 1).
        /// </summary>
        public int LineNumber => LineIndex + 1;
        /// <summary>
        /// Description of the parse error.
        /// </summary>
        public readonly string Description;

        public ScriptParseError (int lineIndex, string description)
        {
            LineIndex = lineIndex;
            Description = description ?? string.Empty;
        }

        public string ToString (string scriptPathOrName)
        {
            var description = Description == string.Empty ? "." : $": {Description}";
            var link = StringUtils.BuildAssetLink(scriptPathOrName, LineNumber);
            return $"Error parsing {link} script at line #{LineNumber}{description}";
        }

        public override int GetHashCode ()
        {
            unchecked
            {
                return (LineIndex * 397) ^ Description.GetHashCode();
            }
        }

        public bool Equals (ScriptParseError other) => LineIndex == other.LineIndex && Description == other.Description;
        public override bool Equals (object obj) => obj is ScriptParseError other && Equals(other);
        public static bool operator == (ScriptParseError left, ScriptParseError right) => left.Equals(right);
        public static bool operator != (ScriptParseError left, ScriptParseError right) => !left.Equals(right);
    }
}
