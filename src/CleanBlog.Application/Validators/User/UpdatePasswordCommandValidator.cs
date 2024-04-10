using CleanBlog.Application.Commands.User;
using FluentValidation;

namespace CleanBlog.Application.Validators.User
{
    public sealed class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty().MinimumLength(6);
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6).Equal(x => x.ConfirmNewPassword);
        }
    }
}
