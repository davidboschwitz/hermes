using Hermes.Capability.News;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Hermes.ViewModels.News
{
    public class NewsAdminViewModel : BaseViewModel
    {
        private INewsController controller;
        public INewsController Controller
        {
            get { return controller; }
            set { SetProperty(ref controller, value); }
        }

        public ICommand AddItemCommand { get; }

        private string titleEditorText = string.Empty;
        public string TitleEditorText { get { return titleEditorText; } set { SetProperty(ref titleEditorText, value); } }

        private string bodyEditorText = string.Empty;
        public string BodyEditorText { get { return bodyEditorText; } set { SetProperty(ref bodyEditorText, value); } }

        public NewsAdminViewModel(INewsController controller)
        {
            Controller = controller;

            AddItemCommand = new Command(AddItemFunction);
        }

        private void AddItemFunction(object obj)
        {
            Controller.InsertReport(TitleEditorText, BodyEditorText);
        }
    }
}
