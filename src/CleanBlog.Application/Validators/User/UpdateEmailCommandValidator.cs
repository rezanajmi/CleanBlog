using CleanBlog.Application.Commands.User;
using FluentValidation;

namespace CleanBlog.Application.Validators.User
{
    public sealed class UpdateEmailCommandValidator : AbstractValidator<UpdateEmailCommand>
    {
        public UpdateEmailCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        }
    }
}
