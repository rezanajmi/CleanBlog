
namespace CleanBlog.Domain.Abstractions
{
    public abstract class Specification<TEntity> where TEntity : IEntity
    {
        public Func<IQueryable<TEntity>, IQueryable<TEntity>> Query { get; set; }
    }
}
