using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Results.Post;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.SharedKernel.Constants;

namespace CleanBlog.Application.Queries.Post
{
    public class GetPostCommentsQuery : IQuery<IList<GetPostCommentsResult>>, IUseCache
    {
        public int PostId { get; }

        public GetPostCommentsQuery(int postId)
        {
            PostId = postId;

            Infixes.Add($"{CacheKeyInfixes.Comment_PostId}{postId}");
        }

        public List<string> Infixes { get; } = new List<string>();
    }
}
