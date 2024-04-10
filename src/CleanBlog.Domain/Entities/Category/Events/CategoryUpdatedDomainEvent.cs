using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Domain.Entities.Category.Events
{
    public class CategoryUpdatedDomainEvent : IDomainEvent
    {
        public int Id { get; }

        public CategoryUpdatedDomainEvent(int id)
        {
            Id = id;
        }
    }
}
