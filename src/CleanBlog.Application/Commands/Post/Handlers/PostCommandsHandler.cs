using CleanBlog.Application.Abstractions;
using CleanBlog.Domain.Abstractions;
using Entities = CleanBlog.Domain.Entities;
using CleanBlog.Application.Events.Post;
using CleanBlog.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using CleanBlog.Application.Specifications.Post;

namespace CleanBlog.Application.Commands.Post.Handlers
{
    internal class PostCommandsHandler :
        ICommandHandler<CreatePostCommand, int>,
        ICommandHandler<UpdatePostCommand>,
        ICommandHandler<DeletePostCommand>,
        ICommandHandler<AddCommentInPostCommand>
    {
        private readonly IAsyncCommandRepository<Entities.Post.Post, int> repository;
        private readonly UserManager<Entities.Identity.User> userManager;
        private readonly IBus eventBus;
        private readonly ICurrentUser currentUser;

        public PostCommandsHandler(
            IAsyncCommandRepository<Entities.Post.Post, int> commandRepo,
            UserManager<Entities.Identity.User> userManager,
            IBus eventBus,
            ICurrentUser currentUser)
        {
            repository = commandRepo;
            this.userManager = userManager;
            this.eventBus = eventBus;
            this.currentUser = currentUser;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken ct)
        {
            var searchedPosts = await repository.GetListAsync(new GetPostByTitleSpec(request.Title), ct);
            if (searchedPosts.Any())
            {
                throw new ValidationException("there is a post with same title.");
            }

            Entities.Category.Category category = null;
            if (request.CategoryId > 0)
            {
                category = await repository.GetAsync<Entities.Category.Category, int>(request.CategoryId, ct);
                if (category is null)
                {
                    throw new ValidationException("category id is wrong.");
                }
            }

            var user = await userManager.FindByIdAsync(request.UserId ?? currentUser.Id);
            if (user is null)
            {
                throw new ValidationException("user id is wrong.");
            }

            var post = new Entities.Post.Post(request.Title, request.Content, category.Id, user.Id);

            await repository.AddAsync(post, ct);
            await repository.SaveAsync(ct);

            await eventBus.Publish(new PostCreatedBusEvent(post), ct);

            return post.Id;
        }

        async Task IRequestHandler<UpdatePostCommand>.Handle(UpdatePostCommand request, CancellationToken ct)
        {
            var post = await repository.GetAsync(new GetPostByIdWithCategoryAndUserSpec(request.Id), ct);
            if (post is null)
            {
                throw new ValidationException("updated post not found.");
            }

            var searchedPosts = await repository.GetListAsync(new GetPostByTitleSpec(request.Title), ct);
            if (searchedPosts.Any(s => s.Id != request.Id))
            {
                throw new ValidationException("there is a post with same title.");
            }

            Entities.Category.Category category = null;
            if (request.CategoryId > 0)
            {
                category = await repository.GetAsync<Entities.Category.Category, int>(request.CategoryId, ct);
                if (category is null)
                {
                    throw new ValidationException("category id is wrong.");
                }
            }

            post.Update(request.Title, request.Content, request.CategoryId);

            repository.Update(post);
            await repository.SaveAsync(ct);

            await eventBus.Publish(new PostUpdatedBusEvent(post), ct);
        }

        async Task IRequestHandler<DeletePostCommand>.Handle(DeletePostCommand request, CancellationToken ct)
        {
            var post = await repository.GetAsync(request.Id, ct);
            if (post is null)
            {
                throw new ValidationException("deleted post not found.");
            }

            post.Delete();

            await repository.DeleteAsync(request.Id, ct);
            await repository.SaveAsync(ct);

            await eventBus.Publish(new PostDeletedBusEvent(post.Id, 
                post.Comments.Select(c => c.Id).ToList()), ct);
        }

        async Task IRequestHandler<AddCommentInPostCommand>.Handle(AddCommentInPostCommand request, CancellationToken ct)
        {
            var post = await repository.GetAsync(request.PostId, ct);
            if (post is null)
            {
                throw new ValidationException("Targeted post not found.");
            }

            var user = await userManager.FindByIdAsync(request.UserId ?? currentUser.Id);
            if (user is null)
            {
                throw new ValidationException("user id is wrong.");
            }

            var comment = post.AddComment(request.Text, user.Id);

            repository.Update(post);
            await repository.SaveAsync(ct);

            await eventBus.Publish(new CommentCreatedBusEvent(comment), ct);
        }
    }
}
