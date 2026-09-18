using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents handle to localizable string resolved via <see cref="ITextLocalizer"/>.
    /// </summary>
    [Serializable]
    public struct LocalizableText : IEquatable<LocalizableText>
    {
        /// <summary>
        /// Empty text instance.
        /// </summary>
        public static readonly LocalizableText Empty = default;
        /// <summary>
        /// Ordered parts of the handle.
        /// </summary>
        public IReadOnlyList<LocalizableTextPart> Parts => parts ?? Array.Empty<LocalizableTextPart>();
        /// <summary>
        /// Whether the text is empty.
        /// </summary>
        public bool IsEmpty => parts == null || parts.Length == 0;

        [SerializeField] private LocalizableTextPart[] parts;

        public LocalizableText (LocalizableTextPart[] parts)
        {
            this.parts = parts;
        }

        public static implicit operator LocalizableText (string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return Empty;
            return FromPlainText(plainText);
        }

        public static implicit operator string (LocalizableText text)
        {
            if (text.IsEmpty) return string.Empty;
            return Engine.GetService<ITextLocalizer>()?.Resolve(text) ?? text.ToString();
        }

        public static bool operator == (LocalizableText left, LocalizableText right)
        {
            return left.Equals(right);
        }

        public static bool operator != (LocalizableText left, LocalizableText right)
        {
            return !left.Equals(right);
        }

        public static LocalizableText operator + (LocalizableText a, LocalizableText b)
        {
            var parts = new LocalizableTextPart[a.Parts.Count + b.Parts.Count];
            for (int i = 0; i < a.Parts.Count; i++)
                parts[i] = a.Parts[i];
            for (int i = 0; i < b.Parts.Count; i++)
                parts[i + a.Parts.Count] = b.Parts[i];
            return new(parts);
        }

        /// <summary>
        /// Creates new handle with single plain text part.
        /// </summary>
        public static LocalizableText FromPlainText (string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return Empty;
            return new(new[] { LocalizableTextPart.FromPlainText(plainText) });
        }

        /// <summary>
        /// Creates new handle by replacing occurrences of <paramref name="replace"/>
        /// in specified <paramref name="template"/> with <paramref name="replacement"/>.
        /// </summary>
        public static LocalizableText FromTemplate (string template, string replace, LocalizableText replacement)
        {
            var parts = new List<LocalizableTextPart>();
            var lastIndex = 0;
            var curIndex = template.IndexOf(replace, lastIndex, StringComparison.Ordinal);
            while (curIndex >= 0)
            {
                if (curIndex > lastIndex)
                    parts.Add(LocalizableTextPart.FromPlainText(template.Substring(lastIndex, curIndex - lastIndex)));
                parts.AddRange(replacement.Parts);
                lastIndex = curIndex + replace.Length;
                if (template.Length <= lastIndex) break;
                curIndex = template.IndexOf(replace, lastIndex, StringComparison.Ordinal);
            }
            if (template.Length > lastIndex + 1)
                parts.Add(LocalizableTextPart.FromPlainText(template[lastIndex..]));
            return new(parts.ToArray());
        }

        /// <summary>
        /// Joins text values delimited with specified separator.
        /// </summary>
        public static LocalizableText Join (string separator, IReadOnlyList<LocalizableText> values)
        {
            if (values.Count == 0) return Empty;
            var separatorPart = LocalizableTextPart.FromPlainText(separator);
            var parts = new LocalizableTextPart[values.Sum(v => v.Parts.Count) * 2 - 1];
            var partIndex = -1;
            for (int valueIdx = 0; valueIdx < values.Count; valueIdx++)
            for (int valuePartIdx = 0; valuePartIdx < values[valueIdx].Parts.Count; valuePartIdx++)
            {
                parts[++partIndex] = values[valueIdx].Parts[valuePartIdx];
                if (partIndex + 1 >= parts.Length) break;
                parts[++partIndex] = separatorPart;
            }
            return new(parts);
        }

        /// <summary>
        /// Preloads resources required to resolve localization for the text.
        /// When <paramref name="holder"/> is specified, will as well hold the resources.
        /// </summary>
        public async UniTask Load (object holder = null)
        {
            if (IsEmpty) return;
            await Engine.GetServiceOrErr<ITextLocalizer>().Load(this, holder);
        }

        /// <summary>
        /// Holds resources required to resolve localization for the text.
        /// </summary>
        public void Hold (object holder)
        {
            if (IsEmpty) return;
            Engine.GetServiceOrErr<ITextLocalizer>().Hold(this, holder);
        }

        /// <summary>
        /// Releases and unloads (when no other holders) resources required to resolve localization for the text.
        /// </summary>
        public void Release (object holder)
        {
            if (IsEmpty) return;
            Engine.GetServiceOrErr<ITextLocalizer>().Release(this, holder);
        }

        /// <summary>
        /// Holds resources required to resolve 'to' text while releasing resources associated with this text,
        /// but only in case they are not the same text and returns the 'to' text.
        /// </summary>
        /// <remarks>
        /// This is intended to be used as a shortcut when re-assigning the localized text, encapsulating the
        /// invocations of <see cref="Hold"/> and <see cref="Release"/>.
        /// </remarks>
        public LocalizableText Juggle (LocalizableText to, object holder)
        {
            to.Hold(holder);
            if (!Equals(to)) Release(holder);
            return to;
        }

        public override string ToString () => string.Join(" ", Parts);

        public bool Equals (LocalizableText other)
        {
            if (parts == null) return other.parts == null || other.parts.Length == 0;
            if (other.parts == null) return parts == null || parts.Length == 0;
            if (parts.Length != other.parts.Length) return false;
            for (int i = 0; i < parts.Length; i++)
                if (!parts[i].Equals(other.parts[i]))
                    return false;
            return true;
        }

        public override bool Equals (object obj)
        {
            return obj is LocalizableText other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return parts == null ? 0 : ((IStructuralEquatable)parts).GetHashCode(EqualityComparer<LocalizableTextPart>.Default);
        }
    }
}
