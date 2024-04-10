using CleanBlog.Application.Abstractions;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.Entities.Category;
using CleanBlog.Domain.Entities.Identity;
using CleanBlog.Domain.Entities.Post;
using CleanBlog.Infrastructure.CommandData.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using System.Reflection;

namespace CleanBlog.Infrastructure.CommandData
{
    internal class CleanBlogDbContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IConfiguration configuration;
        private readonly ICurrentUser currentUser;
        private readonly IMediator mediator;

        public CleanBlogDbContext(
            DbContextOptions<CleanBlogDbContext> options,
            IConfiguration configuration,
            ICurrentUser currentUser,
            IMediator mediator) : base(options)
        {
            this.configuration = configuration;
            this.currentUser = currentUser;
            this.mediator = mediator;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CleanBlogMainDB"));

            optionsBuilder.AddInterceptors(
                new AuditableInterceptors(currentUser),
                new DomainEventInterceptors(mediator)
                );

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            AddSoftDeleteFilter(modelBuilder.Model.GetEntityTypes());

            base.OnModelCreating(modelBuilder);
        }

        private void AddSoftDeleteFilter(IEnumerable<IMutableEntityType> mutableEntityTypes)
        {
            Expression<Func<ISoftDelete, bool>> filter = x => x.IsDeleted == false;

            foreach (var item in mutableEntityTypes)
            {
                if (item.ClrType.IsAssignableTo(typeof(ISoftDelete)))
                {
                    var parameter = Expression.Parameter(item.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filter.Parameters.First(), parameter, filter.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    item.SetQueryFilter(lambdaExpression);
                }
            }
        }
    }
}
