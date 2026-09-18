using System;
using JetBrains.Annotations;

namespace Naninovel
{
    /// <summary>
    /// Holder of the resources associated with a <see cref="LocalizableText"/> reference,
    /// compositing both the holder object and the text-specific context.
    /// </summary>
    /// <remarks>
    /// Using just the holder object to track resource dependencies is not sufficient,
    /// as multiple texts may reference single scenario script or localization document,
    /// so we have to also use unique text ID (when stable) or playback spot (when volatile).
    /// </remarks>
    public readonly struct LocalizableTextHolder : IEquatable<LocalizableTextHolder>
    {
        /// <summary>
        /// The held text part discriminating the holder by text ID and playback spot.
        /// </summary>
        public LocalizableTextPart TextPart { get; }
        /// <summary>
        /// The actual object that was specified as holder, discriminating by the
        /// instance that's depending on the held text.
        /// </summary>
        public object Holder { get; }

        public LocalizableTextHolder (LocalizableTextPart textPart, [NotNull] object holder)
        {
            TextPart = textPart;
            Holder = holder;
        }

        public override int GetHashCode ()
        {
            return HashCode.Combine(TextPart, Holder);
        }

        public bool Equals (LocalizableTextHolder other)
        {
            return TextPart.Equals(other.TextPart) && Equals(Holder, other.Holder);
        }

        public override bool Equals (object obj)
        {
            return obj is LocalizableTextHolder other && Equals(other);
        }

        public override string ToString ()
        {
            return $"{Holder} -> {TextPart}";
        }
    }
}
