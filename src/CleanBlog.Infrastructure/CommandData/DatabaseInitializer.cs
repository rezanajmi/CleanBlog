using CleanBlog.Application.Commands.Category;
using CleanBlog.Application.Commands.Post;
using CleanBlog.Application.Commands.User;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanBlog.Infrastructure.CommandData
{
    internal class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly CleanBlogDbContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IMediator mediator;

        public DatabaseInitializer(
            CleanBlogDbContext context,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMediator mediator)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mediator = mediator;
        }

        public async Task MigrateAsync()
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await context.Database.MigrateAsync();
            }
        }

        public async Task SeedDataAsync()
        {
            if (context.Posts.Any() == false)
            {
                await roleManager.CreateAsync(new Role("Admin"));

                var userId = await mediator.Send(new CreateUserCommand(
                    "rezanajmi",
                    "Reza",
                    "Najmi",
                    30,
                    Domain.SharedKernel.Enums.Gender.Man,
                    "rezanajmi72@gmail.com",
                    "rN123@",
                    "rN123@"
                    ));

                var adminUser = await userManager.FindByNameAsync("rezanajmi");

                if (adminUser is not null)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                var itCategory = await mediator.Send(new CreateCategoryCommand("IT", 0));

                var smartPhoneCategory = await mediator.Send(new CreateCategoryCommand("SmartPhones", itCategory));

                var createdFirstPostResult = await mediator.Send(new CreatePostCommand(
                    "First Post", 
                    "This is first post in this web application",
                    itCategory,
                    userId.ToString()
                    ));

                await mediator.Send(new AddCommentInPostCommand(
                    createdFirstPostResult,
                    "This is the first comment int this web application.",
                    userId.ToString()
                    ));

                await mediator.Send(new AddCommentInPostCommand(
                    createdFirstPostResult,
                    "This is the second comment int this web application.",
                    userId.ToString()
                    ));

                await mediator.Send(new CreatePostCommand(
                    "Second Post",
                    "This is second post in this web application",
                    smartPhoneCategory,
                    userId.ToString()
                    ));
            }
        }
    }
}
