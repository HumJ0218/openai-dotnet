// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace OpenAI.RealtimeConversation
{
    internal readonly partial struct InternalRealtimeRequestSessionUpdateCommandSessionModality : IEquatable<InternalRealtimeRequestSessionUpdateCommandSessionModality>
    {
        private readonly string _value;

        public InternalRealtimeRequestSessionUpdateCommandSessionModality(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string TextValue = "text";
        private const string AudioValue = "audio";

        public static InternalRealtimeRequestSessionUpdateCommandSessionModality Text { get; } = new InternalRealtimeRequestSessionUpdateCommandSessionModality(TextValue);
        public static InternalRealtimeRequestSessionUpdateCommandSessionModality Audio { get; } = new InternalRealtimeRequestSessionUpdateCommandSessionModality(AudioValue);
        public static bool operator ==(InternalRealtimeRequestSessionUpdateCommandSessionModality left, InternalRealtimeRequestSessionUpdateCommandSessionModality right) => left.Equals(right);
        public static bool operator !=(InternalRealtimeRequestSessionUpdateCommandSessionModality left, InternalRealtimeRequestSessionUpdateCommandSessionModality right) => !left.Equals(right);
        public static implicit operator InternalRealtimeRequestSessionUpdateCommandSessionModality(string value) => new InternalRealtimeRequestSessionUpdateCommandSessionModality(value);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is InternalRealtimeRequestSessionUpdateCommandSessionModality other && Equals(other);
        public bool Equals(InternalRealtimeRequestSessionUpdateCommandSessionModality other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(_value) : 0;
        public override string ToString() => _value;
    }
}