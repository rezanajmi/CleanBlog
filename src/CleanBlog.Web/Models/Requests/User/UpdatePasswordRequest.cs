using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Commands.User;

namespace CleanBlog.Web.Models.Requests.User
{
    public class UpdatePasswordRequest : IMapTo<UpdatePasswordCommand>
    {
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmNewPassword { get; set; }
    }
}