using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Commands.Post;

namespace CleanBlog.Web.Models.Requests.Post
{
    public class AddCommentRequest : IMapTo<AddCommentInPostCommand>
    {
        public int postId { get; set; }
        public string text { get; set; }
    }
}