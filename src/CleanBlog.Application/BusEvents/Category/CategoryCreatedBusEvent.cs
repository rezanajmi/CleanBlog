using CleanBlog.Domain.Abstractions;
using Entities = CleanBlog.Domain.Entities.Category;
using CleanBlog.Application.Abstractions;
using CleanBlog.Application.QueryEntities.Category;

namespace CleanBlog.Application.Events.Category
{
    public class CategoryCreatedBusEvent : IBusEvent, IMapTo<CategoryQueryEntity>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string ParentTitle { get; set; }

        public CategoryCreatedBusEvent() { /* for mapping */ }

        public CategoryCreatedBusEvent(Entities.Category category)
        {
            Id = category.Id;
            Title = category.Title;
            ParentId = category.ParentId;
            ParentTitle = category.Parent?.Title;
        }
    }
}
