using Hermes.Capability.Chat.Model;
using Hermes.Database;
using Hermes.Networking;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hermes.Capability.Chat
{
    [HermesNotifyNamespace(Capability.Namespace)]
    [HermesSyncTable(typeof(ChatMessage)), HermesSyncTable(typeof(ChatVerificationMessage)), HermesSyncTable(typeof(ChatImageMessage))]
    public class ChatController : IChatController
    {
        public Guid Me => new Guid("89c50f2b-83ce-4b05-9c9c-b50c3067e7e1");

        private ChatConversation currentConversation;
        public ChatConversation CurrentConversation
        {
            get { return currentConversation; }
            set { SetProperty(ref currentConversation, value); }
        }

        private ObservableCollection<ChatConversation> conversations;
        public ObservableCollection<ChatConversation> Conversations
        {
            get { return conversations; }
            private set { SetProperty(ref conversations, value); }
        }
        public Dictionary<Guid, ChatConversation> ConversationsMap { get; }

        private ObservableCollection<ChatContact> contacts;
        public ObservableCollection<ChatContact> Contacts
        {
            get { return contacts; }
            private set { SetProperty(ref contacts, value); }
        }
        public Dictionary<Guid, ChatContact> ContactsMap { get; }

        private DatabaseController DatabaseController { get; }

        public ChatController(DatabaseController databaseController)
        {
            DatabaseController = databaseController;

            //contacts
            DatabaseController.CreateTable<ChatContact>();
            ContactsMap = new Dictionary<Guid, ChatContact>();
            foreach (var contact in DatabaseController.Table<ChatContact>())
            {
                ContactsMap.Add(contact.ID, contact);
            }

            ConversationsMap = new Dictionary<Guid, ChatConversation>();
            //ChatMessages
            DatabaseController.CreateTable<ChatMessage>();
            var x1 = new ChatMessage(Me, Guid.NewGuid(), "test1");
            DatabaseController.Insert(x1);
            var x2 = new ChatMessage(Guid.NewGuid(), Me, "test2");
            DatabaseController.Insert(x2);
            foreach (var msg in DatabaseController.Table<ChatMessage>())
            {
                AddMessage(msg);
            }

            //ChatVerifciationMessages
            DatabaseController.CreateTable<ChatVerificationMessage>();
            foreach (var msg in DatabaseController.Table<ChatVerificationMessage>())
            {
                AddMessage(msg);
            }
            //ChatImageMessages
            DatabaseController.CreateTable<ChatImageMessage>();
            foreach (var msg in DatabaseController.Table<ChatImageMessage>())
            {
                AddMessage(msg);
            }

            //Finalize initialization of conversations
            SortConversations();
            SortContacts();
        }

        public void AddMessage(ChatMessage msg)
        {
            var other = (Me == msg.RecipientID ? msg.SenderID : msg.RecipientID);

            if (!ConversationsMap.TryGetValue(other, out var conversation))
            {
                if (!ContactsMap.TryGetValue(other, out var contact))
                {
                    contact = new ChatContact(other, "no name");
                    ContactsMap.Add(other, contact);
                }
                conversation = new ChatConversation(contact);
                ConversationsMap.Add(other, conversation);
            }

            conversation.Messages.Add(msg);
        }

        public event Action<Type, DatabaseItem> SendMessage;

        public void OnNotification(string messageNamespace, string messageName, Guid messageID)
        {
            //throw new NotImplementedException();
            Debug.WriteLine($"[ChatController]: OnNotification({messageNamespace}, {messageName}, {messageID})");
        }

        public void SendNewChatMessage(ChatConversation conversation, string messageBody)
        {
            var msg = new ChatMessage(conversation.Other, Me, messageBody);

            //add message to conversation view
            conversation.Messages.Add(msg);

            //add message to database
            DatabaseController.Insert(msg);

            //send message to networking
            SendMessage?.Invoke(typeof(ChatMessage), msg);

            SortConversations();
        }

        public void SendNewChatImageMessage(ChatConversation conversation, string messageBody, string image)
        {
            var msg = new ChatImageMessage(conversation.Other, Me, messageBody, image);

            //add message to conversation view
            conversation.Messages.Add(msg);

            //add message to database
            DatabaseController.Insert(msg);

            //send message to networking
            SendMessage?.Invoke(typeof(ChatImageMessage), msg);

            SortConversations();
        }


        public void Poop()
        {
        }

        public void SelectConversation(ChatConversation conversation)
        {
            CurrentConversation = conversation;
        }

        public void SelectConversation(ChatContact contact)
        {
            if (!ConversationsMap.TryGetValue(contact.ID, out var conversation))
            {
                conversation = new ChatConversation(contact);
                ConversationsMap.Add(contact.ID, conversation);
            }
            SelectConversation(conversation);
        }

        public void CreateContact(ChatContact contact)
        {
            Debug.WriteLine($"AddContact({contact.ID}, {contact.Name}");
            ContactsMap.Add(contact.ID, contact);
            DatabaseController.Insert(contact);
            SortContacts();
        }

        private void SortContacts()
        {
            var list = ContactsMap.Values.OrderBy(o => o.Name);
            Contacts = new ObservableCollection<ChatContact>(list);
        }

        private void SortConversations()
        {
            var list = ConversationsMap.Values.OrderByDescending(d => d.LastTimestamp);
            Conversations = new ObservableCollection<ChatConversation>(list);
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
