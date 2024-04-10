using CleanBlog.Application.Commands.User;
using FluentValidation;

namespace CleanBlog.Application.Validators.User
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Family).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Gender).IsInEnum();
            RuleFor(x => x.Age).GreaterThan((byte)10).LessThan((byte)120);
        }
    }
}
