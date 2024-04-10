using CleanBlog.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanBlog.Domain.Entities.Identity
{
    public class Role : IdentityRole<Guid>, IAuditableEntity
    {
        private Role() { /* for EF */ }
        public Role(string roleName) : base(roleName) { }

        #region Auditable
        public DateTime? CreatedDate { get; private set; } = null;
        public string CreatedBy { get; private set; } = null;
        public DateTime? LastModifiedDate { get; private set; } = null;
        public string LastModifiedBy { get; private set; } = null;

        public void SetAuditInfoOnAdd(DateTime createdDate, string createdBy)
        {
            CreatedDate = createdDate;
            CreatedBy = createdBy;
        }

        public void SetAuditInfoOnDelete(DateTime deletedDate, string deletedBy)
        {
            LastModifiedDate = deletedDate;
            LastModifiedBy = deletedBy;
        }

        public void SetAuditInfoOnModify(DateTime lastModifiedDate, string lastModifiedBy)
        {
            LastModifiedDate = lastModifiedDate;
            LastModifiedBy = lastModifiedBy;
        }
        #endregion

        #region DomainEvent
        private List<IDomainEvent> _events = new();

        [NotMapped]
        public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

        public void AddEvent(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
        #endregion
    }
}
