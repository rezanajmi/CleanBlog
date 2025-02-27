using Asp.Versioning;
using AutoMapper;
using CleanBlog.Application.Commands.User;
using CleanBlog.Application.Queries.User;
using CleanBlog.Web.Bases;
using CleanBlog.Web.Models.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanBlog.Web.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    public sealed class UserController : BaseApiController
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        public UserController(
            IMediator mediator,
            IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("signin")]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInRequest request, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetUserByCredentialQuery(request.username, request.password), ct);
            var generateTokenResult = await mediator.Send(new GenerateJwtTokenCommand(result, request.username), ct);
            return SendOk(new { token = generateTokenResult });
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken ct = default)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return SendBadRequest("You have already registered.");

            var result = await mediator.Send(mapper.Map<CreateUserCommand>(request), ct);
            return SendCreated("", null);
        }

        [HttpGet("validateToken")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> ValidateToken(CancellationToken ct = default)
        {
            return SendOk();
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetByToken(CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetUserByIdQuery());
            return SendOk(result);
        }

        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Update(UpdateUserRequest request, CancellationToken ct = default)
        {
            await mediator.Send(mapper.Map<UpdateUserCommand>(request), ct);
            return SendNoContent();
        }

        [HttpPut("email")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateEmail(UpdateEmailRequest request, CancellationToken ct = default)
        {
            await mediator.Send(mapper.Map<UpdateEmailCommand>(request), ct);
            return SendNoContent();
        }

        [HttpPut("confirmEmail")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request, CancellationToken ct = default)
        {
            await mediator.Send(mapper.Map<ConfirmEmailCommand>(request), ct);
            return SendNoContent();
        }

        [HttpPut("password")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request, CancellationToken ct = default)
        {
            await mediator.Send(mapper.Map<UpdatePasswordCommand>(request), ct);
            return SendNoContent();
        }
    }
}