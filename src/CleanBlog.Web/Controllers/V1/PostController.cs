using CleanBlog.Web.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanBlog.Web.Models.Requests.Post;
using CleanBlog.Application.Queries.Post;
using CleanBlog.Application.Commands.Post;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace CleanBlog.Web.Controllers.V1
{
    [ApiVersion("1.0")]
    public sealed class PostController : BaseApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        public PostController(IMediator mediator,
            IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetPostByIdQuery(id), ct);
            return SendOk(result);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList([FromQuery] SearchPostRequest request, CancellationToken ct = default)
        {
            var result = await mediator.Send(mapper.Map<GetPaginatedPostsQuery>(request), ct);
            return SendOk(result);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Create(AddPostRequest request, CancellationToken ct = default)
        {
            var result = await mediator.Send(mapper.Map<CreatePostCommand>(request), ct);
            return SendCreated("", null);
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Update(UpdatePostRequest request, CancellationToken ct = default)
        {
            await mediator.Send(mapper.Map<UpdatePostCommand>(request), ct);
            return SendNoContent();
        }

        [HttpDelete]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
        {
            await mediator.Send(new DeletePostCommand(id), ct);
            return SendNoContent();
        }

        [HttpGet("comment/{postId}")]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCommentsAsync(int postId, CancellationToken ct = default)
        {
            var appResult = await mediator.Send(new GetPostCommentsQuery(postId), ct);
            return SendOk(appResult);
        }

        [HttpPost("comment")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> AddComment(AddCommentRequest request, CancellationToken ct = default)
        {
            await mediator.Send(mapper.Map<AddCommentInPostCommand>(request), ct);
            return SendCreated("", null);
        }
    }
}