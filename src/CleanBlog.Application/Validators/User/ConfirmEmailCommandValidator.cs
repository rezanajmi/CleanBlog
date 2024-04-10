using CleanBlog.Application.Commands.User;
using FluentValidation;

namespace CleanBlog.Application.Validators.User
{
    public sealed class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
