using AutoMapper;
using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Events.User;
using CleanBlog.Application.QueryEntities.User;
using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Application.EventHandlers.BusEventHandlers
{
    internal class UserBusEventHandler :
        IBusEventHandler<UserCreatedBusEvent>,
        IBusEventHandler<UserUpdatedBusEvent>
    {
        private readonly IAsyncQueryRepository repository;
        private readonly IMapper mapper;

        public UserBusEventHandler(IAsyncQueryRepository repo,
            IMapper mapper)
        {
            repository = repo;
            this.mapper = mapper;
        }

        public async Task Handle(UserCreatedBusEvent notification, CancellationToken ct)
        {
            await repository.AddAsync(mapper.Map<UserQueryEntity>(notification), ct);
        }

        public async Task Handle(UserUpdatedBusEvent notification, CancellationToken ct)
        {
            await repository.UpdateAsync(mapper.Map<UserQueryEntity>(notification), ct);
        }
    }
}