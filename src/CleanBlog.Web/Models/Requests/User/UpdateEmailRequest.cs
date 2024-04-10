using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Commands.User;

namespace CleanBlog.Web.Models.Requests.User
{
    public class UpdateEmailRequest : IMapTo<UpdateEmailCommand>
    {
        public string Email { get; set; }
    }
}