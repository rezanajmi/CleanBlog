
namespace CleanBlog.Domain.Abstractions
{
    public interface IAuditableEntity
    {
        DateTime? CreatedDate { get; }
        string CreatedBy { get; }
        DateTime? LastModifiedDate { get; }
        string LastModifiedBy { get; }

        void SetAuditInfoOnAdd(DateTime createdDate, string createdBy);
        void SetAuditInfoOnModify(DateTime lastModifiedDate, string lastModifiedBy);
        void SetAuditInfoOnDelete(DateTime deletedDate, string deletedBy);
    }
}
