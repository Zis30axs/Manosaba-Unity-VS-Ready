using System;

namespace Naninovel
{
    /// <summary>
    /// Arguments for script command parsing.
    /// </summary>
    public readonly struct CommandParseArgs : IEquatable<CommandParseArgs>
    {
        public readonly Parsing.Command Model;
        public readonly PlaybackSpot PlaybackSpot;
        public readonly int Indent;
        public readonly bool Transient;

        public CommandParseArgs (Parsing.Command model, PlaybackSpot playbackSpot, int indent, bool transient)
        {
            Model = model;
            PlaybackSpot = playbackSpot;
            Indent = indent;
            Transient = transient;
        }

        public bool Equals (CommandParseArgs other)
        {
            return Equals(Model, other.Model) &&
                   PlaybackSpot.Equals(other.PlaybackSpot) &&
                   Transient == other.Transient;
        }

        public override bool Equals (object obj)
        {
            return obj is CommandParseArgs other && Equals(other);
        }

        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = (Model != null ? Model.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PlaybackSpot.GetHashCode();
                hashCode = (hashCode * 397) ^ Transient.GetHashCode();
                return hashCode;
            }
        }
    }
}
