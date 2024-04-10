using CleanBlog.Application.Commands.Category;
using FluentValidation;

namespace CleanBlog.Application.Validators.Category
{
    public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.ParentId).GreaterThanOrEqualTo(0);
        }
    }
}
