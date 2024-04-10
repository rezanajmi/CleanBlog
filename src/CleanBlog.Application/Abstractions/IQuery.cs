using MediatR;

namespace CleanBlog.Application.Abstractions
{
    internal interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
