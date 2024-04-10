using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Results.Category;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.SharedKernel.Constants;

namespace CleanBlog.Application.Queries.Category
{
    public class GetCategoriesQuery : IQuery<IList<GetCategoriesResult>>, IUseCache
    {
        public string SearchedTitle { get; }

        public GetCategoriesQuery(string searchedTitle)
        {
            SearchedTitle = searchedTitle;

            Infixes.Add(CacheKeyInfixes.Category_All);
        }

        public List<string> Infixes { get; } = new List<string>();
    }
}
