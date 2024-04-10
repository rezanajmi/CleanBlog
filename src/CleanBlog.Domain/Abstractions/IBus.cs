
namespace CleanBlog.Domain.Abstractions
{
    public interface IBus
    {
        void Publish<TEvent>(TEvent @evnet) where TEvent : IBusEvent;
        void Subscribe<T>() where T : class;
    }
}
