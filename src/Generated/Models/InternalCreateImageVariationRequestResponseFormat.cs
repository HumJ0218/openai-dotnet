// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace OpenAI.Images
{
    internal readonly partial struct InternalCreateImageVariationRequestResponseFormat : IEquatable<InternalCreateImageVariationRequestResponseFormat>
    {
        private readonly string _value;

        public InternalCreateImageVariationRequestResponseFormat(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string UrlValue = "url";
        private const string B64JsonValue = "b64_json";

        public static InternalCreateImageVariationRequestResponseFormat Url { get; } = new InternalCreateImageVariationRequestResponseFormat(UrlValue);
        public static InternalCreateImageVariationRequestResponseFormat B64Json { get; } = new InternalCreateImageVariationRequestResponseFormat(B64JsonValue);
        public static bool operator ==(InternalCreateImageVariationRequestResponseFormat left, InternalCreateImageVariationRequestResponseFormat right) => left.Equals(right);
        public static bool operator !=(InternalCreateImageVariationRequestResponseFormat left, InternalCreateImageVariationRequestResponseFormat right) => !left.Equals(right);
        public static implicit operator InternalCreateImageVariationRequestResponseFormat(string value) => new InternalCreateImageVariationRequestResponseFormat(value);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is InternalCreateImageVariationRequestResponseFormat other && Equals(other);
        public bool Equals(InternalCreateImageVariationRequestResponseFormat other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        public override string ToString() => _value;
    }
}