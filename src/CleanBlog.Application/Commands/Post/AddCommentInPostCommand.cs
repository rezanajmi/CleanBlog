using CleanBlog.Application.Abstractions;

namespace CleanBlog.Application.Commands.Post
{
    public class AddCommentInPostCommand : ICommand
    {
        public int PostId { get; }
        public string Text { get; }
        public string UserId { get; }

        public AddCommentInPostCommand() { /* for mapping */ }

        public AddCommentInPostCommand(
            int postId,
            string text,
            string userId = null
            )
        {
            PostId = postId;
            Text = text;
            UserId = userId;
        }
    }
}
