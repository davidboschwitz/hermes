using Hermes.Capability.Chat;
using Hermes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        public ChatPage(IChatController controller)
        {
            InitializeComponent();
            
            var vm = new ChatPageViewModel(controller);
            BindingContext = vm;

            void ScrollToLast()
            {
                MessagesListView.ScrollTo(controller.CurrentConversation?.Messages.Last(), ScrollToPosition.End, true);
            }
            vm.ScrollToLast += ScrollToLast;
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
