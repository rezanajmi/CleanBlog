using AutoMapper;
using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Events.Category;
using CleanBlog.Application.QueryEntities.Category;
using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Application.EventHandlers.BusEventHandlers
{
    internal class CategoryBusEventHandler :
        IBusEventHandler<CategoryCreatedBusEvent>,
        IBusEventHandler<CategoryUpdatedBusEvent>
    {
        private readonly IAsyncQueryRepository repository;
        private readonly IMapper mapper;

        public CategoryBusEventHandler(IAsyncQueryRepository repo,
            IMapper mapper)
        {
            repository = repo;
            this.mapper = mapper;
        }

        public async Task Handle(CategoryCreatedBusEvent notification, CancellationToken ct)
        {
            await repository.AddAsync(mapper.Map<CategoryQueryEntity>(notification), ct);
        }

        public async Task Handle(CategoryUpdatedBusEvent notification, CancellationToken ct)
        {
            await repository.UpdateAsync(mapper.Map<CategoryQueryEntity>(notification), ct);
        }
    }
}
