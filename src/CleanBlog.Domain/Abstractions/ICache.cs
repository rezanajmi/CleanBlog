
namespace CleanBlog.Domain.Abstractions
{
    public interface ICache
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value);
        Task RemoveAsync(string key);
        Task RemoveAll();
        Task RemoveAllByKeyPrefixAsync(string keyPerfix);
        Task RemoveAllByKeyInfixAsync(params string[] keyInfix);
    }
}
