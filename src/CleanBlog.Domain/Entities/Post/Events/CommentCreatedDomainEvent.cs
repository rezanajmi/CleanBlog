using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Domain.Entities.Post.Events
{
    public class CommentCreatedDomainEvent : IDomainEvent
    {
        public int PostId { get; }

        public CommentCreatedDomainEvent(int postId)
        {
            PostId = postId;
        }
    }
}
