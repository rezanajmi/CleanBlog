
namespace CleanBlog.Domain.Abstractions
{
    public interface IEventSourcing
    {
        Task AppendAsync(string type, object data, CancellationToken ct);
    }
}
