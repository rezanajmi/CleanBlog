using CleanBlog.Application.Commands.Post;
using FluentValidation;

namespace CleanBlog.Application.Validators.Post
{
    public sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5).MaximumLength(200);
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.CategoryId).GreaterThan(0);
        }
    }
}
