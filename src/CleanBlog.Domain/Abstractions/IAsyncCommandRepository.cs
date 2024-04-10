using CleanBlog.Domain.Bases;
using CleanBlog.Domain.SharedKernel.Models;

namespace CleanBlog.Domain.Abstractions
{
    public interface IAsyncCommandRepository<TEntity, TKey> where TEntity : BaseEntity where TKey : struct
    {
        Task<TEntity> GetAsync(TKey id, CancellationToken ct = default);
        Task<TEntity> GetAsync(Specification<TEntity> specification, CancellationToken ct = default);
        Task<TRelativeEntity> GetAsync<TRelativeEntity, TRelativeKay>(TRelativeKay id, CancellationToken ct = default)
            where TRelativeEntity : BaseEntity;
        Task<IList<TEntity>> GetListAsync(Specification<TEntity> specification = null, CancellationToken ct = default);
        Task<PagedList<TEntity>> GetPagedListAsync(Specification<TEntity> specification = null,
            int page = 1, int pageItemCount = 10, CancellationToken ct = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
        void Update(TEntity entity);
        Task DeleteAsync(TKey id, CancellationToken ct = default);
        Task SaveAsync(CancellationToken ct = default);
    }
}
