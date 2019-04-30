using Hermes.Capability.Chat;
using Hermes.Views.Chat;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Xamarin.Forms;

namespace Hermes.ViewModels
{
    public class ChatPageViewModel : INotifyPropertyChanged
    {
        public ICommand SendCommand { get; }
        public ICommand TakeImageCommand { get; }
        public ICommand SendImageCommand { get; }

        public ChatBubbleTypeSelector ChatBubbleTypeSelector { get; }

        private ChatController controller;
        public ChatController Controller
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

        private ImageSource inputImageSource;
        public ImageSource InputImageSource
        {
            get { return inputImageSource; }
            set { SetProperty(ref inputImageSource, value); }
        }
        private MediaFile photo;

        public ChatPageViewModel(ChatController controller)
        {
            Controller = controller;

            SendCommand = new Command(SendFunction);
            TakeImageCommand = new Command(TakeImageFunction);

            ChatBubbleTypeSelector = new ChatBubbleTypeSelector(controller);
        }

        public event Action ScrollToLast;

        private async void TakeImageFunction()
        {
            Debug.WriteLine("TakePhotoFunction");
            photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
                InputImageSource = ImageSource.FromStream(() => { return photo.GetStream(); });
        }

        private void SendFunction()
        {
            Debug.WriteLine($"vm:SendNewChatMessage({Controller.CurrentConversation}, {InputBarText})");

            if (photo != null)
            {
                using (var fs = new FileStream(photo.Path, FileMode.Open, FileAccess.Read))
                {
                    var imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, (int)fs.Length);
                    var imageBase64 = Convert.ToBase64String(imageData);
                    Controller.SendNewChatImageMessage(Controller.CurrentConversation, inputBarText, imageBase64);
                }
                photo = null;
                InputImageSource = null;
            }
            else
            {
                Controller.SendNewChatMessage(Controller.CurrentConversation, InputBarText);
            }
            InputBarText = string.Empty;
            ScrollToLast?.Invoke();
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
