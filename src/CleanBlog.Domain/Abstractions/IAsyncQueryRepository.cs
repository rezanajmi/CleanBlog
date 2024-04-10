using CleanBlog.Domain.Bases;
using CleanBlog.Domain.SharedKernel.Models;

namespace CleanBlog.Domain.Abstractions
{
    public interface IAsyncQueryRepository
    {
        Task<TQueryEntity> GetAsync<TQueryEntity>(string id, CancellationToken ct = default)
            where TQueryEntity : BaseQueryEntity;

        Task<TQueryEntity> GetAsync<TQueryEntity>(Specification<TQueryEntity> spec, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity;

        Task<IList<TQueryEntity>> GetListAsync<TQueryEntity>(Specification<TQueryEntity> spec, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity;

        Task<PagedList<TQueryEntity>> GetPagedListAsync<TQueryEntity>(Specification<TQueryEntity> spec = null,
            int page = 1, int pageItemCount = 10, CancellationToken ct = default) where TQueryEntity : BaseQueryEntity;

        Task AddAsync<TQueryEntity>(TQueryEntity entity, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity;

        Task<bool> UpdateAsync<TQueryEntity>(TQueryEntity entity, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity;

        Task<bool> DeleteAsync<TQueryEntity>(TQueryEntity entity, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity;

        Task<bool> DeleteAsync<TQueryEntity>(string id, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity;
    }
}
