using System;

namespace Hermes.Networking
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class HermesNotifyNamespaceAttribute : Attribute
    {
        public HermesNotifyNamespaceAttribute(string @namespace)
        {
            Namespace = @namespace;
        }

        public string Namespace { get; }

        public override string ToString()
        {
            return $"[HermesNotifyNamespace(\"{Namespace}\")]";
        }
    }
}
