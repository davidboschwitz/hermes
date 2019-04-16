using Hermes.Database;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Hermes.Networking
{
    public class TableSyncController
    {
        public Type T { get; }
        public string Name { get; }

        private NetworkController NetworkController;

        public TableSyncController(Type t, NetworkController networkController)
        {
            T = t;
            Name = t.Name;
            NetworkController = networkController;
        }

        public async void SyncAll(DatabaseController db, NetworkConnection net, DateTime lastSynced)
        {
            Debug.WriteLine($"SyncingTable:{Name}");
            var mapping = db.GetMapping(T);
            var localTable = db.Query(mapping, $"SELECT * FROM {Name}");

            var localMeta = localTable
                .Select(r => new SyncMetadata(((DatabaseItem)r).MessageID, ((DatabaseItem)r).UpdatedTimestamp))
                .AsEnumerable<SyncMetadata>()
                .ToDictionary(r => r.MessageID, r => r);
            await net.SendString(JsonConvert.SerializeObject(localMeta.Values));

            var remoteMetaJson = await net.ReceiveString();
            var remoteMeta = JsonConvert
                .DeserializeObject<IEnumerable<SyncMetadata>>(remoteMetaJson);

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
                var item = row.ToObject(T);
                db.Insert(item);
                if (item is DatabaseItem dbitem)
                    NetworkController.Notify(dbitem.MessageNamespace, dbitem.MessageName, dbitem.MessageID);
            }
        }

        public override string ToString()
        {
            return $"TableSyncController({T})";
        }


        public class SyncMetadata
        {
            public SyncMetadata(Guid messageID, DateTime updatedTimestamp)
            {
                MessageID = messageID;
                UpdatedTimestamp = updatedTimestamp;
            }

            public Guid MessageID { get; set; }
            public DateTime UpdatedTimestamp { get; set; }

            public override string ToString()
            {
                return $"[{MessageID}]{UpdatedTimestamp}";
            }
        }
    }
}
