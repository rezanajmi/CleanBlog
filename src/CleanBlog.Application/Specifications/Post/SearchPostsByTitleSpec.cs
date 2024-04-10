using CleanBlog.Domain.Abstractions;
using CleanBlog.Application.QueryEntities.Post;

namespace CleanBlog.Application.Specifications.Post
{
    internal sealed class SearchPostsByTitleSpec : Specification<PostQueryEntity>
    {
        public SearchPostsByTitleSpec(string expression)
        {
            Query = q => q.Where(x => string.IsNullOrEmpty(expression) ? true : x.Title.ToLower().Contains(expression.ToLower()));
        }
    }
}
