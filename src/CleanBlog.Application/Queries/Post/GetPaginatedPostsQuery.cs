using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Results.Post;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.SharedKernel.Constants;
using CleanBlog.Domain.SharedKernel.Models;

namespace CleanBlog.Application.Queries.Post
{
    public class GetPaginatedPostsQuery : IQuery<PagedList<SearchedPostResult>>, IUseCache
    {
        public int Page { get; }
        public int PageItemCount { get; }
        public string SearchedTitle { get; }

        public GetPaginatedPostsQuery(
            string searchedTitle,
            int page = 1,
            int pageItemCount = 10
            )
        {
            this.Page = page;
            this.PageItemCount = pageItemCount;
            this.SearchedTitle = searchedTitle;

            Infixes.Add(CacheKeyInfixes.Post_All);
        }

        public List<string> Infixes { get; } = new List<string>();
    }
}
