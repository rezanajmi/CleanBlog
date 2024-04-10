using CleanBlog.Application.Abstractions;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.Entities.Category.Events;
using CleanBlog.Domain.Entities.Post.Events;
using CleanBlog.Domain.SharedKernel.Constants;

namespace CleanBlog.Application.EventHandlers.DomainEventHandlers
{
    internal class CachingEventHandlers :
        IDomainEventHandler<CategoryCreatedDomainEvent>,
        IDomainEventHandler<CategoryUpdatedDomainEvent>,
        IDomainEventHandler<PostCreatedDomainEvent>,
        IDomainEventHandler<PostUpdatedDomainEvent>,
        IDomainEventHandler<PostDeletedDomainEvent>,
        IDomainEventHandler<CommentCreatedDomainEvent>
    {
        private readonly ICache _cache;

        public CachingEventHandlers(ICache cache)
        {
            _cache = cache;
        }

        public async Task Handle(CategoryCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _cache.RemoveAllByKeyInfixAsync(CacheKeyInfixes.Category_All);
        }

        public async Task Handle(CategoryUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _cache.RemoveAllByKeyInfixAsync(
                CacheKeyInfixes.Category_All,
                $"{CacheKeyInfixes.Category_Id}{notification.Id}"
                );
        }

        public async Task Handle(PostCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _cache.RemoveAllByKeyInfixAsync(CacheKeyInfixes.Post_All);
        }

        public async Task Handle(PostUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _cache.RemoveAllByKeyInfixAsync(
                CacheKeyInfixes.Post_All,
                $"{CacheKeyInfixes.Post_Id}{notification.Id}"
                );
        }

        public async Task Handle(PostDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _cache.RemoveAllByKeyInfixAsync(
                CacheKeyInfixes.Post_All,
                $"{CacheKeyInfixes.Post_Id}{notification.PostId}"
                );
        }

        public async Task Handle(CommentCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _cache.RemoveAllByKeyInfixAsync(
                CacheKeyInfixes.Comment_All,
                $"{CacheKeyInfixes.Comment_PostId}{notification.PostId}"
                );
        }
    }
}
