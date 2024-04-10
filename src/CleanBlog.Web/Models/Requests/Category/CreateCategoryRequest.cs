using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Commands.Category;

namespace CleanBlog.Web.Models.Requests.Category
{
    public class CreateCategoryRequest : IMapTo<CreateCategoryCommand>
    {
        public string Title { get; set; }
        public int ParentId { get; set; }
    }
}