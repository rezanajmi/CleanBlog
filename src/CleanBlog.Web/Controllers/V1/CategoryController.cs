using AutoMapper;
using CleanBlog.Application.Commands.Category;
using CleanBlog.Application.Queries.Category;
using CleanBlog.Web.Bases;
using CleanBlog.Web.Models.Requests.Category;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanBlog.Web.Controllers.V1
{
    [ApiVersion("1.0")]
    public sealed class CategoryController : BaseApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CategoryController(
            IMediator mediator,
            IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList(string searchedTitle = default, CancellationToken ct = default)
        {
            return SendOk(await mediator.Send(
                new GetCategoriesQuery(searchedTitle), ct));
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Create(CreateCategoryRequest request, CancellationToken ct = default)
        {
            await mediator.Send(
                mapper.Map<CreateCategoryCommand>(request), ct);
            return SendCreated("", null);
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Update(UpdateCategoryRequest request, CancellationToken ct = default)
        {
            await mediator.Send(
                mapper.Map<UpdateCategoryCommand>(request), ct);
            return SendNoContent();
        }
    }
}