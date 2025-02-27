using CleanBlog.Application.Abstractions;
using CleanBlog.Domain.Abstractions;
using Entities = CleanBlog.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using MediatR;
using CleanBlog.Application.Exceptions;
using CleanBlog.Application.Events.User;
using System.Reflection.Metadata;

namespace CleanBlog.Application.Commands.User.Handlers
{
    internal class UserCommandsHandler :
        ICommandHandler<GenerateJwtTokenCommand, string>,
        ICommandHandler<CreateUserCommand, Guid>,
        ICommandHandler<UpdateUserCommand>,
        ICommandHandler<UpdateEmailCommand>,
        ICommandHandler<ConfirmEmailCommand>,
        ICommandHandler<UpdatePasswordCommand>
    {
        private readonly IJwtService jwtService;
        private readonly UserManager<Entities.User> userManager;
        private readonly IMapper mapper;
        private readonly ICurrentUser currentUser;
        private readonly IBus eventBus;

        public UserCommandsHandler(
            IJwtService jwtService,
            UserManager<Entities.User> userManager,
            IMapper mapper,
            ICurrentUser currentUser,
            IBus eventBus)
        {
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.currentUser = currentUser;
            this.eventBus = eventBus;
        }

        public async Task<string> Handle(GenerateJwtTokenCommand request, CancellationToken ct)
        {
            return await Task.FromResult(jwtService.GenerateToken(request.Identity, request.Username));
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken ct)
        {
            var user = mapper.Map<Entities.User>(request);

            user.Create();
            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded is false)
            {
                throw new ValidationException(identityResult.Errors.Select(e => e.Description));
            }

            await eventBus.Publish(new UserCreatedBusEvent(user), ct);
            return user.Id;
        }

        async Task IRequestHandler<UpdateUserCommand>.Handle(UpdateUserCommand request, CancellationToken ct)
        {
            var identityUser = await userManager.FindByIdAsync(currentUser.Id);
            if (identityUser is null)
            {
                throw new ValidationException("user could not be found.");
            }

            mapper.Map(request, identityUser);

            var identityResult = await userManager.UpdateAsync(identityUser);
            if (identityResult.Succeeded is false)
            {
                throw new ValidationException(identityResult.Errors.Select(e => e.Description));
            }

            await eventBus.Publish(new UserUpdatedBusEvent(identityUser), ct);
        }

        async Task IRequestHandler<UpdateEmailCommand>.Handle(UpdateEmailCommand request, CancellationToken ct)
        {
            var identityUser = await userManager.FindByIdAsync(currentUser.Id);
            if (identityUser is null)
            {
                throw new ValidationException("user could not be found.");
            }

            identityUser.UpdateEmail(request.Email);
            var identityResult = await userManager.SetEmailAsync(identityUser, request.Email);

            if (identityResult.Succeeded is false)
            {
                throw new ValidationException(identityResult.Errors.Select(e => e.Description));
            }
        }

        async Task IRequestHandler<ConfirmEmailCommand>.Handle(ConfirmEmailCommand request, CancellationToken ct)
        {
            var identityUser = await userManager.FindByIdAsync(currentUser.Id);
            if (identityUser is null)
            {
                throw new ValidationException("user could not be found.");
            }

            var identityResult = await userManager.ConfirmEmailAsync(identityUser, request.Token);
            if (identityResult.Succeeded is false)
            {
                throw new ValidationException(identityResult.Errors.Select(e => e.Description));
            }

            await eventBus.Publish(new UserUpdatedBusEvent(identityUser), ct);
        }

        async Task IRequestHandler<UpdatePasswordCommand>.Handle(UpdatePasswordCommand request, CancellationToken ct)
        {
            var identityUser = await userManager.FindByIdAsync(currentUser.Id);
            if (identityUser is null)
            {
                throw new ValidationException("user could not be found.");
            }

            var identityResult = await userManager.ChangePasswordAsync(identityUser, request.CurrentPassword, request.NewPassword);
            if (identityResult.Succeeded is false)
            {
                throw new ValidationException(identityResult.Errors.Select(e => e.Description));
            }
        }
    }
}
