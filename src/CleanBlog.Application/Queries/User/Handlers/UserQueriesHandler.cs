using CleanBlog.Application.Abstractions;
using Entities = CleanBlog.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using CleanBlog.Application.Exceptions;
using CleanBlog.Application.Results.User;

namespace CleanBlog.Application.Queries.User.Handlers
{
    internal class UserQueriesHandler :
        IQueryHandler<GetUserByCredentialQuery, Guid>,
        IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
    {
        private readonly UserManager<Entities.User> userManager;
        private readonly ICurrentUser currentUser;

        public UserQueriesHandler(UserManager<Entities.User> userManager,
            ICurrentUser currentUser)
        {
            this.userManager = userManager;
            this.currentUser = currentUser;
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

        public async Task<GetUserByIdResult> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            var user = await userManager.FindByIdAsync(currentUser.Id);
            if (user is null)
            {
                throw new ValidationException("user don't find!");
            }

            return new GetUserByIdResult(
                user.Id,
                user.UserName,
                user.Name,
                user.Family,
                user.Age,
                user.Gender,
                user.Email
                );
        }
    }
}
