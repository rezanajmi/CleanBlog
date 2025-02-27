
namespace CleanBlog.Application.Results.Post
{
    public class SearchedPostResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserFullName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
    }
}
