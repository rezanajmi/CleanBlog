using CleanBlog.Application.Abstractions;

namespace CleanBlog.Application.Commands.User
{
    public class GenerateJwtTokenCommand : ICommand<string>
    {
        public Guid Identity { get; }
        public string Username { get; }

        public GenerateJwtTokenCommand() { /* for mapping */ }

        public GenerateJwtTokenCommand(
            Guid id,
            string username
            )
        {
            Identity = id;
            Username = username;
        }
    }
}
