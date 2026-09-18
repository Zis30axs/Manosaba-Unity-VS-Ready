using System;

namespace Naninovel
{
    public readonly struct RawDataRepresentation : IEquatable<RawDataRepresentation>
    {
        public readonly string Extension, MimeType;

        public RawDataRepresentation (string extension, string mimeType)
        {
            Extension = extension;
            MimeType = mimeType;
        }

        public bool Equals (RawDataRepresentation other) => Extension == other.Extension && MimeType == other.MimeType;
        public override bool Equals (object obj) => obj is RawDataRepresentation other && Equals(other);
        public override int GetHashCode () => HashCode.Combine(Extension, MimeType);
    }

    /// <summary>
    /// Implementation is able to convert <see cref="T:byte[]"/> to <typeparamref name="TResult"/>
    /// and provide additional information about the raw data representation of the object. 
    /// </summary>
    public interface IRawConverter<TResult> : IConverter<byte[], TResult>
    {
        RawDataRepresentation[] Representations { get; }
    }
}
