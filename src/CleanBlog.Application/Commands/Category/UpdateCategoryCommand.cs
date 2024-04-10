using CleanBlog.Application.Abstractions;
using Entities = CleanBlog.Domain.Entities.Category;

namespace CleanBlog.Application.Commands.Category
{
    public class UpdateCategoryCommand : ICommand, IMapTo<Entities.Category>
    {
        public int Id { get; }
        public string Title { get; }
        public int? ParentId { get; }

        public UpdateCategoryCommand() { /* for mapping */ }

        public UpdateCategoryCommand(
            int id,
            string title,
            int parentId
            )
        {
            Id = id;
            Title = title;
            ParentId = parentId > 0 ? parentId : null;
        }
    }
}
