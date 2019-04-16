using Hermes.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hermes.Networking
{
    public class UpdateAllSequence : NetworkSequence
    {
        public override string SequenceID => "UPDATE_ALL";

        Dictionary<Guid, DateTime> lastSyncedDictionary = new Dictionary<Guid, DateTime>();

        public override async Task Run(NetworkController controller, DatabaseController db, NetworkConnection net)
        {
            await net.SendString(controller.PersonalID.ToString());
            var otherID = Guid.Parse(await net.ReceiveString());
            Debug.WriteLine($"OtherID={otherID}");

            if(!lastSyncedDictionary.TryGetValue(otherID, out var lastSynced))
            {
                lastSynced = DateTime.MinValue;
            }
            Debug.WriteLine($"LastSynced={lastSynced}");

            var mySyncedTables = controller.NamesOfSyncedTables;
            var mySyncedTablesJson = JsonConvert.SerializeObject(mySyncedTables);
            Debug.WriteLine($"sT={mySyncedTablesJson}");
            await net.SendString(mySyncedTablesJson);

            var otherSyncedTables = JsonConvert.DeserializeObject<IEnumerable<string>>(await net.ReceiveString());
            var tablesToSync = otherSyncedTables.Intersect(mySyncedTables);
            

            foreach (var tableName in tablesToSync)
            {
                if (controller.TableSyncControllers.TryGetValue(tableName, out var tableSyncController))
                {
                    tableSyncController.SyncAll(db, net, lastSynced);
                }
            }
            lastSyncedDictionary[otherID] = DateTime.Now;
        }
    }
}
