using Hermes.Capability.Chat;
using Hermes.Capability.Chat.Model;
using Hermes.Views;
using Hermes.Views.Chat;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace Hermes.ViewModels
{
    public class ConversationPageViewModel : INotifyPropertyChanged
    {
        public Guid Me => new Guid("89c50f2b-83ce-4b05-9c9c-b50c3067e7e1");

        private IChatController controller;
        public IChatController Controller
        {
            get { return controller; }
            set { SetProperty(ref controller, value); }
        }

        private MainPage RootPage => Application.Current.MainPage as MainPage;
        private ChatPage ChatPage { get; }
        private NavigationPage NavigationChatPage { get; }

        public ConversationPageViewModel(IChatController controller, ChatPage chatPage)
        {
            Controller = controller;
            ChatPage = chatPage;
            NavigationChatPage = new NavigationPage(chatPage);
        }

        public async void SelctedItemHandler(object sender, SelectedItemChangedEventArgs e)
        {
            if (e == null)
                return;

            Debug.WriteLine($"Selected Conversation {e.SelectedItem}");
            Controller.SelectConversation(e.SelectedItem as ChatConversation);
            await RootPage.NavigateFromMenu(NavigationChatPage);
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
