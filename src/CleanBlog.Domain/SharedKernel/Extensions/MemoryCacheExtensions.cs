using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Reflection;

namespace CleanBlog.Domain.SharedKernel.Extensions
{
    public static class MemoryCacheExtensions
    {
        public static IEnumerable<string> GetKeys(this IMemoryCache memoryCache)
        {
            var coherentState = typeof(MemoryCache).GetField("_coherentState", BindingFlags.NonPublic | BindingFlags.Instance);
            var coherentStatevalue = coherentState.GetValue(memoryCache);
            var stringEntries = coherentStatevalue.GetType().GetProperty("StringEntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var nonStringEntries = coherentStatevalue.GetType().GetProperty("NonStringEntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);

            var stringEntriesCollection = stringEntries.GetValue(coherentStatevalue) as ICollection;
            var nonStringEntriesCollection = nonStringEntries.GetValue(coherentStatevalue) as ICollection;

            var keys = new List<string>();

            if (stringEntriesCollection is not null && stringEntriesCollection.Count > 0)
            {
                foreach (var item in stringEntriesCollection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo.GetValue(item);
                    keys.Add(val.ToString());
                }
            }

            if (nonStringEntriesCollection is not null && nonStringEntriesCollection.Count > 0)
            {
                foreach (var item in nonStringEntriesCollection)
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
