using MediatR;

namespace CleanBlog.Application.Abstractions
{
    internal interface ICommand : IRequest
    {
    }

    internal interface ICommand<TResponse> : IRequest<TResponse>
    {
    }
}
