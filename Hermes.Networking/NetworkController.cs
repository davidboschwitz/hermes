using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hermes.Capability;
using Hermes.Database;

namespace Hermes.Networking
{
    public class NetworkController
    {
        private DatabaseController DatabaseController { get; }
        public Guid PersonalID { get; }

        public NetworkConnection CurrentConnection => throw new NotImplementedException();

        public Dictionary<string, TableSyncController> TableSyncControllers { get; }
        public IEnumerable<string> NamesOfSyncedTables => TableSyncControllers.Values.Select(t => t.Name);

        public Dictionary<string, NamespaceNotificationController> NotificationControllers { get; }

        public Dictionary<string, NetworkConnection> NetworkConnections { get; }

        public Dictionary<string, NetworkSequence> NetworkSequences { get; }

        internal NetworkController(DatabaseController databaseController,
                                   IEnumerable<ICapabilityController> capabilityControllers,
                                   IEnumerable<NetworkConnection> networkConnections,
                                   IEnumerable<NetworkSequence> networkSequences)
        {
            DatabaseController = databaseController;
            PersonalID = Guid.NewGuid();

            TableSyncControllers = new Dictionary<string, TableSyncController>();
            NotificationControllers = new Dictionary<string, NamespaceNotificationController>();
            InitializeControllers(capabilityControllers);

            NetworkConnections = new Dictionary<string, NetworkConnection>();
            InitializeConnections(networkConnections);

            NetworkSequences = new Dictionary<string, NetworkSequence>();
            InitializeSequences(networkSequences);
        }

        #region Initialization Methods
        private void InitializeControllers(IEnumerable<ICapabilityController> capabilityControllers)
        {
            Debug.WriteLine("NetworkControllerFactory()");
            foreach (var controller in capabilityControllers)
            {
                Debug.WriteLine($"NC: {controller.GetType().Name}");
                object[] attrs = controller.GetType().GetCustomAttributes(false);
                foreach (Attribute attr in attrs)
                {
                    Debug.WriteLine(attr);
                    if (attr is HermesNotifyNamespaceAttribute nsAttr)
                    {
                        if (!NotificationControllers.TryGetValue(nsAttr.Namespace, out var notificationController))
                        {
                            notificationController = new NamespaceNotificationController(nsAttr.Namespace);
                            NotificationControllers.Add(nsAttr.Namespace, notificationController);
                        }

                        notificationController.Notify += controller.OnNotification;
                        controller.SendMessage += SendMessage;
                    }
                    else if (attr is HermesSyncTableAttribute tableAttr)
                    {
                        if (!TableSyncControllers.TryGetValue(tableAttr.TableType.Name, out var tableController))
                        {
                            tableController = new TableSyncController(tableAttr.TableType, this);
                            TableSyncControllers.Add(tableAttr.TableType.Name, tableController);
                        }
                    }
                }
            }
        }

        private void InitializeConnections(IEnumerable<NetworkConnection> networkConnections)
        {
            foreach (var connection in networkConnections)
            {
                if (!NetworkConnections.ContainsKey(connection.Name))
                {
                    NetworkConnections.Add(connection.Name, connection);
                }
                else
                {
                    throw new Exception($"Duplicate connection '{connection.Name}' name provided!");
                }
            }
        }

        private void InitializeSequences(IEnumerable<NetworkSequence> networkSequences)
        {
            foreach (var sequence in networkSequences)
            {
                if (!NetworkSequences.ContainsKey(sequence.SequenceID))
                {
                    NetworkSequences.Add(sequence.SequenceID, sequence);
                }
                else
                {
                    throw new Exception($"Duplicate connection '{sequence.SequenceID}' name provided!");
                }
            }
        }
        #endregion

        public void Notify(string messageNamespace, string messageName, Guid messageID)
        {
            if (NotificationControllers.TryGetValue(messageNamespace, out var notifier))
            {
                notifier.FireNotify(messageName, messageID);
            }
        }

        public void Sync(NetworkConnection connection, NetworkSequence sequence)
        {
            Debug.WriteLine("Syncing!!");
            sequence.Run(this, DatabaseController, connection);
        }

        public void SendMessage(Type type, DatabaseItem item)
        {
            Debug.WriteLine($"Unimplemented:SendMessage({type}, {item}");
        }

        //TODO: Add handling of network connections and syncing
    }
}
