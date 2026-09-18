using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A message printed by a <see cref="ITextPrinterActor"/>.
    /// </summary>
    [Serializable]
    public struct PrintedMessage : IEquatable<PrintedMessage>
    {
        /// <summary>
        /// The text content of the message.
        /// </summary>
        public LocalizableText Text => text;
        /// <summary>
        /// The author of the message or null when un-authored.
        /// </summary>
        public MessageAuthor? Author => string.IsNullOrEmpty(author.Id) ? null : author;

        [SerializeField] private LocalizableText text;
        [SerializeField] private MessageAuthor author;

        public PrintedMessage (LocalizableText text, MessageAuthor author = default)
        {
            this.text = text;
            this.author = author;
        }

        public bool Equals (PrintedMessage other)
        {
            return text.Equals(other.text) && author.Equals(other.author);
        }

        public override bool Equals (object obj)
        {
            return obj is PrintedMessage other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return HashCode.Combine(text, author);
        }
    }
}
