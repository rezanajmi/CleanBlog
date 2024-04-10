using CleanBlog.Application.Abstractions;
using CleanBlog.Domain.Abstractions;
using Entities = CleanBlog.Domain.Entities.Category;
using CleanBlog.Application.Events.Category;
using CleanBlog.Application.Exceptions;
using MediatR;
using CleanBlog.Application.Specifications.Category;

namespace CleanBlog.Application.Commands.Category.Handlers
{
    internal class CategoryCommandHandler : 
        ICommandHandler<CreateCategoryCommand, int>,
        ICommandHandler<UpdateCategoryCommand>
    {
        private readonly IAsyncCommandRepository<Entities.Category, int> repository;
        private readonly IBus eventBus;

        public CategoryCommandHandler(
            IAsyncCommandRepository<Entities.Category, int> repo,
            IBus eventBus)
        {
            repository = repo;
            this.eventBus = eventBus;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken ct)
        {
            var category = await repository.GetAsync(new GetCategoryByTitleSpec(request.Title), ct);
            if (category is not null)
            {
                throw new ValidationException("there is a category by same title.");
            }

            Entities.Category parentCategory = null;
            if (request.ParentId.HasValue)
            {
                parentCategory = await repository.GetAsync(request.ParentId.Value, ct);
                if (parentCategory is null)
                {
                    throw new ValidationException("parent category id is wrong.");
                }
            }

            category = new Entities.Category(request.Title, request.ParentId);

            await repository.AddAsync(category, ct);
            await repository.SaveAsync(ct);

            eventBus.Publish(new CategoryCreatedBusEvent(category));

            return category.Id;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken ct)
        {
            var category = await repository.GetAsync(request.Id, ct);
            if (category is null)
            {
                throw new ValidationException($"there is not any category with {request.Id} id.");
            }

            Entities.Category parentCategory = null;
            if (request.ParentId.HasValue)
            {
                parentCategory = await repository.GetAsync(request.ParentId.Value, ct);
                if (parentCategory is null)
                {
                    throw new ValidationException("parent category id is wrong.");
                }
            }

            category.Update(request.Title, request.ParentId);

            repository.Update(category);
            await repository.SaveAsync(ct);

            eventBus.Publish(new CategoryUpdatedBusEvent(category));

            return await Unit.Task;
        }
    }
}
