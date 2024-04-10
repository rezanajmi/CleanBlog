using CleanBlog.Domain.Abstractions;
using MediatR;

namespace CleanBlog.Application.Abstractions
{
    public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
    }
}
