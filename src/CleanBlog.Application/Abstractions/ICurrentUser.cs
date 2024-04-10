
namespace CleanBlog.Application.Abstractions
{
    public interface ICurrentUser
    {
        string Id { get; }
        Guid Identity { get; }
        string Username { get; }
    }
}
