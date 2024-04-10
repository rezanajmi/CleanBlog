
namespace CleanBlog.Domain.Abstractions
{
    public interface ICacheManager<TModel>
    {
        TModel Get(string key);
        void Set(string key, TModel model);
        void Delete(string key);
        void ClearAll();
    }
}
