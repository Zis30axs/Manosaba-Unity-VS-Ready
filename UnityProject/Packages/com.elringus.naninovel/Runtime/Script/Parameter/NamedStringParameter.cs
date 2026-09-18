using System;

namespace Naninovel
{
    /// <summary>
    /// Represents a serializable <see cref="Command"/> parameter with <see cref="NamedString"/> value.
    /// </summary>
    [Serializable]
    public class NamedStringParameter : NamedParameter<NamedString, NullableString>
    {
        public static implicit operator NamedStringParameter (NamedString value) => new() { Value = value };
        public static implicit operator NamedString (NamedStringParameter param) => param is null || !param.HasValue ? null : param.Value;

        protected override NamedString ParseRaw (RawValue raw, out string errors)
        {
            ParseNamedValueText(InterpolatePlainText(raw.Parts), out var name, out var value, out errors);
            return new(name, value);
        }
    }
}
