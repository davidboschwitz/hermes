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
