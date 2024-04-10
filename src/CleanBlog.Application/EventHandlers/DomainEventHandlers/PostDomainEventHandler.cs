using CleanBlog.Application.Abstractions;
using CleanBlog.Domain.Abstractions;
using Entities = CleanBlog.Domain.Entities;
using CleanBlog.Domain.Entities.Post.Events;
using Microsoft.AspNetCore.Identity;
using CleanBlog.Application.Specifications.Post;

namespace CleanBlog.Application.EventHandlers.DomainEventHandlers
{
    internal class PostDomainEventHandler :
        IDomainEventHandler<PostCreatedDomainEvent>,
        IDomainEventHandler<PostDeletedDomainEvent>
    {
        private readonly IEmailSender emailSender;
        private readonly ICurrentUser currentUser;
        private readonly UserManager<Entities.Identity.User> userManager;
        private readonly IAsyncCommandRepository<Entities.Post.Comment, int> commentRepository;

        public PostDomainEventHandler(IEmailSender emailSender,
            ICurrentUser currentUser,
            UserManager<Entities.Identity.User> userManager,
            IAsyncCommandRepository<Entities.Post.Comment, int> commentRepo)
        {
            this.emailSender = emailSender;
            this.currentUser = currentUser;
            this.userManager = userManager;
            commentRepository = commentRepo;
        }

        public async Task Handle(PostCreatedDomainEvent notification, CancellationToken ct)
        {
            var user = await userManager.FindByIdAsync(currentUser.Id);
            if (user == null || user.EmailConfirmed == false) return;

            var message = $"your new post, \"{notification.Title}\", has been created.";

            await emailSender.SendEmail(user.Email, "CleanBlog New Post", message, ct);
        }

        public async Task Handle(PostDeletedDomainEvent notification, CancellationToken ct)
        {
            var comments = await commentRepository.GetListAsync(new GetCommentsByPostIdSpec(notification.PostId), ct);
            foreach (var comment in comments)
            {
                await commentRepository.DeleteAsync(comment.Id, ct);
            }
            await commentRepository.SaveAsync(ct);
        }
    }
}
