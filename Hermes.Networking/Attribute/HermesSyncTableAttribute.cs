using System;

namespace Hermes.Networking
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class HermesSyncTableAttribute : Attribute
    {
        public HermesSyncTableAttribute(Type tableType)
        {
            TableType = tableType;
        }

        public Type TableType { get; }

        public override string ToString()
        {
            return $"[HermesSyncTable({TableType})]";
        }
    }
}
