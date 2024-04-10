using CleanBlog.Application.Commands.Category;
using FluentValidation;

namespace CleanBlog.Application.Validators.Category
{
    public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.ParentId).Must(x => x.HasValue ? x.Value > 0 : true);
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
