using Hermes.Database;
using System;

namespace Hermes.Capability.Permissions.Model
{
    public class PermissionGrant : DatabaseItem
    {
        public int Grant { get; set; }

        public PermissionGrant(Guid id, int grant) : base(id, new Guid(), new Guid(), DateTime.Now, DateTime.Now, Capability.Namespace, Capability.MessageNames.PermissionGrant)
        {
            Grant = grant;
        }

        public PermissionGrant() { }
    }
}
