using CleanBlog.Application.QueryEntities.Category;
using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Application.Specifications.Category
{
    internal class SearchCategoriesByTitleSpec : Specification<CategoryQueryEntity>
    {
        public SearchCategoriesByTitleSpec(string title)
        {
            Query = q => q.Where(x => string.IsNullOrEmpty(title) ? true : x.Title.ToLower().Contains(title.ToLower()));
        }
    }
}
