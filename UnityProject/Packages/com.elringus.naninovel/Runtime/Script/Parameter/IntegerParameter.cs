using System;

namespace Naninovel
{
    /// <summary>
    /// Represents a serializable <see cref="Command"/> parameter with a nullable <see cref="int"/> value.
    /// </summary>
    [Serializable]
    public class IntegerParameter : CommandParameter<int>
    {
        public static implicit operator IntegerParameter (int value) => new() { Value = value };
        public static implicit operator int? (IntegerParameter param) => param is null || !param.HasValue ? null : param.Value;
        public static implicit operator IntegerParameter (NullableInteger value) => new() { Value = value };
        public static implicit operator NullableInteger (IntegerParameter param) => param?.Value;

        protected override int ParseRaw (RawValue raw, out string errors)
        {
            return ParseIntegerText(InterpolatePlainText(raw.Parts), out errors);
        }
    }
}
