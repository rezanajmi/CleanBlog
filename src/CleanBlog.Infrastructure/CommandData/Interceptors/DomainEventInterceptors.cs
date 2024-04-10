using CleanBlog.Domain.Bases;
using CleanBlog.Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanBlog.Infrastructure.CommandData.Interceptors
{
    internal class DomainEventInterceptors : SaveChangesInterceptor
    {
        private readonly IMediator mediator;
        public DomainEventInterceptors(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavedChanges(eventData, result);
        }

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context, cancellationToken);
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private async Task DispatchDomainEvents(DbContext context, CancellationToken ct = default)
        {
            if (context == null) return;

            var entities = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.Events.Any())
                .Select(x => x.Entity);

            var userEntity = context.ChangeTracker
                .Entries<User>()
                .Where(x => x.Entity.Events.Any())
                .Select(x => x.Entity);

            var roleEntity = context.ChangeTracker
                .Entries<Role>()
                .Where(x => x.Entity.Events.Any())
                .Select(x => x.Entity);

            var events = entities.SelectMany(x => x.Events).ToList();
            events.AddRange(userEntity.SelectMany(x => x.Events).ToList());
            events.AddRange(roleEntity.SelectMany(x => x.Events).ToList());

            entities.ToList().ForEach(entity => entity.ClearEvents());

            foreach (var item in events)
            {
                await mediator.Publish(item, ct);
            }
        }
    }
}
