using System;

namespace Naninovel
{
    /// <summary>
    /// Represents a serializable <see cref="Command"/> parameter with a nullable <see cref="float"/> value.
    /// </summary>
    [Serializable]
    public class DecimalParameter : CommandParameter<float>
    {
        public static implicit operator DecimalParameter (float value) => new() { Value = value };
        public static implicit operator float? (DecimalParameter param) => param is null || !param.HasValue ? null : param.Value;
        public static implicit operator DecimalParameter (NullableFloat value) => new() { Value = value };
        public static implicit operator NullableFloat (DecimalParameter param) => param?.Value;

        protected override float ParseRaw (RawValue raw, out string errors)
        {
            return ParseFloatText(InterpolatePlainText(raw.Parts), out errors);
        }
    }
}
