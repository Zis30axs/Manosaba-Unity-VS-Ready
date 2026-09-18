using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="Script"/> line representing a commentary left by the author of the script.
    /// </summary>
    [Serializable]
    public class CommentScriptLine : ScriptLine
    {
        /// <summary>
        /// Text contents of the commentary.
        /// </summary>
        public string CommentText => commentText;

        [SerializeField] private string commentText;

        public CommentScriptLine (string commentText, int lineIndex, int indent, string lineHash)
            : base(lineIndex, indent, lineHash)
        {
            this.commentText = commentText;
        }
    }
}
