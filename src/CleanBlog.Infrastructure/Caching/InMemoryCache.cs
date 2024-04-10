using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.SharedKernel.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace CleanBlog.Infrastructure.Caching
{
    internal class InMemoryCache : ICache
    {
        private readonly IMemoryCache memoryCache;

        public InMemoryCache(IMemoryCache mc)
        {
            memoryCache = mc;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (memoryCache.TryGetValue(key, out T value))
            {
                return await Task.FromResult(value);
            }
            return default(T);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            await Task.FromResult(memoryCache.Set(key, value, TimeSpan.FromHours(1)));
        }

        public Task RemoveAsync(string key)
        {
            memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task RemoveAll()
        {
            var keys = memoryCache.GetKeys();

            foreach (var key in keys)
            {
                memoryCache.Remove(key);
            }

            return Task.CompletedTask;
        }

        public Task RemoveAllByKeyPrefixAsync(string keyPerfix)
        {
            var keys = memoryCache.GetKeys()
                .Where(x => x.StartsWith(keyPerfix)).ToList();

            foreach (var key in keys)
            {
                memoryCache.Remove(key);
            }

            return Task.CompletedTask;
        }

        public Task RemoveAllByKeyInfixAsync(params string[] keyInfixs)
        {
            if (keyInfixs is null ||
                keyInfixs.Count() == 0) 
                return default;

            var keys = memoryCache.GetKeys();

            keys = keys.Where(x => { 
                var sections = x.Split('#');
                foreach (var item in keyInfixs)
                {
                    if (sections[1].Contains(item))
                        return true;
                }
                return false;
            }).ToList();

            foreach (var key in keys)
            {
                memoryCache.Remove(key);
            }

            return Task.CompletedTask;
        }
    }
}
