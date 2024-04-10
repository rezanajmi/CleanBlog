using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Results.Post;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.SharedKernel.Constants;

namespace CleanBlog.Application.Queries.Post
{
    public class GetPostByIdQuery : IQuery<GetPostByIdResult>, IUseCache
    {
        public int Id { get; }

        public GetPostByIdQuery(int id)
        {
            Id = id;

            Infixes.Add($"{CacheKeyInfixes.Post_Id}{id}");
        }

        public List<string> Infixes { get; } = new List<string>();
    }
}
