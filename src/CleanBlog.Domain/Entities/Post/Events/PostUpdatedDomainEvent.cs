using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Domain.Entities.Post.Events
{
    public class PostUpdatedDomainEvent : IDomainEvent
    {
        public int Id { get; }

        public PostUpdatedDomainEvent(int id)
        {
            Id = id;
        }
    }
}
