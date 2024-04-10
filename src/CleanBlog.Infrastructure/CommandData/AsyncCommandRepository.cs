using CleanBlog.Domain.Bases;
using CleanBlog.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using CleanBlog.Domain.SharedKernel.Models;
using CleanBlog.Domain.SharedKernel.Extensions;

namespace CleanBlog.Infrastructure.CommandData
{
    internal class AsyncCommandRepository<TEntity, TKey> : IAsyncCommandRepository<TEntity, TKey>
        where TEntity : BaseEntity where TKey : struct
    {
        private readonly CleanBlogDbContext context;
        public AsyncCommandRepository(CleanBlogDbContext cnx)
        {
            context = cnx;
        }

        public async Task<TEntity> GetAsync(TKey id, CancellationToken ct = default)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Specification<TEntity> specification, CancellationToken ct = default)
        {
            return await specification.Query(context.Set<TEntity>()).FirstOrDefaultAsync(ct);
        }

        public async Task<TRelativeEntity> GetAsync<TRelativeEntity, TRelativeKay>(TRelativeKay id, CancellationToken ct = default) 
            where TRelativeEntity : BaseEntity
        {
            return await context.Set<TRelativeEntity>().FindAsync(id);
        }

        public async Task<IList<TEntity>> GetListAsync(Specification<TEntity> specification = null, CancellationToken ct = default)
        {
            var list = context.Set<TEntity>().AsQueryable();
            if (specification is not null)
            {
                list = specification.Query(list);
            }
            return await list.ToListAsync(ct);
        }

        public async Task<PagedList<TEntity>> GetPagedListAsync(Specification<TEntity> specification = null,
            int page = 1, int pageItemCount = 10, CancellationToken ct = default)
        {
            var list = context.Set<TEntity>().AsQueryable();
            if (specification is not null)
            {
                list = specification.Query(list);
            }
            return await list.ToPagedListAsync(page, pageItemCount, ct);
        }

        public async Task<bool> ExistAsync(Specification<TEntity> specification = null, CancellationToken ct = default)
        {
            return await specification.Query(context.Set<TEntity>()).AnyAsync(ct);
        }

        public async Task<bool> ExistAsync<TRelativeEntity>(Specification<TRelativeEntity> specification = null,
            CancellationToken ct = default) where TRelativeEntity : BaseEntity
        {
            return await specification.Query(context.Set<TRelativeEntity>()).AnyAsync(ct);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            var entry = await context.Set<TEntity>().AddAsync(entity, ct);
            return entry.Entity;
        }

        public void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }

        public async Task DeleteAsync(TKey id, CancellationToken ct = default)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity is not null)
            {
                context.Set<TEntity>().Remove(entity);
            }
        }

        public async Task SaveAsync(CancellationToken ct = default)
        {
            await context.SaveChangesAsync(ct);
        }
    }
}
