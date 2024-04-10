using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Domain.Entities.Post.Events
{
    public class PostCreatedDomainEvent : IDomainEvent
    {
        public string Title { get; }

        public PostCreatedDomainEvent(string title)
        {
            Title = title;
        }
    }
}
