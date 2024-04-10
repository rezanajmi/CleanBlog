using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.Entities.Post;

namespace CleanBlog.Application.Specifications.Post
{
    internal sealed class GetCommentsByPostIdSpec : Specification<Comment>
    {
        public GetCommentsByPostIdSpec(int postId)
        {
            Query = q => q.Where(c => c.PostId == postId);
        }
    }
}
