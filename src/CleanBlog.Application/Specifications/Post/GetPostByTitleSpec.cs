using Entities = CleanBlog.Domain.Entities.Post;
using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Application.Specifications.Post
{
    public sealed class GetPostByTitleSpec : Specification<Entities.Post>
    {
        public GetPostByTitleSpec(string title)
        {
            Query = q => q.Where(e => e.Title == title);
        }
    }
}
