using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Commands.User;
using CleanBlog.Domain.SharedKernel.Enums;

namespace CleanBlog.Web.Models.Requests.User
{
    public class UpdateUserRequest : IMapTo<UpdateUserCommand>
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public byte Age { get; set; }
        public Gender Gender { get; set; }
    }
}