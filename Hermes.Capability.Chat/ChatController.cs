﻿using Hermes.Capability.Chat.Model;
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
    [HermesSyncTable(typeof(ChatMessage)), HermesSyncTable(typeof(ChatVerificationMessage)), HermesSyncTable(typeof(ChatImageMessage)), HermesSyncTable(typeof(ChatContact))]
    public class ChatController : ICapabilityController
    {
        public Guid Me => DatabaseController.PersonalID();

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
            foreach (var msg in DatabaseController.Table<ChatMessage>().Where(m => m.RecipientID == Me || m.SenderID == Me))
            {
                AddMessage(msg);
            }

            //ChatVerifciationMessages
            DatabaseController.CreateTable<ChatVerificationMessage>();
            foreach (var msg in DatabaseController.Table<ChatVerificationMessage>().Where(m => m.RecipientID == Me || m.SenderID == Me))
            {
                AddMessage(msg);
            }

            //ChatImageMessages
            DatabaseController.CreateTable<ChatImageMessage>();
            foreach (var msg in DatabaseController.Table<ChatImageMessage>().Where(m => m.RecipientID == Me || m.SenderID == Me))
            {
                AddMessage(msg);
            }

            //Finalize initialization of conversations
            SortConversations();
            SortContacts();
        }

        public void AddMessage(ChatMessage msg)
        {
            if (msg == null || (msg.RecipientID != Me && msg.SenderID != Me))
            {
                return;
            }

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
            if (Capability.Namespace.Equals(messageNamespace))
            {
                if (Capability.MessageNames.ChatMessage.Equals(messageName))
                {
                    var msg = DatabaseController.Table<ChatMessage>().Where(m => m.MessageID.Equals(messageID)).First();
                    AddMessage(msg);
                    SortConversations();
                }
                else if (Capability.MessageNames.ChatVerificationMessage.Equals(messageName))
                {
                    var msg = DatabaseController.Table<ChatVerificationMessage>().Where(m => m.MessageID.Equals(messageID)).First();
                    AddMessage(msg);
                    SortConversations();
                }
                else if (Capability.MessageNames.ChatImageMessage.Equals(messageName))
                {
                    var msg = DatabaseController.Table<ChatImageMessage>().Where(m => m.MessageID.Equals(messageID)).First();
                    AddMessage(msg);
                    SortConversations();
                }
                else if (Capability.MessageNames.ChatContact.Equals(messageName))
                {
                    var contact = DatabaseController.Table<ChatContact>().Where(m => m.MessageID.Equals(messageID)).First();
                    if (contact == null)
                    {
                        return;
                    }
                    if (ContactsMap.ContainsKey(contact.ID))
                    {
                        ContactsMap[contact.ID] = contact;
                    }
                    else
                    {
                        ContactsMap.Add(contact.ID, contact);
                    }
                    SortContacts();
                }
            }
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

        public void SendNewChatVerificationMessage(ChatConversation conversation, string messageBody, string image)
        {
            var msg = new ChatVerificationMessage(conversation.Other, Me, messageBody, image);

            //add message to conversation view
            conversation.Messages.Add(msg);

            //add message to database
            DatabaseController.Insert(msg);

            //send message to networking
            SendMessage?.Invoke(typeof(ChatVerificationMessage), msg);

            SortConversations();
        }

        public void SelectConversation(ChatConversation conversation)
        {
            CurrentConversation = conversation;
            foreach (var msg in CurrentConversation.Messages.Reverse())
            {
                if (msg.Read)
                {
                    break;
                }
                msg.Read = true;
                DatabaseController.Update(msg);
                SendMessage?.Invoke(msg.GetType(), msg);
            }
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

        public void AcceptVerification()
        {
            var msg = CurrentConversation.Messages.Where(m => (m.GetType().Equals(typeof(ChatVerificationMessage)) && m.RecipientID == Me))
                                              .First() as ChatVerificationMessage;
            msg.Accepted = true;
            DatabaseController.Update(msg);
            SendMessage?.Invoke(msg.GetType(), msg);
            CurrentConversation.OnPropertyChanged("Messages");
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

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
