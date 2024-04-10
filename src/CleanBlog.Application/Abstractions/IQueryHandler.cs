using MediatR;

namespace CleanBlog.Application.Abstractions
{
    internal interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
