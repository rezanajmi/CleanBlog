using CleanBlog.Domain.Abstractions;
using Entities = CleanBlog.Domain.Entities.Category;

namespace CleanBlog.Application.Specifications.Category
{
    public sealed class GetCategoryByTitleSpec : Specification<Entities.Category>
    {
        public GetCategoryByTitleSpec(string title)
        {
            Query = q => q.Where(x => x.Title == title);
        }
    }
}
