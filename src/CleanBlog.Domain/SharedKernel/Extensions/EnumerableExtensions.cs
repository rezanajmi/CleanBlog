using CleanBlog.Domain.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanBlog.Domain.SharedKernel.Extensions
{
    public static class EnumerableExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> list, int page, int pageItemCount) 
            where T : class
        {
            return new PagedList<T>()
            {
                PagingInfo = new PagingInfo(list.Count(), page, pageItemCount),
                List = list.Skip((page - 1) * pageItemCount).Take(pageItemCount).ToList()
            };
        }

        public async static Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> list,
            int page, int pageItemCount, CancellationToken ct) where T : class
        {
            return new PagedList<T>()
            {
                PagingInfo = new PagingInfo(list.Count(), page, pageItemCount),
                List = await list.Skip((page - 1) * pageItemCount).Take(pageItemCount).ToListAsync()
            };
        }
    }
}
