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
        public string OOBEInputName { get; set; }
        public ICommand CreateContactCommand { get; }
        DatabaseController DatabaseController { get; }
        NavigationPage NavigationPage { get; }

        public OOBEViewModel(DatabaseController databaseController, NavigationPage navigationPage)
        {
            DatabaseController = databaseController;

            CreateContactCommand = new Command(CreateContactFunction);
        }

        private void CreateContactFunction(object obj)
        {
            try
            {
                DatabaseController.CreateTable<ChatContact>();
                var contact = new ChatContact(DatabaseController.PersonalID(), OOBEInputName);
                DatabaseController.Insert(contact);

            }
            catch (Exception e)
            {
                Debug.WriteLine("POOP1");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                Debug.WriteLine("");
            }
            try { 
            DatabaseController.SetProperty("OOBE", "done");
            }
            catch(Exception e)
            {
                Debug.WriteLine("POOP2");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                Debug.WriteLine("");
            }
            RootPage.SetNavigationRoot(NavigationPage);
        }
    }
}
