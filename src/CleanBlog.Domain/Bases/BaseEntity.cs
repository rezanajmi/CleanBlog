using CleanBlog.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanBlog.Domain.Bases
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        [Required]
        public virtual int Id { get; set; }

        private List<IDomainEvent> _events = new();

        [NotMapped]
        public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

        public void AddEvent(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
