using Hermes.Capability.Chat.Model;
using Hermes.Database;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace Hermes.ViewModels
{
    public class OOBEViewModel : BaseViewModel
    {
        public ICommand CreateContactCommand { get; }
        DatabaseController DatabaseController { get; }
        NavigationPage NavigationPage { get; }

        public string OOBEInputName { get; set; }
        public string OOBEInputURL { get; set; }

        public OOBEViewModel(DatabaseController databaseController, NavigationPage navigationPage)
        {
            DatabaseController = databaseController;
            NavigationPage = navigationPage;

            CreateContactCommand = new Command(CreateContactFunction);

            OOBEInputURL = string.Empty;
            OOBEInputName = string.Empty;
        }

        private void CreateContactFunction(object obj)
        {
            if (OOBEInputName.Length < 3)
            {
                Toast.ShortAlert(Resources.OOBEPage_Toast_NameTooShort);
                return;
            }
            if (OOBEInputURL.Length < 4 || !OOBEInputURL.Contains("."))
            {
                Toast.ShortAlert(Resources.OOBEPage_Toast_URLInvalid);
                return;
            }

            try
            {
                DatabaseController.CreateTable<ChatContact>();
                var contact = new ChatContact(DatabaseController.PersonalID(), OOBEInputName);
                DatabaseController.InsertOrReplace(contact);
                DatabaseController.SetProperty("OOBE", "done");
                DatabaseController.SetProperty("URL", OOBEInputURL);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error in OOBEPage Database Operations");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }
            RootPage.SetNavigationRoot(NavigationPage);
        }
    }
}
