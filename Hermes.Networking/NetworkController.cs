﻿using System;
using System.Collections.Generic;
using Hermes.Database;

namespace Hermes.Networking
{
    public class NetworkController : INetworkController
    {
        public INetworkConnection CurrentConnection => throw new NotImplementedException();
        
        public List<TableSyncController> TableSyncControllers { get; }
        private HashSet<string> namesOfSyncedTables = new HashSet<string>();
        public ISet<string> NamesOfSyncedTables
        {
            get
            {
                foreach (var tsc in TableSyncControllers)
                {
                    if (!namesOfSyncedTables.Contains(tsc.Name))
                    {
                        namesOfSyncedTables.Add(tsc.Name);
                    }
                }
                return namesOfSyncedTables;
            }
        }

        public Dictionary<string, NamespaceNotificationController> NamespaceNotificationControllers { get; }

        internal NetworkController(List<TableSyncController> tableSyncControllers, Dictionary<string, NamespaceNotificationController> namespaceNotificationControllers)
        {
            TableSyncControllers = tableSyncControllers;
            NamespaceNotificationControllers = namespaceNotificationControllers;
        }

        public void SendMessage(Type type, DatabaseItem item)
        {
            throw new NotImplementedException();
        }

        //TODO: Add handling of network connections and syncing
    }
}
