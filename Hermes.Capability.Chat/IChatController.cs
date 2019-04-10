﻿using Hermes.Capability.Chat.Model;
using Hermes.Networking;
using System;
using System.Collections.ObjectModel;

namespace Hermes.Capability.Chat
{
    [HermesNotifyNamespace("Chat")]
    [HermesSyncTable(typeof(ChatMessage)), HermesSyncTable(typeof(ChatVerificationMessage)), HermesSyncTable(typeof(ChatImageMessage))]
    public interface IChatController : ICapabilityController
    {
        Guid Me { get; }
        ChatConversation CurrentConversation { get; }
        ObservableCollection<ChatConversation> Conversations { get; }

        void SelectConversation(ChatConversation conversation);

        void SendNewChatMessage(ChatConversation conversation, string messageBody);
        void SendNewChatImageMessage(ChatConversation conversation, string messageBody, string image);

        void Poop();
    }
}
