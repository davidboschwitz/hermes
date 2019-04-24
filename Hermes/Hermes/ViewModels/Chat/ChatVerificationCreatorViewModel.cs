using Hermes.Capability.Chat;

using System.Diagnostics;
using System.Windows.Input;

using Xamarin.Forms;

namespace Hermes.ViewModels.Chat
{
    public class ChatVerificationCreatorViewModel : ChatBaseViewModel
    {
        public ICommand TakePhotoCommand { get; }
        
        private ImageSource photoImageSource;
        public ImageSource PhotoImageSource
        {
            get { return photoImageSource; }
            set { SetProperty(ref photoImageSource, value); }
        }

        public ChatVerificationCreatorViewModel(ChatController controller) : base(controller)
        {
            TakePhotoCommand = new Command(TakePhotoFunction);
        }

        private async void TakePhotoFunction()
        {
            Debug.WriteLine("TakePhotoFunction");
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
                PhotoImageSource = ImageSource.FromStream(() => { return photo.GetStream(); });
        }
    }
}
