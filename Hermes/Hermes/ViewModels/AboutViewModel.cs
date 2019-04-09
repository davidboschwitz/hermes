using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Hermes.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://sdmay19-40.sd.ece.iastate.edu/")));
        }

        public ICommand OpenWebCommand { get; }
    }
}