using CleanBlog.Application.QueryEntities.Post;
using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Application.Specifications.Post
{
    internal sealed class GetQueryCommentsByPostIdSpec : Specification<CommentQueryEntity>
    {
        public GetQueryCommentsByPostIdSpec(int postId)
        {
            Query = q => q.Where(x => x.PostId == postId);
        }
    }
}
