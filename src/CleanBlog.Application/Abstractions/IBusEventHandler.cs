using CleanBlog.Domain.Abstractions;
using MediatR;

namespace CleanBlog.Application.Abstractions
{
    internal interface IBusEventHandler<in TBusEvent> : INotificationHandler<TBusEvent> where TBusEvent : IBusEvent
    {
    }
}
