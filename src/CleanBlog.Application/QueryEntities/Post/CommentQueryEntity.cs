using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Results.Post;
using CleanBlog.Domain.Bases;

namespace CleanBlog.Application.QueryEntities.Post
{
    public class CommentQueryEntity : BaseQueryEntity, IMapTo<GetPostCommentsResult>
    {
        public string UserFullName { get; private set; }
        public string Text { get; private set; }
        public int PostId { get; private set; }

        public CommentQueryEntity() { /* for mapping */}

        public CommentQueryEntity(
            string id,
            string userFullName,
            string text,
            int postId)
        {
            Id = id;
            UserFullName = userFullName;
            Text = text;
            PostId = postId;
        }
    }
}
