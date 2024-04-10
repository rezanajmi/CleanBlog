
namespace CleanBlog.Application.Results.Category
{
    public class GetCategoriesResult
    {
        public object Id { get; set; }
        public string Title { get; set; }
        public string ParentTitle { get; set; }

        public GetCategoriesResult(object id, string title, string parentTile)
        {
            Id = id;
            Title = title;
            ParentTitle = parentTile;
        }
    }
}
