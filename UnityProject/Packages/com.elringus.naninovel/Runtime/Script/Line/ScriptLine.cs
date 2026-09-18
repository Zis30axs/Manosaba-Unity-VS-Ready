using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents a single line in a <see cref="Script"/>.
    /// </summary>
    [Serializable]
    public abstract class ScriptLine
    {
        /// <summary>
        /// Index of the line in naninovel script.
        /// </summary>
        public int LineIndex => lineIndex;
        /// <summary>
        /// Number of the line in naninovel script (index + 1).
        /// </summary>
        public int LineNumber => LineIndex + 1;
        /// <summary>
        /// Persistent hash code of the original text line.
        /// </summary>
        public string LineHash => lineHash;
        /// <summary>
        /// Indentation level of the line.
        /// </summary>
        public int Indent => indent;

        [SerializeField] private int lineIndex;
        [SerializeField] private int indent;
        [SerializeField] private string lineHash;

        protected ScriptLine (int lineIndex, int indent, string lineHash)
        {
            this.lineIndex = lineIndex;
            this.indent = indent;
            this.lineHash = lineHash;
        }
    }
}
