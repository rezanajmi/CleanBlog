using CleanBlog.Application.Abstractions;

namespace CleanBlog.Application.Commands.User
{
    public class UpdateEmailCommand : ICommand
    {
        public string Email { get; }

        public UpdateEmailCommand() { /* for mapping */ }

        public UpdateEmailCommand(string email)
        {
            Email = email;
        }
    }
}
