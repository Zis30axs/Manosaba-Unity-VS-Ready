using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel.UI
{
    /// <summary>
    /// Represents a message recorded in <see cref="IBacklogUI"/>.
    /// </summary>
    [Serializable]
    public struct BacklogMessage : IEquatable<BacklogMessage>
    {
        /// <summary>
        /// Text of the message.
        /// </summary>
        public LocalizableText Text => text;
        /// <summary>
        /// Whether the message has an associated actor.
        /// </summary>
        public bool Authored => !string.IsNullOrEmpty(author);
        /// <summary>
        /// Actor ID associated with the message when <see cref="Authored"/> or null.
        /// </summary>
        public string AuthorId => author;
        /// <summary>
        /// Whether the message has rollback option associated.
        /// </summary>
        public bool HasRollback => spot != PlaybackSpot.Invalid;
        /// <summary>
        /// Playback spot associated with the message for rollback option;
        /// or <see cref="PlaybackSpot.Invalid"/> when not <see cref="HasRollback"/>.
        /// </summary>
        public PlaybackSpot RollbackSpot => spot;
        /// <summary>
        /// Voice local resource paths associated with the message, in order; empty when none.
        /// </summary>
        public IReadOnlyList<string> Voice => voice ?? Array.Empty<string>();

        [SerializeField] private LocalizableText text;
        [SerializeField] private string author;
        [SerializeField] private PlaybackSpot spot;
        [SerializeField] private string[] voice;

        public BacklogMessage (LocalizableText text, string author = null, PlaybackSpot? spot = null, string[] voice = null)
        {
            this.text = text;
            this.author = author;
            this.spot = spot ?? PlaybackSpot.Invalid;
            this.voice = voice;
        }

        public bool Equals (BacklogMessage other)
        {
            return text.Equals(other.text);
        }

        public override bool Equals (object obj)
        {
            return obj is BacklogMessage other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return text.GetHashCode();
        }
    }
}
