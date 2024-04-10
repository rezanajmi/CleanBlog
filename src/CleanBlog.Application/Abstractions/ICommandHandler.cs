using MediatR;

namespace CleanBlog.Application.Abstractions
{
    internal interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> 
        where TCommand : ICommand
    {
    }

    internal interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult> 
        where TCommand : ICommand<TResult>
    {
    }
}
