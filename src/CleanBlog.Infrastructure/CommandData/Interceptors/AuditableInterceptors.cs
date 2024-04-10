using CleanBlog.Application.Abstractions;
using CleanBlog.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanBlog.Infrastructure.CommandData.Interceptors
{
    internal class AuditableInterceptors : SaveChangesInterceptor
    {
        private readonly DateTime dateTime;
        private readonly ICurrentUser currentUser;
        public AuditableInterceptors(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
            dateTime = DateTime.Now;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntityAuditableInfo(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntityAuditableInfo(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntityAuditableInfo(DbContext context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.SetAuditInfoOnAdd(dateTime, currentUser.Id);
                        break;
                    case EntityState.Modified:
                        entry.Entity.SetAuditInfoOnModify(dateTime, currentUser.Id);
                        break;
                    case EntityState.Deleted:
                        if (entry.Entity is ISoftDelete)
                        {
                            entry.State = EntityState.Modified;
                            entry.Entity.SetAuditInfoOnDelete(dateTime, currentUser.Id);
                            var entity = entry.Entity as ISoftDelete;
                            entity.SetDeleted();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
