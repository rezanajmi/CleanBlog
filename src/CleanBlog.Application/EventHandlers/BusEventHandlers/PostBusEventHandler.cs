using AutoMapper;
using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Events.Post;
using CleanBlog.Application.QueryEntities.Post;
using CleanBlog.Domain.Abstractions;

namespace CleanBlog.Application.EventHandlers.BusEventHandlers
{
    internal class PostBusEventHandler :
        IBusEventHandler<PostCreatedBusEvent>,
        IBusEventHandler<PostUpdatedBusEvent>,
        IBusEventHandler<PostDeletedBusEvent>,
        IBusEventHandler<CommentCreatedBusEvent>
    {
        private readonly IAsyncQueryRepository repository;
        private readonly IMapper mapper;

        public PostBusEventHandler(IAsyncQueryRepository repo,
            IMapper mapper)
        {
            repository = repo;
            this.mapper = mapper;
        }

        public async Task Handle(PostCreatedBusEvent notification, CancellationToken ct)
        {
            await repository.AddAsync(mapper.Map<PostQueryEntity>(notification), ct);
        }

        public async Task Handle(PostUpdatedBusEvent notification, CancellationToken ct)
        {
            await repository.UpdateAsync(mapper.Map<PostQueryEntity>(notification), ct);
        }

        public async Task Handle(PostDeletedBusEvent notification, CancellationToken ct)
        {
            await repository.DeleteAsync<PostQueryEntity>(notification.PostId.ToString(), ct);
            foreach (var item in notification.CommentsId)
            {
                await repository.DeleteAsync<CommentQueryEntity>(item.ToString(), ct);
            }
        }

        public async Task Handle(CommentCreatedBusEvent notification, CancellationToken ct)
        {
            await repository.AddAsync(mapper.Map<CommentQueryEntity>(notification), ct);
        }
    }
}
