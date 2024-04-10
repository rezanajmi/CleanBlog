using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Domain.Entities.Post.Events
{
    public class PostDeletedDomainEvent : IDomainEvent
    {
        public int PostId { get; }

        public PostDeletedDomainEvent(int postId)
        {
            PostId = postId;
        }
    }
}
