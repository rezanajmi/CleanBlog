using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Commands.Category;

namespace CleanBlog.Web.Models.Requests.Category
{
    public class UpdateCategoryRequest : IMapTo<UpdateCategoryCommand>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ParentId { get; set; }
    }
}