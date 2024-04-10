using CleanBlog.Domain.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CleanBlog.Infrastructure.Caching
{
    internal class RedisCache : ICache
    {
        private readonly IDistributedCache cache;
        private readonly IConnectionMultiplexer connection;

        public RedisCache(IDistributedCache cache, IConnectionMultiplexer connection)
        {
            this.cache = cache;
            this.connection = connection;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await cache.GetStringAsync(key);
            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default(T);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };
            await cache.SetStringAsync(key, JsonConvert.SerializeObject(value), options);
        }

        public async Task RemoveAsync(string key)
        {
            await cache.RemoveAsync(key);
        }

        public async Task RemoveAll()
        {
            var endpoints = connection.GetEndPoints();
            var server = connection.GetServer(endpoints[0]);

            await server.FlushAllDatabasesAsync();
        }

        public async Task RemoveAllByKeyPrefixAsync(string keyPerfix)
        {
            var endpoints = connection.GetEndPoints();
            var server = connection.GetServer(endpoints[0]);

            var keys = server.Keys()
                .Where(x => x.ToString().StartsWith(keyPerfix)).ToList();

            foreach (var key in keys)
            {
                await cache.RemoveAsync(key);
            }
        }

        public async Task RemoveAllByKeyInfixAsync(params string[] keyInfixs)
        {
            var endpoints = connection.GetEndPoints();
            var server = connection.GetServer(endpoints[0]);

            var keys = server.Keys();

            keys = keys.Where(x => {
                var sections = x.ToString().Split('#');
                foreach (var item in keyInfixs)
                {
                    if (sections[1].Contains(item))
                        return true;
                }
                return false;
            }).ToList();

            foreach (var key in keys)
            {
                await cache.RemoveAsync(key);
            }
        }
    }
}
