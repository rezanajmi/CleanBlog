using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Commands.User;
using CleanBlog.Domain.SharedKernel.Enums;

namespace CleanBlog.Web.Models.Requests.User
{
    public class RegisterUserRequest : IMapTo<CreateUserCommand>
    {
        public string username { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public byte age { get; set; }
        public Gender gender { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }
}