using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Commands.Post;

namespace CleanBlog.Web.Models.Requests.Post
{
    public class AddPostRequest : IMapTo<CreatePostCommand>
    {
        public string title { get; set; }
        public string content { get; set; }
        public int categoryId { get; set; }
    }
}
