using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents raw (un-parsed) <see cref="ICommandParameter"/> value.
    /// </summary>
    [Serializable]
    public struct RawValue : IEquatable<RawValue>
    {
        /// <summary>
        /// Parameter's value content; can contain multiple parts of different kinds.
        /// </summary>
        public IReadOnlyList<RawValuePart> Parts => parts ?? Array.Empty<RawValuePart>();
        /// <summary>
        /// Whether a part of the value is an expression and have to evaluated at runtime.
        /// </summary>
        public bool Dynamic => IsDynamic();

        [SerializeField] private RawValuePart[] parts;

        public RawValue (RawValuePart[] parts)
        {
            this.parts = parts;
        }

        public override string ToString ()
        {
            var builder = new StringBuilder();
            foreach (var part in parts)
                builder.Append(part);
            return builder.ToString();
        }

        public bool Equals (RawValue other)
        {
            if (parts == null) return other.parts == null;
            if (parts.Length != other.parts.Length) return false;
            for (int i = 0; i < parts.Length; i++)
                if (!parts[i].Equals(other.parts[i]))
                    return false;
            return true;
        }

        public override bool Equals (object obj)
        {
            return obj is RawValue other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return parts == null ? 0 : ((IStructuralEquatable)parts).GetHashCode(EqualityComparer<RawValuePart>.Default);
        }

        private bool IsDynamic ()
        {
            for (int i = 0; i < Parts.Count; i++)
                if (parts[i].Kind == ParameterValuePartKind.Expression)
                    return true;
            return false;
        }
    }
}
