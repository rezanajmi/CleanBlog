
namespace CleanBlog.Domain.SharedKernel.Models
{
    public sealed class PagingInfo
    {
        public PagingInfo(int totalItemsCount, int page, int pageItemCount)
        {
            this.Page = page > 0 ? page : 1;
            this.PageItemCount = pageItemCount > 0 ? pageItemCount : 10;
            this.TotalItemsCount = totalItemsCount;
        }

        public int Page { get; }
        public int PageItemCount { get; }
        public int TotalItemsCount { get; }
        public int PageCount => TotalItemsCount % PageItemCount == 0 ? TotalItemsCount / PageItemCount : (TotalItemsCount / PageItemCount) + 1;
        public int RowStart => ((Page * PageItemCount) - PageItemCount) + 1;
        public bool HasPrevPage => Page > 1;
        public bool HasNextPage => PageCount > Page;
    }
}
