using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Formatting template for the printed text messages.
    /// </summary>
    [Serializable]
    public struct MessageTemplate : IEquatable<MessageTemplate>
    {
        /// <summary>
        /// ID of the character actor, whose messages to format with this template.
        /// Specify "+" to apply for any authored message, "-" — for un-authored messages
        /// or "*" to apply for all messages, authored or not.
        /// </summary>
        public string Author => author;
        /// <summary>
        /// The template to apply when formatting text messages authored by <see cref="Author"/>.
        /// %TEXT% is replaced with the message text and %AUTHOR% — with the message author, if any.
        /// </summary>
        public string Template => template;

        [SerializeField] private string author;
        [SerializeField] private string template;

        public MessageTemplate (string author, string template)
        {
            this.author = author;
            this.template = template;
        }

        /// <summary>
        /// Whether this template is applicable to an author actor with the specified ID.
        /// </summary>
        public bool Applicable ([CanBeNull] string authorId)
        {
            if (Author == "*" || Author == authorId) return true;
            if (string.IsNullOrEmpty(authorId)) return Author == "-";
            return Author == "+";
        }

        public bool Equals (MessageTemplate other)
        {
            return author == other.author && template == other.template;
        }

        public override bool Equals (object obj)
        {
            return obj is MessageTemplate other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return HashCode.Combine(author, template);
        }
    }
}
