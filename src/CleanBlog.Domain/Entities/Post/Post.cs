using CleanBlog.Domain.Bases;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.Entities.Post.Events;
using CleanBlog.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanBlog.Domain.Entities.Post
{
    public class Post : BaseAuditableEntity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Content { get; private set; }

        #region Navigation Properties

        [ForeignKey(nameof(User))]
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; private set; }
        public virtual Category.Category Category { get; private set; }

        public virtual ICollection<Comment> Comments { get; private set; } = new List<Comment>();
        #endregion

        public Post() { /* for EF */ }

        public Post(string title, string content, int categoryId, Guid userId)
        {
            Title = title;
            Content = content;
            CategoryId = categoryId;
            UserId = userId;
            AddEvent(new PostCreatedDomainEvent(title));
        }

        public void Update(string title, string content, int categoryId)
        {
            Title = title;
            Content = content;
            CategoryId = categoryId;
            AddEvent(new PostUpdatedDomainEvent(Id));
        }

        public void Delete()
        {
            AddEvent(new PostDeletedDomainEvent(Id));
        }

        public Comment AddComment(string text, Guid userId)
        {
            var comment = new Comment(text, Id, userId);
            Comments.Add(comment);
            return comment;
        }
    }
}
