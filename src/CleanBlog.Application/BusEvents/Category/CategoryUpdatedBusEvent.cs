using CleanBlog.Application.Abstractions;
using CleanBlog.Domain.Abstractions;
using Entities = CleanBlog.Domain.Entities.Category;
using CleanBlog.Application.QueryEntities.Category;

namespace CleanBlog.Application.Events.Category
{
    public class CategoryUpdatedBusEvent : IBusEvent, IMapTo<CategoryQueryEntity>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string ParentTitle { get; set; }

        public CategoryUpdatedBusEvent() { /* for mapping */ }

        public CategoryUpdatedBusEvent(Entities.Category category)
        {
            Id = category.Id;
            Title = category.Title;
            ParentId = category.ParentId;
            ParentTitle = category.Parent?.Title;
        }
    }
}
