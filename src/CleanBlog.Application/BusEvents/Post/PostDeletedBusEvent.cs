using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Application.Events.Post
{
    public class PostDeletedBusEvent : IBusEvent
    {
        public int PostId { get; set; }
        public IList<int> CommentsId { get; set; }

        public PostDeletedBusEvent() { /* for mapping */ }

        public PostDeletedBusEvent(int id, IEnumerable<int> commentsId)
        {
            this.PostId = id;
            this.CommentsId = commentsId.ToList();
        }
    }
}
