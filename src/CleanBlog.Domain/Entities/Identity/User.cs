using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.Entities.Identity.Events;
using CleanBlog.Domain.SharedKernel.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanBlog.Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>, IAuditableEntity
    {
        public string Name { get; private set; }
        public string Family { get; private set; }
        public byte Age { get; private set; }
        public Gender Gender { get; private set; }

        public User() { /* for EF */ }

        #region Domain Business
        public void Create()
        {
            AddEvent(new UserCreatedDomainEvent(Email));
        }

        public void UpdateEmail(string newEmail)
        {
            AddEvent(new EmailChangedDomainEvent(newEmail));
        }
        #endregion

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
