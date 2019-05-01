using Hermes.Capability.Chat;
using Hermes.Views.Chat;
using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace Hermes.ViewModels.Chat
{
    public class ChatPageViewModel : ChatBaseViewModel
    {
        public ICommand SendCommand { get; }
        public ICommand SendVerificationCommand { get; }
        public ICommand AcceptVerificationCommand { get; }
        public ICommand TakeImageCommand { get; }
        public ICommand SendImageCommand { get; }

        public ChatBubbleTypeSelector ChatBubbleTypeSelector { get; }

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

        public ChatPageViewModel(ChatController controller) : base(controller)
        {
            SendCommand = new Command(SendFunction);
            SendVerificationCommand = new Command(SendVerificationFunction);
            TakeImageCommand = new Command(TakeImageFunction);

            ChatBubbleTypeSelector = new ChatBubbleTypeSelector(controller);

            AcceptVerificationCommand = new Command(AcceptVerificationFunction);
        }

        private void AcceptVerificationFunction(object obj)
        {
            if (obj is Guid messageID)
            {
                Controller.AcceptVerification();
            }
        }

        private void SendVerificationFunction()
        {
            Debug.WriteLine($"vm:SendNewChatVerificationMessage({Controller.CurrentConversation}, {InputBarText})");
            if (photo == null || InputBarText.Length < 3)
            {
                Toast.LongAlert("To request sending a message, please type a message and send a photo.");
                return;
            }

            using (var fs = new FileStream(photo.Path, FileMode.Open, FileAccess.Read))
            {
                var imageData = new byte[fs.Length];
                fs.Read(imageData, 0, (int)fs.Length);
                var imageBase64 = Convert.ToBase64String(imageData);
                Controller.SendNewChatVerificationMessage(Controller.CurrentConversation, inputBarText, imageBase64);
            }
            photo = null;
            InputImageSource = null;
            InputBarText = string.Empty;
            ScrollToLast?.Invoke();
        }

        public event Action ScrollToLast;

        private async void TakeImageFunction()
        {
            Debug.WriteLine("TakePhotoFunction");
            photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
            {
                InputImageSource = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
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
    }
}
