using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Domain.Entities.Identity.Events
{
    public class EmailChangedDomainEvent : IDomainEvent
    {
        public string Email { get; }

        public EmailChangedDomainEvent(string email)
        {
            Email = email;
        }
    }
}
