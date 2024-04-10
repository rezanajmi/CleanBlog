using CleanBlog.Application.Abstractions;
using Entities = CleanBlog.Domain.Entities.Identity;
using CleanBlog.Domain.SharedKernel.Enums;

namespace CleanBlog.Application.Commands.User
{
    public class CreateUserCommand : ICommand<Guid>, IMapTo<Entities.User>
    {
        public string Username { get; }
        public string Name { get; }
        public string Family { get; }
        public byte Age { get; }
        public Gender Gender { get; }
        public string Email { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }

        public CreateUserCommand() { /* for mapping */ }

        public CreateUserCommand(
            string username,
            string name,
            string family,
            byte age,
            Gender gender,
            string email,
            string password,
            string confirmPassword
            )
        {
            Username = username;
            Name = name;
            Family = family;
            Age = age;
            Gender = gender;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}
