using System;

namespace Naninovel.UI
{
    /// <summary>
    /// Represents a choice option recorded in <see cref="IBacklogUI"/>.
    /// </summary>
    public readonly struct BacklogChoice : IEquatable<BacklogChoice>
    {
        /// <summary>
        /// Choice summary text.
        /// </summary>
        public LocalizableText Summary { get; }
        /// <summary>
        /// Whether the choice was picked by player.
        /// </summary>
        public bool Selected { get; }

        public BacklogChoice (LocalizableText summary, bool selected)
        {
            Summary = summary;
            Selected = selected;
        }

        public bool Equals (BacklogChoice other)
        {
            return Summary.Equals(other.Summary) && Selected == other.Selected;
        }

        public override bool Equals (object obj)
        {
            return obj is BacklogChoice other && Equals(other);
        }

        public override int GetHashCode ()
        {
            unchecked { return (Summary.GetHashCode() * 397) ^ Selected.GetHashCode(); }
        }
    }
}
