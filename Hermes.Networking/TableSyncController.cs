using System;

namespace Hermes.Networking
{
    public class TableSyncController
    {
        public Type T { get; }
        public string Name { get; }

        public TableSyncController(Type t)
        {
            T = t;
            Name = t.Name;
        }
    }
}
