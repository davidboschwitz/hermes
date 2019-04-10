using Hermes.Capability.Chat;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Xamarin.Forms;

namespace Hermes.ViewModels
{
    public class ChatVerificationCreatorViewModel : INotifyPropertyChanged
    {
        public ICommand TakePhotoCommand { get; }

        private IChatController controller;
        public IChatController Controller
        {
            get { return controller; }
            set { SetProperty(ref controller, value); }
        }

        private ImageSource photoImageSource;
        public ImageSource PhotoImageSource
        {
            get { return photoImageSource; }
            set { SetProperty(ref photoImageSource, value); }
        }

        public ChatVerificationCreatorViewModel(IChatController controller)
        {
            Controller = controller;

            TakePhotoCommand = new Command(TakePhotoFunction);
        }

        private async void TakePhotoFunction()
        {
            Debug.WriteLine("TakePhotoFunction");
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
                PhotoImageSource = ImageSource.FromStream(() => { return photo.GetStream(); });
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
