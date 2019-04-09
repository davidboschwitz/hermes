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

        public ObservableCollection<ChatConversation> Conversations { get; }

        private DatabaseController DatabaseController { get; }

        public ChatController(DatabaseController databaseController)
        {
            DatabaseController = databaseController;
            DatabaseController.CreateTable<ChatMessage>();
            DatabaseController.CreateTable<ChatVerificationMessage>();

            Conversations = new ObservableCollection<ChatConversation>()
            {
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
                new ChatConversation(Me, Guid.NewGuid()),
            };
            CurrentConversation = Conversations.First();

            OnPropertyChanged("Conversations");
        }

        public event Action<Type, DatabaseItem> SendMessage;

        public void OnNotification(string messageNamespace, string messageName, Guid messageID)
        {
            throw new NotImplementedException();
        }

        public void SendNewChatMessage(ChatConversation conversation, string messageBody)
        {
            var msg = new ChatMessage(conversation.Other, Me, messageBody);

            //add message to conversation view
            conversation.Messages.Add(msg);

            DatabaseController.Table<ChatMessage>() where RecipeintID is Me;

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
