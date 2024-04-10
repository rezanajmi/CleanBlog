using CleanBlog.Domain.Bases;
using CleanBlog.Domain.Entities.Category.Events;

namespace CleanBlog.Domain.Entities.Category
{
    public class Category : BaseAuditableEntity
    {
        public string Title { get; private set; }

        #region Navigation Properties
        public int? ParentId { get; private set; }
        public Category Parent { get; private set; }
        public ICollection<Category> Children { get; private set; } = new List<Category>();
        public ICollection<Post.Post> Posts { get; private set; } = new List<Post.Post>();
        #endregion

        public Category() { /* for EF */ }

        public Category(string title, int? parentId)
        {
            Title = title;
            ParentId = parentId;
            AddEvent(new CategoryCreatedDomainEvent());
        }

        public void Update(string title, int? parentId)
        {
            Title = title;
            ParentId = parentId;
            AddEvent(new CategoryUpdatedDomainEvent(Id));
        }
    }
}
