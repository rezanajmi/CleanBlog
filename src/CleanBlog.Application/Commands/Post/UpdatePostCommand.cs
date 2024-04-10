using CleanBlog.Application.Abstractions;
using Entities = CleanBlog.Domain.Entities.Post;

namespace CleanBlog.Application.Commands.Post
{
    public class UpdatePostCommand : ICommand, IMapTo<Entities.Post>
    {
        public int Id { get; }
        public string Title { get; }
        public string Content { get; }
        public int CategoryId { get; }

        public UpdatePostCommand() { /* for mapping */ }

        public UpdatePostCommand(
            int id,
            string title,
            string content,
            int categoryId
            )
        {
            Id = id;
            Title = title;
            Content = content;
            CategoryId = categoryId;
        }
    }
}
