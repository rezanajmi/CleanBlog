using CleanBlog.Application.Abstractions;
using Entities = CleanBlog.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using CleanBlog.Application.Exceptions;

namespace CleanBlog.Application.Queries.User.Handlers
{
    internal class UserQueriesHandler : 
        IQueryHandler<GetUserByCredentialQuery, Guid>
    {
        private readonly UserManager<Entities.User> userManager;

        public UserQueriesHandler(UserManager<Entities.User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Guid> Handle(GetUserByCredentialQuery request, CancellationToken ct)
        {
            var user = await userManager.FindByNameAsync(request.Username);
            if (user is null || await userManager.CheckPasswordAsync(user, request.Password) is false)
            {
                throw new ValidationException("username or password is wrong!");
            }

            return user.Id;
        }
    }
}
