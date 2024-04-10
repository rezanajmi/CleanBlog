using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Domain.Bases
{
    public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity, ISoftDelete
    {
        public DateTime? CreatedDate { get; private set; } = null;
        public string CreatedBy { get; private set; } = null;
        public DateTime? LastModifiedDate { get; private set; } = null;
        public string LastModifiedBy { get; private set; } = null;
        public bool IsDeleted { get; set; }

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

        public void SetDeleted()
        {
            IsDeleted = true;
        }
    }
}
