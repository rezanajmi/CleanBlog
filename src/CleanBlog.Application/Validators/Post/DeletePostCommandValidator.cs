using CleanBlog.Application.Commands.Post;
using FluentValidation;

namespace CleanBlog.Application.Validators.Post
{
    public sealed class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
