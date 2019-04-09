﻿using Hermes.Capability.Chat;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Xamarin.Forms;

namespace Hermes.ViewModels
{
    public class ChatPageViewModel : INotifyPropertyChanged
    {
        public ICommand SendCommand { get; }

        private IChatController controller;
        public IChatController Controller
        {
            get { return controller; }
            set { SetProperty(ref controller, value); }
        }
        private string inputBarText = string.Empty;
        public string InputBarText
        {
            get { return inputBarText; }
            set { SetProperty(ref inputBarText, value); }
        }

        public ChatPageViewModel(IChatController controller)
        {
            Controller = controller;

            SendCommand = new Command(() =>
            {
                Debug.WriteLine($"vm:SendNewChatMessage({Controller.CurrentConversation}, {InputBarText})");
                Controller.SendNewChatMessage(Controller.CurrentConversation, InputBarText);
                InputBarText = string.Empty;
            });
        }

        public event Action ScrollToLast;

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
