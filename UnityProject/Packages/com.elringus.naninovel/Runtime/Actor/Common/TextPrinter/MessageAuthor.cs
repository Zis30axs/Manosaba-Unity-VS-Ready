using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Information about an author of a printed message.
    /// </summary>
    [Serializable]
    public struct MessageAuthor : IEquatable<MessageAuthor>
    {
        /// <summary>
        /// Actor ID of the author.
        /// </summary>
        public string Id => id;
        /// <summary>
        /// Custom name label of the author, if any.
        /// </summary>
        public LocalizableText Label => label;

        [SerializeField] private string id;
        [SerializeField] private LocalizableText label;

        public MessageAuthor (string id, LocalizableText label = default)
        {
            this.id = id;
            this.label = label;
        }

        public bool Equals (MessageAuthor other)
        {
            return id == other.id &&
                   label.Equals(other.label);
        }

        public override bool Equals (object obj)
        {
            return obj is MessageAuthor other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return HashCode.Combine(id, label);
        }
    }
}
