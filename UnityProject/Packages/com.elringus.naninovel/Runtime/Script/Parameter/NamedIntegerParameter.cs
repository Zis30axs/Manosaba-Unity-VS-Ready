using System;

namespace Naninovel
{
    /// <summary>
    /// Represents a serializable <see cref="Command"/> parameter with <see cref="NamedInteger"/> value.
    /// </summary>
    [Serializable]
    public class NamedIntegerParameter : NamedParameter<NamedInteger, NullableInteger>
    {
        public static implicit operator NamedIntegerParameter (NamedInteger value) => new() { Value = value };
        public static implicit operator NamedInteger (NamedIntegerParameter param) => param is null || !param.HasValue ? null : param.Value;

        protected override NamedInteger ParseRaw (RawValue raw, out string errors)
        {
            ParseNamedValueText(InterpolatePlainText(raw.Parts), out var name, out var namedValueText, out errors);
            var namedValue = string.IsNullOrEmpty(namedValueText) ? null : ParseIntegerText(namedValueText, out errors) as int?;
            return new(name, namedValue);
        }
    }
}
