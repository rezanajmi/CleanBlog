using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Domain.Bases
{
    public abstract class BaseQueryEntity : IEntity
    {
        public string Id { get; protected set; }
    }
}
