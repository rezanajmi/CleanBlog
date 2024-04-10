using CleanBlog.Application.Abstractions;

namespace CleanBlog.Application.Commands.Post
{
    public class DeletePostCommand : ICommand
    {
        public int Id { get; }

        public DeletePostCommand(int id)
        {
            Id = id;
        }
    }
}
