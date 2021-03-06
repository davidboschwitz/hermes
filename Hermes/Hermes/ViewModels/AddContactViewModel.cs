﻿using Hermes.Capability.Chat;
using Hermes.Capability.Chat.Model;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Hermes.ViewModels
{
    public class AddContactViewModel : BaseViewModel
    {
        public IChatController Controller { get; }

        public ICommand CreateContactCommand { get; }

        private string contactInputGuidText = string.Empty;
        public string ContactInputGuidText
        {
            get { return contactInputGuidText; }
            set { SetProperty(ref contactInputGuidText, value); }
        }

        private string contactInputNameText = string.Empty;
        public string ContactInputNameText
        {
            get { return contactInputNameText; }
            set { SetProperty(ref contactInputNameText, value); }
        }

        public AddContactViewModel(IChatController controller)
        {
            Controller = controller;

            CreateContactCommand = new Command(CreateContactFunction);
        }

        private async void CreateContactFunction(object obj)
        {
            var contact = new ChatContact(Guid.Parse(ContactInputGuidText), ContactInputNameText);
            Controller.CreateContact(contact);
            await RootPage.NavigatePop();
        }
    }
}
