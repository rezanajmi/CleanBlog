using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Reflection;

namespace CleanBlog.Domain.SharedKernel.Extensions
{
    public static class MemoryCacheExtensions
    {
        public static IEnumerable<string> GetKeys(this IMemoryCache memoryCache)
        {
            var test = typeof(MemoryCache);
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field.GetValue(memoryCache) as ICollection;
            var keys = new List<string>();
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo.GetValue(item);
                    keys.Add(val.ToString());
                }
            }
            return keys;
        }
    }
}
