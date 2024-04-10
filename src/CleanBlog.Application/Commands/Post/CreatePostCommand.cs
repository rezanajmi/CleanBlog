using CleanBlog.Application.Abstractions;
using Entities = CleanBlog.Domain.Entities.Post;

namespace CleanBlog.Application.Commands.Post
{
    public class CreatePostCommand : ICommand<int>, IMapTo<Entities.Post>
    {
        public string Title { get; }
        public string Content { get; }
        public int CategoryId { get; }
        public string UserId { get; }

        public CreatePostCommand() { /* for mapping */ }

        public CreatePostCommand(
            string title,
            string content,
            int categoryId,
            string userId = null
            )
        {
            Title = title;
            Content = content;
            CategoryId = categoryId;
            UserId = userId;
        }
    }
}
