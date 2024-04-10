using CleanBlog.Application.Commands.User;
using FluentValidation;

namespace CleanBlog.Application.Validators.User
{
    public sealed class GenerateJwtTokenCommandValidator : AbstractValidator<GenerateJwtTokenCommand>
    {
        public GenerateJwtTokenCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Identity).NotEmpty();
        }
    }
}
