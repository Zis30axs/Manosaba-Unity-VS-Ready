using System;
using Naninovel.Parsing;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents a part of <see cref="LocalizableText"/>.
    /// Can be either ID of a localizable text or plain text chunk of the resulting string.
    /// </summary>
    /// <remarks>
    /// Mixed semantic hack is required to support Unity's serialization,
    /// where interfaces can't be serialized by value.
    /// </remarks>
    [Serializable]
    public struct LocalizableTextPart : IEquatable<LocalizableTextPart>
    {
        /// <summary>
        /// Unique (script-wide) persistent identifier of the associated localizable text.
        /// Can be null, in which case <see cref="Text"/> should be used.
        public string Id => PlainText ? throw new Error("The localizable text is a plain text and doesn't have unique identifier associated with it; use 'Text' property instead.") : id;
        /// <summary>
        /// Script playback spot containing associated localizable text.
        /// Can be null, in which case <see cref="Text"/> should be used.
        public PlaybackSpot Spot => PlainText ? throw new Error("The localizable text is a plain text and doesn't have playback spot associated with it; use 'Text' property instead.") : spot;
        /// <summary>
        /// Plain (non-localizable) text chunk of the resulting string.
        /// Can be null, in which case <see cref="Id"/> and/or <see cref="Spot"/> should be used.
        /// </summary>
        public string Text => PlainText ? text : throw new Error("The localizable text is associated with an unique identifier and script playback spot; use 'Id' and 'Spot' properties to resolve the plain text.");
        /// <summary>
        /// Whether the part represents plain (non-localizable) text and <see cref="Text"/> should be used.
        /// </summary>
        public bool PlainText => string.IsNullOrEmpty(id);

        [SerializeField] private string id;
        [SerializeField] private PlaybackSpot spot;
        [SerializeField] private string text;

        public static LocalizableTextPart FromIdentified (string id, PlaybackSpot spot)
        {
            return new() { id = id, spot = spot, text = null };
        }

        public static LocalizableTextPart FromPlainText (string text)
        {
            return new() { id = null, spot = default, text = text };
        }

        public override string ToString ()
        {
            if (PlainText) return Text;
            return $"{Spot} {Syntax.Default.TextIdOpen}{Id}{Syntax.Default.TextIdClose}";
        }

        public bool Equals (LocalizableTextPart other)
        {
            if (PlainText != other.PlainText) return false;
            if (PlainText) return text == other.text;
            return id == other.id && spot.Equals(other.spot);
        }

        public override bool Equals (object obj)
        {
            return obj is LocalizableTextPart other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return HashCode.Combine(id, spot, text);
        }
    }
}
