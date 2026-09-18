using System;
using System.Collections.Generic;
using Naninovel.Parsing;

namespace Naninovel
{
    /// <summary>
    /// Arguments for script lines parsing.
    /// </summary>
    public readonly struct LineParseArgs<TModel> : IEquatable<LineParseArgs<TModel>> where TModel : IScriptLine
    {
        public readonly string ScriptPath;
        public readonly string LineText;
        public readonly int LineIndex;
        public readonly bool Transient;
        public readonly TModel LineModel;

        public LineParseArgs (string scriptPath, string lineText,
            int lineIndex, bool transient, TModel lineModel)
        {
            ScriptPath = scriptPath;
            LineText = lineText;
            LineIndex = lineIndex;
            Transient = transient;
            LineModel = lineModel;
        }

        public bool Equals (LineParseArgs<TModel> other)
        {
            return ScriptPath == other.ScriptPath &&
                   LineText == other.LineText &&
                   LineIndex == other.LineIndex &&
                   Transient == other.Transient &&
                   EqualityComparer<TModel>.Default.Equals(LineModel, other.LineModel);
        }

        public override bool Equals (object obj)
        {
            return obj is LineParseArgs<TModel> other && Equals(other);
        }

        public override int GetHashCode ()
        {
            unchecked
            {
                var hashCode = (ScriptPath != null ? ScriptPath.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LineText != null ? LineText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ LineIndex;
                hashCode = (hashCode * 397) ^ Transient.GetHashCode();
                hashCode = (hashCode * 397) ^ EqualityComparer<TModel>.Default.GetHashCode(LineModel);
                return hashCode;
            }
        }
    }
}
