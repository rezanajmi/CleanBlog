using CleanBlog.Domain.Bases;
using CleanBlog.Domain.Entities.Identity;
using CleanBlog.Domain.Entities.Post.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanBlog.Domain.Entities.Post
{
    public class Comment : BaseAuditableEntity
    {
        public string Text { get; private set; }

        #region Navigation Properties
        [ForeignKey(nameof(Post))]
        public int PostId { get; private set; }
        public virtual Post Post { get; private set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }
        #endregion

        public Comment() { /* for EF */ }

        public Comment(string text, int postId, Guid userId)
        {
            UserId = userId;
            Text = text;
            AddEvent(new CommentCreatedDomainEvent(postId));
        }
    }
}
