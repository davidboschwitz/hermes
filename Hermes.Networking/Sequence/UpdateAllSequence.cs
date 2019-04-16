using Hermes.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public override async Task RunSequence(NetworkController controller, DatabaseController db, NetworkConnection net)
        {
            await net.SendString(controller.PersonalID.ToString());
            var otherID = Guid.Parse(await net.ReceiveString());
            Debug.WriteLine($"OtherID={otherID}");

            if (!lastSyncedDictionary.TryGetValue(otherID, out var lastSynced))
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
                    RunTableSyncSequence(tableSyncController, db, net, controller, lastSynced);
                }
            }
            lastSyncedDictionary[otherID] = DateTime.Now;
        }

        public override async void RunTableSyncSequence(TableSyncController t, DatabaseController db, NetworkConnection net, NetworkController controller, DateTime lastSynced)
        {
            Debug.WriteLine($"SyncingTable:{t.Name}");
            var mapping = db.GetMapping(t.T);
            var localTable = db.Query(mapping, $"SELECT * FROM {t.Name}");

            var localMeta = localTable
                .Select(r => new TableSyncController.SyncMetadata(((DatabaseItem)r).MessageID, ((DatabaseItem)r).UpdatedTimestamp))
                .AsEnumerable<TableSyncController.SyncMetadata>()
                .ToDictionary(r => r.MessageID, r => r);
            await net.SendString(JsonConvert.SerializeObject(localMeta.Values));

            var remoteMetaJson = await net.ReceiveString();
            var remoteMeta = JsonConvert
                .DeserializeObject<IEnumerable<TableSyncController.SyncMetadata>>(remoteMetaJson);

            var rowsToGet = remoteMeta.Where(r => (!localMeta.ContainsKey(r.MessageID) || localMeta[r.MessageID].UpdatedTimestamp < r.UpdatedTimestamp));
            var rowsToGetJson = JsonConvert.SerializeObject(rowsToGet.Select(x => x.MessageID));
            await net.SendString(rowsToGetJson);

            var rowsToSendJson = await net.ReceiveString();
            var rowsToSend = JsonConvert.DeserializeObject<List<Guid>>(rowsToSendJson);

            var queryToSend = localTable.Where(l => (rowsToSend.Contains(((DatabaseItem)l).MessageID)));
            var queryToSendJson = JsonConvert.SerializeObject(queryToSend);
            await net.SendString(queryToSendJson);

            var rowsRecievedJson = await net.ReceiveString();
            var rowsRecieved = JsonConvert.DeserializeObject<JArray>(rowsRecievedJson);
            foreach (var row in rowsRecieved)
            {
                var item = row.ToObject(t.T);
                db.Insert(item);
                if (item is DatabaseItem dbitem)
                    controller.Notify(dbitem.MessageNamespace, dbitem.MessageName, dbitem.MessageID);
            }
        }
    }
}
