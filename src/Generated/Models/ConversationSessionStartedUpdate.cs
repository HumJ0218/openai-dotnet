// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;

namespace OpenAI.RealtimeConversation
{
    public partial class ConversationSessionStartedUpdate : ConversationUpdate
    {
        internal ConversationSessionStartedUpdate(string eventId, InternalRealtimeResponseSession internalSession) : base(eventId)
        {
            Argument.AssertNotNull(internalSession, nameof(internalSession));

            Kind = ConversationUpdateKind.SessionStarted;
            _internalSession = internalSession;
        }

        internal ConversationSessionStartedUpdate(ConversationUpdateKind kind, string eventId, IDictionary<string, BinaryData> serializedAdditionalRawData, InternalRealtimeResponseSession internalSession) : base(kind, eventId, serializedAdditionalRawData)
        {
            _internalSession = internalSession;
        }

        internal ConversationSessionStartedUpdate()
        {
        }
    }
}