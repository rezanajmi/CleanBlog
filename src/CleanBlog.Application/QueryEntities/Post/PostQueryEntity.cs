using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Results.Post;
using CleanBlog.Domain.Bases;

namespace CleanBlog.Application.QueryEntities.Post
{
    public class PostQueryEntity : BaseQueryEntity, 
        IMapTo<SearchedPostResult>, 
        IMapTo<GetPostByIdResult>
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string UserFullName { get; private set; }
        public int CategoryId { get; private set; }
        public string CategoryTitle { get; private set; }

        public PostQueryEntity() { /* for mapping */ }

        public PostQueryEntity(
            string id,
            string title,
            string content,
            string userFullName,
            int categoryId,
            string categoryTitle)
        {
            Id = id;
            Title = title;
            Content = content;
            UserFullName = userFullName;
            CategoryId = categoryId;
            CategoryTitle = categoryTitle;
        }
    }
}
