using CleanBlog.Application.Abstractions;
using Entities = CleanBlog.Domain.Entities.Identity;
using CleanBlog.Domain.SharedKernel.Enums;

namespace CleanBlog.Application.Commands.User
{
    public class UpdateUserCommand : ICommand, IMapTo<Entities.User>
    {
        public string Name { get; }
        public string Family { get; }
        public byte Age { get; }
        public Gender Gender { get; }

        public UpdateUserCommand() { /* for mapping */ }

        public UpdateUserCommand(
            string name,
            string family,
            byte age,
            Gender gender
            )
        {
            Name = name;
            Family = family;
            Age = age;
            Gender = gender;
        }
    }
}
