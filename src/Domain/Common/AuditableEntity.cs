using System;

namespace MyBills.Domain.Common
{
    public abstract class AuditableEntity
    {
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
