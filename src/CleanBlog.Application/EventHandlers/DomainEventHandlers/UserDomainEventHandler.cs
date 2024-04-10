using CleanBlog.Application.Abstractions;
using Entities = CleanBlog.Domain.Entities.Identity;
using CleanBlog.Domain.Entities.Identity.Events;
using Microsoft.AspNetCore.Identity;
using CleanBlog.Domain.Abstractions;
using MediatR;

namespace CleanBlog.Application.EventHandlers.DomainEventHandlers
{
    internal class UserDomainEventHandler :
        IDomainEventHandler<EmailChangedDomainEvent>,
        IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly UserManager<Entities.User> userManager;
        private readonly IEmailSender emailSender;
        private readonly IMediator mediator;

        public UserDomainEventHandler(UserManager<Entities.User> userManager,
            IEmailSender emailSender,
            IMediator mediator)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.mediator = mediator;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken ct)
        {
            await mediator.Publish(new EmailChangedDomainEvent(notification.Email), ct);
        }

        public async Task Handle(EmailChangedDomainEvent notification, CancellationToken ct)
        {
            var user = await userManager.FindByEmailAsync(notification.Email);
            if (user == null)
            {
                return;
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = new UriBuilder("https", "www.cleanblog.com", 0, "api/user/confirmEmail", $"?token={token}");
            var message = $"Confirmation email link:\r\n{confirmationLink}";
            await emailSender.SendEmail(user.Email, "CleanBlog Confirmation Mail", message, ct);
        }
    }
}
