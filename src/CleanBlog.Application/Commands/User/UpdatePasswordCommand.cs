using CleanBlog.Application.Abstractions;

namespace CleanBlog.Application.Commands.User
{
    public class UpdatePasswordCommand : ICommand
    {
        public string CurrentPassword { get; }
        public string NewPassword { get; }
        public string ConfirmNewPassword { get; }

        public UpdatePasswordCommand() { /* for mapping */ }

        public UpdatePasswordCommand(
            string currentPassword,
            string newPassword,
            string confirmNewPassword
            )
        {
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
            ConfirmNewPassword = confirmNewPassword;
        }
    }
}
