using System;

namespace Conduit.ApplicationCore.Entities
{
    public class AuditableEntity<T> : BaseEntity<T>
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
