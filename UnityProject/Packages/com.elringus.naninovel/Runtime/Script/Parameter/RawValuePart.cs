using System;
using Naninovel.Parsing;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents part of raw (un-parsed) <see cref="ICommandParameter"/> value.
    /// </summary>
    /// <remarks>
    /// When only <see cref="Id"/> is assigned, represents <see cref="Parsing.IdentifiedText"/>.
    /// When only <see cref="Text"/> is assigned, represents <see cref="Parsing.PlainText"/>.
    /// When only <see cref="Expression"/> is assigned, represents <see cref="Parsing.Expression"/>.
    /// Mixed semantic hack is required to support Unity's value serialization (can't use interfaces).
    /// </remarks>
    [Serializable]
    public struct RawValuePart : IEquatable<RawValuePart>
    {
        /// <summary>
        /// When represents <see cref="Parsing.IdentifiedText"/>, contains identifier of the text.
        /// </summary>
        public string Id => Kind != ParameterValuePartKind.IdentifiedText ? throw new Error("Wrong value kind.") : id;
        /// <summary>
        /// When represents <see cref="Parsing.PlainText"/> contains plain text.
        /// </summary>
        public string Text => Kind != ParameterValuePartKind.PlainText ? throw new Error("Wrong value kind.") : text;
        /// <summary>
        /// When represents <see cref="Parsing.Expression"/> contains expression body (w/o curly braces).
        /// </summary>
        public string Expression => Kind != ParameterValuePartKind.Expression ? throw new Error("Wrong value kind.") : expression;
        /// <summary>
        /// Evaluated type of this value part based on <see cref="Id"/>, <see cref="Text"/> and <see cref="Expression"/>.
        /// </summary>
        public ParameterValuePartKind Kind => EvaluateKind();

        [SerializeField] private string id;
        [SerializeField] private string text;
        [SerializeField] private string expression;

        public static RawValuePart FromIdentifiedText (string id)
        {
            return new() { id = id, text = null, expression = null };
        }

        public static RawValuePart FromPlainText (string text)
        {
            return new() { id = null, text = text, expression = null };
        }

        public static RawValuePart FromExpression (string expressionBody)
        {
            return new() { id = null, text = null, expression = expressionBody };
        }

        public override string ToString ()
        {
            if (Kind == ParameterValuePartKind.IdentifiedText)
                return $"{Syntax.Default.TextIdOpen}{id}{Syntax.Default.TextIdClose}";
            if (Kind == ParameterValuePartKind.Expression)
                return $"{Syntax.Default.ExpressionOpen}{expression}{Syntax.Default.ExpressionClose}";
            return text;
        }

        public bool Equals (RawValuePart other)
        {
            return id == other.id && text == other.text && expression == other.expression;
        }

        public override bool Equals (object obj)
        {
            return obj is RawValuePart other && Equals(other);
        }

        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = id != null ? id.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (text != null ? text.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (expression != null ? expression.GetHashCode() : 0);
                return hashCode;
            }
        }

        private ParameterValuePartKind EvaluateKind ()
        {
            if (!string.IsNullOrEmpty(text)) return ParameterValuePartKind.PlainText;
            if (!string.IsNullOrEmpty(id)) return ParameterValuePartKind.IdentifiedText;
            return ParameterValuePartKind.Expression;
        }
    }
}
