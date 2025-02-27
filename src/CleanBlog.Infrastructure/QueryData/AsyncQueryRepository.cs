using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.Bases;
using CleanBlog.Domain.SharedKernel.Extensions;
using CleanBlog.Domain.SharedKernel.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CleanBlog.Infrastructure.QueryData
{
    internal class AsyncQueryRepository : IAsyncQueryRepository
    {
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;

        public AsyncQueryRepository(IOptions<MongoDbSettings> options)
        {
            mongoClient = new MongoClient(options.Value.ConnectionString);
            database = mongoClient.GetDatabase(options.Value.DatabaseName);
        }

        public async Task<TQueryEntity> GetAsync<TQueryEntity>(string id, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity
        {
            var data = await database.GetCollection<TQueryEntity>(typeof(TQueryEntity).Name).FindAsync(x => x.Id == id);
            return await data.FirstOrDefaultAsync(ct);
        }

        public async Task<TQueryEntity> GetAsync<TQueryEntity>(Specification<TQueryEntity> spec, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity
        {
            var data = database.GetCollection<TQueryEntity>(typeof(TQueryEntity).Name).AsQueryable();
            if (spec is not null)
            {
                data = spec.Query(data);
            }
            return await data.FirstOrDefaultAsync(ct);
        }

        public async Task<IList<TQueryEntity>> GetListAsync<TQueryEntity>(Specification<TQueryEntity> spec, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity
        {
            var data = database.GetCollection<TQueryEntity>(typeof(TQueryEntity).Name).AsQueryable();
            if (spec is not null)
            {
                data = spec.Query(data);
            }
            return await data.ToListAsync(ct);
        }

        public async Task<PagedList<TQueryEntity>> GetPagedListAsync<TQueryEntity>(Specification<TQueryEntity> spec = null,
            int page = 1, int pageItemCount = 10, CancellationToken ct = default) where TQueryEntity : BaseQueryEntity
        {
            var data = database.GetCollection<TQueryEntity>(typeof(TQueryEntity).Name).AsQueryable();
            if (spec is not null)
            {
                data = spec.Query(data);
            }
            return await Task.FromResult(data.ToPagedList(page, pageItemCount));
        }

        public async Task AddAsync<TQueryEntity>(TQueryEntity entity, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity
        {
            await database.GetCollection<TQueryEntity>(typeof(TQueryEntity).Name).InsertOneAsync(entity, ct);
        }

        public async Task<bool> UpdateAsync<TQueryEntity>(TQueryEntity entity, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity
        {
            var result = await database.GetCollection<TQueryEntity>(typeof(TQueryEntity).Name).ReplaceOneAsync(x => x.Id == entity.Id, entity);
            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync<TQueryEntity>(TQueryEntity entity, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity
        {
            var result = await database.GetCollection<TQueryEntity>(typeof(TQueryEntity).Name).DeleteOneAsync(x => x.Id == entity.Id);
            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync<TQueryEntity>(string id, CancellationToken ct = default) 
            where TQueryEntity : BaseQueryEntity
        {
            var result = await database.GetCollection<TQueryEntity>(typeof(TQueryEntity).Name).DeleteOneAsync(x => x.Id == id);
            return result.IsAcknowledged;
        }
    }
}