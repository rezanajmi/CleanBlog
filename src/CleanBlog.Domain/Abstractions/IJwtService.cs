
namespace CleanBlog.Domain.Abstractions
{
    public interface IJwtService
    {
        string GenerateToken(Guid guid, string username);
    }
}
