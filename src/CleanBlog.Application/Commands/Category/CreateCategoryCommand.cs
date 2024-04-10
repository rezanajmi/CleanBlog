using CleanBlog.Application.Abstractions;
using Entities = CleanBlog.Domain.Entities.Category;

namespace CleanBlog.Application.Commands.Category
{
    public class CreateCategoryCommand : ICommand<int>, IMapTo<Entities.Category>
    {
        public string Title { get; }
        public int? ParentId { get; }

        public CreateCategoryCommand() { /* for mapping */ }

        public CreateCategoryCommand(
            string title,
            int parentId
            )
        {
            Title = title;
            ParentId = parentId > 0 ? parentId : null;
        }
    }
}
