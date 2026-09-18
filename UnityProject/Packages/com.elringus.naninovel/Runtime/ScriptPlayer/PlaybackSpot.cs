using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents position of a playable element inside a <see cref="Script"/>.
    /// </summary>
    [Serializable]
    public struct PlaybackSpot : IEquatable<PlaybackSpot>
    {
        /// <summary>
        /// An uninitialized playback spot, that doesn't belong to any script asset.
        /// </summary>
        public static readonly PlaybackSpot Invalid = new("Invalid", -1, -1);

        /// <summary>
        /// Whether the spot is initialized and belong to an actual script asset.
        /// </summary>
        public bool Valid => lineIndex >= 0 && inlineIndex >= 0;
        /// <summary>
        /// Unique (project-wide) local resource path of the script.
        /// </summary>
        public string ScriptPath => scriptPath;
        /// <summary>
        /// Index of the line inside the script.
        /// </summary>
        public int LineIndex => lineIndex;
        /// <summary>
        /// Number (index + 1) of the line inside the script.
        /// </summary>
        public int LineNumber => lineIndex + 1;
        /// <summary>
        /// Index of an embedded playable component (such as inlined command) inside the line.
        /// </summary>
        public int InlineIndex => inlineIndex;

        [SerializeField] private string scriptPath;
        [SerializeField] private int lineIndex;
        [SerializeField] private int inlineIndex;

        public PlaybackSpot (string scriptPath, int lineIndex, int inlineIndex)
        {
            this.scriptPath = scriptPath;
            this.lineIndex = lineIndex;
            this.inlineIndex = inlineIndex;
        }

        public override bool Equals (object obj)
        {
            return obj is PlaybackSpot spot && Equals(spot);
        }

        public bool Equals (PlaybackSpot other)
        {
            return scriptPath == other.scriptPath &&
                   lineIndex == other.lineIndex &&
                   inlineIndex == other.inlineIndex;
        }

        public override int GetHashCode ()
        {
            var hashCode = 646664838;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ScriptPath);
            hashCode = hashCode * -1521134295 + LineIndex.GetHashCode();
            hashCode = hashCode * -1521134295 + InlineIndex.GetHashCode();
            return hashCode;
        }

        public static bool operator == (PlaybackSpot left, PlaybackSpot right)
        {
            return left.Equals(right);
        }

        public static bool operator != (PlaybackSpot left, PlaybackSpot right)
        {
            return !(left == right);
        }

        public override string ToString () => $"{ScriptPath} #{LineNumber}.{InlineIndex}";
    }
}
