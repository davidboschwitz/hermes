using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hermes.Capability.Chat.Model
{
    public class ChatConversation : INotifyPropertyChanged
    {
        public ChatContact Contact { get; set; }
        public Guid Other { get; set; }
        public ObservableCollection<ChatMessage> Messages { get; set; }
        public bool CanSendMessage => Messages.Where(m => (m.GetType().Equals(typeof(ChatVerificationMessage)) && m.RecipientID == Other && ((ChatVerificationMessage)m).Accepted))
                                              .Count() > 1;
        public bool CanSendVerificationMessage => Messages.Where(m => (m.GetType().Equals(typeof(ChatVerificationMessage)) && m.RecipientID == Other && !((ChatVerificationMessage)m).Accepted))
                                              .Count() == 0;

        public ChatMessage LastMessage
        {
            get
            {
                if (Messages.Count == 0)
                {
                    return null;
                }

                return Messages.Last();
            }
        }
        public DateTime? LastTimestamp => LastMessage?.CreatedTimestamp;
        public string LastBody => LastMessage?.Body;

        public ChatConversation(ChatContact contact)
        {
            Messages = new ObservableCollection<ChatMessage>();
            Contact = contact;
            Other = contact.ID;

            Debug.WriteLine(this);
        }

        public void Sort()
        {
            Messages = new ObservableCollection<ChatMessage>(Messages.OrderBy(msg => msg.CreatedTimestamp));
        }

        public override string ToString()
        {
            return $"Conversation({Contact.Name}, {Messages.Count} messages, {LastTimestamp})";
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
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
