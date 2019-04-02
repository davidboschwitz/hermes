using Hermes.Capability;
using System;
using System.Collections.Generic;

namespace Hermes.Networking
{
    public class NetworkControllerFactory
    {
        public INetworkController NetworkController { get; }

        public NetworkControllerFactory(IEnumerable<ICapabilityController> capabilityControllers)
        {
            NetworkController = new NetworkController(new List<TableSyncController>(),
                                                      new Dictionary<string, NamespaceNotificationController>());
            var tableSyncControllers = NetworkController.TableSyncControllers;
            var notificationControllers = NetworkController.NamespaceNotificationControllers;

            foreach (var controller in capabilityControllers)
            {
                object[] attrs = controller.GetType().GetCustomAttributes(true);
                foreach (Attribute attr in attrs)
                {
                    if (attr is HermesNotifyNamespaceAttribute nsAttr)
                    {
                        NamespaceNotificationController notificationController;
                        if (!notificationControllers.TryGetValue(nsAttr.Namespace, out notificationController))
                        {
                            notificationController = new NamespaceNotificationController(nsAttr.Namespace);
                            notificationControllers.Add(nsAttr.Namespace, notificationController);
                        }

                        notificationController.Notify += controller.OnNotification;
                        controller.SendMessage += NetworkController.SendMessage;
                    }
                    else if (attr is HermesSyncTableAttribute tableAttr)
                    {
                        var tableSyncController = new TableSyncController(tableAttr.TableType);
                        tableSyncControllers.Add(tableSyncController);
                    }
                }
            }
        }
    }
}
