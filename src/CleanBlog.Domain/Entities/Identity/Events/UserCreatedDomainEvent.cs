
using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Domain.Entities.Identity.Events
{
    public class UserCreatedDomainEvent : IDomainEvent
    {
        public string Email { get; }

        public UserCreatedDomainEvent(string email)
        {
            Email = email;
        }
    }
}
