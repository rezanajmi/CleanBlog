using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.SharedKernel.Extensions;
using MediatR;

namespace CleanBlog.Application.Behaviors
{
    internal class EventSourcingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEventSourcing eventSourcing;

        public EventSourcingBehavior(IEventSourcing eventSourcing)
        {
            this.eventSourcing = eventSourcing;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            if (request.IsCommand())
            {
                Type requestType = request.GetType();
                string commandName = requestType.Name;

                var data = new Dictionary<string, object>
                {
                    {
                        "Request", request
                    },
                    {
                        "Response", response
                    }
                };

                await eventSourcing.AppendAsync(commandName, data, cancellationToken);
            }

            return response;
        }
    }
}
