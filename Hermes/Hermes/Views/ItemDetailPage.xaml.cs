using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Hermes.Models;
using Hermes.ViewModels;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ChatDetailViewModel viewModel;

        public ItemDetailPage(ChatDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ChatDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}