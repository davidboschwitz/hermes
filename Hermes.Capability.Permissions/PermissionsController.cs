using Hermes.Capability.Permissions.Model;
using Hermes.Database;
using Hermes.Networking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Hermes.Capability.Permissions
{
    [HermesNotifyNamespace(Capability.Namespace)]
    [HermesSyncTable(typeof(PermissionGrant))]
    public class PermissionsController : ICapabilityController
    {
        public enum Level
        {
            USER = 0b0000,
            ADMIN = 0b0001,
            SUPER = 0b0011,
        }

        public int CurrentGrant = 0;
        public Level CurrentLevel => (Level)CurrentGrant;

        public ObservableCollection<PermissionGrant> Grants;

        Guid PersonalID => DatabaseController.PersonalID();
        DatabaseController DatabaseController { get; }

        public PermissionsController(DatabaseController databaseController)
        {
            DatabaseController = databaseController;
            Debug.WriteLine($"PersonalID={DatabaseController.PersonalID()}");

            Grants = new ObservableCollection<PermissionGrant>();
            DatabaseController.CreateTable<PermissionGrant>();
            foreach (var grant in DatabaseController.Table<PermissionGrant>())
            {
                Grants.Add(grant);
                if (PersonalID.Equals(grant.MessageID))
                {
                    SetPermissions(grant);
                }
            }
        }

        public event Action<Type, DatabaseItem> SendMessage;

        public void OnNotification(string messageNamespace, string messageName, Guid messageID)
        {
            if (Capability.Namespace.Equals(messageNamespace))
            {
                if (Capability.MessageNames.PermissionGrant.Equals(messageName))
                {
                    if (PersonalID.Equals(messageID))
                    {
                        var grant = DatabaseController.Table<PermissionGrant>().Where(m => PersonalID.Equals(m.MessageID)).FirstOrDefault();
                        SetPermissions(grant);
                    }
                }
            }
        }

        private void SetPermissions(PermissionGrant grant)
        {
            if (grant == null)
            {
                return;
            }
            CurrentGrant = grant.Grant;
            DatabaseController.SetProperty("Permissions", grant.Grant.ToString());
            Debug.WriteLine($"Updated Permissions level to {grant.Grant}");
        }

        public void Update(Guid id, string level)
        {
            Update(id, (Level)Enum.Parse(typeof(Level), level));
        }

        public void Update(Guid id, Level level)
        {
            if (id == null)
            {
                return;
            }
            var grant = new PermissionGrant(id, (int)level);
            DatabaseController.InsertOrReplace(grant);
            SendMessage?.Invoke(typeof(PermissionGrant), grant);
        }

        public bool HasPermission(Level l)
        {
            return ((int)CurrentLevel & (int)l) == (int)l;
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

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
