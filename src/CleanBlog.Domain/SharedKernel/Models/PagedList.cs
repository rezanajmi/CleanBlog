
namespace CleanBlog.Domain.SharedKernel.Models
{
    public class PagedList<T> where T : class
    {
        public IList<T> List { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
