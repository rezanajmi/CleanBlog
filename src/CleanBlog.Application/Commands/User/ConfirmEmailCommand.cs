using CleanBlog.Application.Abstractions;

namespace CleanBlog.Application.Commands.User
{
    public class ConfirmEmailCommand : ICommand
    {
        public string Token { get; }

        public ConfirmEmailCommand() { /* for mapping */ }

        public ConfirmEmailCommand(string token)
        {
            Token = token;
        }
    }
}
