using System;
using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Configures <see cref="IScriptParser.ParseText"/>.
    /// </summary>
    public readonly struct ParseOptions : IEquatable<ParseOptions>
    {
        /// <summary>
        /// When assigned and error occurs while parsing, will add the error to the collection.
        /// </summary>
        public readonly ICollection<ScriptParseError> Errors;
        /// <summary>
        /// Whether parsed script is created at runtime and won't be available via <see cref="IScriptManager"/>;
        /// when enabled, identified (localizable) text will be parsed as plain text.
        /// </summary>
        public readonly bool Transient;

        public ParseOptions (ICollection<ScriptParseError> errors, bool transient)
        {
            Errors = errors;
            Transient = transient;
        }

        public bool Equals (ParseOptions other)
        {
            return Equals(Errors, other.Errors)
                   && Transient == other.Transient;
        }

        public override bool Equals (object obj)
        {
            return obj is ParseOptions other && Equals(other);
        }

        public override int GetHashCode ()
        {
            unchecked { return ((Errors != null ? Errors.GetHashCode() : 0) * 397) ^ Transient.GetHashCode(); }
        }
    }
}
