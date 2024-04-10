
namespace CleanBlog.Domain.SharedKernel.Constants
{
    public static class CacheKeyInfixes
    {
        public const string Category_All = $"{nameof(Entities.Category.Category)}:all";
        public const string Category_Id = $"{nameof(Entities.Category.Category)}:";

        public const string Post_All = $"{nameof(Entities.Post.Post)}:all";
        public const string Post_Id = $"{nameof(Entities.Post.Post)}:";

        public const string Comment_All = $"{nameof(Entities.Post.Comment)}:all";
        public const string Comment_PostId = $"{nameof(Entities.Post.Comment)}:";
    }
}
