using Hermes.Capability.Chat.Model;
using Hermes.Networking;
using System;
using System.Collections.ObjectModel;

namespace Hermes.Capability.Chat
{
    public interface IChatController : ICapabilityController
    {
        Guid Me { get; }
        ChatConversation CurrentConversation { get; }
        ObservableCollection<ChatConversation> Conversations { get; }

        void SelectConversation(ChatConversation conversation);
        void SelectConversation(ChatContact contact);

        void SendNewChatMessage(ChatConversation conversation, string messageBody);
        void SendNewChatImageMessage(ChatConversation conversation, string messageBody, string image);

        void Poop();
        void CreateContact(ChatContact contact);
    }
}
