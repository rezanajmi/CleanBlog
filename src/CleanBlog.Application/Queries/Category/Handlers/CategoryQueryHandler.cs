using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Results.Category;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Application.QueryEntities.Category;
using CleanBlog.Application.Specifications.Category;

namespace CleanBlog.Application.Queries.Category.Handlers
{
    internal class CategoryQueryHandler : 
        IQueryHandler<GetCategoriesQuery, IList<GetCategoriesResult>>
    {
        private readonly IAsyncQueryRepository repository;

        public CategoryQueryHandler(IAsyncQueryRepository repo)
        {
            repository = repo;
        }

        public async Task<IList<GetCategoriesResult>> Handle(GetCategoriesQuery request, CancellationToken ct)
        {
            var categories = await repository.GetListAsync<CategoryQueryEntity>(
                new SearchCategoriesByTitleSpec(request.SearchedTitle), ct);
            return categories.Select(c => new GetCategoriesResult(c.Id, c.Title, c.ParentId, c.ParentTitle)).ToList();
        }
    }
}