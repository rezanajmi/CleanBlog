
namespace CleanBlog.Domain.Abstractions
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; }

        void SetDeleted();
    }
}
