using CleanBlog.Application.Commands.Post;
using FluentValidation;

namespace CleanBlog.Application.Validators.Post
{
    public sealed class AddCommentInPostCommandValidator : AbstractValidator<AddCommentInPostCommand>
    {
        public AddCommentInPostCommandValidator()
        {
            RuleFor(x => x.PostId).GreaterThan(0);
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
