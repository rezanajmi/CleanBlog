using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Queries.Post;

namespace CleanBlog.Web.Models.Requests.Post
{
    public class SearchPostRequest : IMapTo<GetPaginatedPostsQuery>
    {
        public string searchedTitle { get; set; }
        public int page { get; set; } = 1;
        public int pageItemCount { get; set; } = 10;
    }
}
