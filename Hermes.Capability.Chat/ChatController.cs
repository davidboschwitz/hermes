using Hermes.Capability.Chat.Model;
using Hermes.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hermes.Capability.Chat
{
    public class ChatController : IChatController
    {
        public Guid Me => new Guid("89c50f2b-83ce-4b05-9c9c-b50c3067e7e1");

        private ChatConversation currentConversation;
        public ChatConversation CurrentConversation
        {
            get { return currentConversation; }
            set { SetProperty(ref currentConversation, value); }
        }

        public ObservableCollection<ChatConversation> Conversations { get; private set; }
        public Dictionary<Guid, ChatContact> Contacts { get; }

        private DatabaseController DatabaseController { get; }

        public ChatController(DatabaseController databaseController)
        {
            DatabaseController = databaseController;

            //contacts
            DatabaseController.CreateTable<ChatContact>();
            Contacts = new Dictionary<Guid, ChatContact>();
            foreach (var contact in DatabaseController.Table<ChatContact>())
            {
                Contacts.Add(contact.ID, contact);
            }

            var conversations = new Dictionary<Guid, ChatConversation>();
            //ChatMessages
            DatabaseController.CreateTable<ChatMessage>();
            var x1 = new ChatMessage(Me, Guid.NewGuid(), "test1");
            DatabaseController.Insert(x1);
            var x2 = new ChatMessage(Guid.NewGuid(), Me, "test2");
            DatabaseController.Insert(x2);
            foreach (var msg in DatabaseController.Table<ChatMessage>())
            {
                var other = (Me == msg.RecipientID ? msg.SenderID : msg.RecipientID);

                ChatConversation conversation;
                if (!conversations.TryGetValue(other, out conversation))
                {
                    ChatContact contact;
                    if (!Contacts.TryGetValue(other, out contact))
                    {
                        contact = new ChatContact(other, "no name");
                        Contacts.Add(other, contact);
                    }
                    conversation = new ChatConversation(contact);
                    conversations.Add(other, conversation);
                }

                conversation.Messages.Add(msg);
            }

            //ChatVerifciationMessages
            DatabaseController.CreateTable<ChatVerificationMessage>();
            foreach (var msg in DatabaseController.Table<ChatVerificationMessage>())
            {
                var other = (Me == msg.RecipientID ? msg.SenderID : msg.RecipientID);

                ChatConversation conversation;
                if (!conversations.TryGetValue(other, out conversation))
                {
                    ChatContact contact;
                    if (!Contacts.TryGetValue(other, out contact))
                    {
                        contact = new ChatContact(other, "no name");
                        Contacts.Add(other, contact);
                    }
                    conversation = new ChatConversation(contact);
                    conversations.Add(other, conversation);
                }

                conversation.Messages.Add(msg);
            }

            //Finalize initialization of conversations
            Conversations = new ObservableCollection<ChatConversation>(conversations.Values);
            SortConversations();
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
        }

        public void Poop()
        {
        }

        public void SelectConversation(ChatConversation conversation)
        {
            CurrentConversation = conversation;
        }

        private void SortConversations()
        {
            Conversations = new ObservableCollection<ChatConversation>(Conversations.OrderBy(d => d.LastTimestamp));
            OnPropertyChanged("Conversations");
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
