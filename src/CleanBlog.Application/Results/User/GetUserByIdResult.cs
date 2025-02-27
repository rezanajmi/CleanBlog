using CleanBlog.Domain.SharedKernel.Enums;

namespace CleanBlog.Application.Results.User
{
    public class GetUserByIdResult
    {
        public Guid Id { get; set; }
        public string Username { get; }
        public string Name { get; }
        public string Family { get; }
        public byte Age { get; }
        public Gender Gender { get; }
        public string Email { get; }

        public GetUserByIdResult(
            Guid id,
            string userName,
            string name,
            string family,
            byte age,
            Gender gender,
            string email
            )
        {
            Id = id;
            Username = userName;
            Name = name;
            Family = family;
            Age = age;
            Gender = gender;
            Email = email;
        }
    }
}
