using CleanBlog.Application.Commands.Post;
using FluentValidation;

namespace CleanBlog.Application.Validators.Post
{
    public sealed class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5).MaximumLength(200);
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.CategoryId).GreaterThan(0);
        }
    }
}
