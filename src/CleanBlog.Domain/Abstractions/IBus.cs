
namespace CleanBlog.Domain.Abstractions
{
    public interface IBus
    {
        Task Publish<TEvent>(TEvent @evnet, CancellationToken ct = default) where TEvent : IBusEvent;
        Task Subscribe<T>(CancellationToken ct = default) where T : class;
    }
}
