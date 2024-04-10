using CleanBlog.Domain.Bases;

namespace CleanBlog.Application.QueryEntities.Category
{
    public class CategoryQueryEntity : BaseQueryEntity
    {
        public string Title { get; private set; }
        public int? ParentId { get; private set; }
        public string ParentTitle { get; private set; }

        public CategoryQueryEntity() { /* for mapping */ }

        public CategoryQueryEntity(
            string id,
            string title,
            int? parentId,
            string parentTitle
            )
        {
            Id = id;
            Title = title;
            ParentId = parentId;
            ParentTitle = parentTitle;
        }
    }
}