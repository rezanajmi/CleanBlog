using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Commands.User;

namespace CleanBlog.Web.Models.Requests.User
{
    public class ConfirmEmailRequest : IMapTo<ConfirmEmailCommand>
    {
        public string token { get; set; }
    }
}