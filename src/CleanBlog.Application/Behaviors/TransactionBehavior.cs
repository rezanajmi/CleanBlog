using CleanBlog.Domain.SharedKernel.Extensions;
using MediatR;
using System.Transactions;

namespace CleanBlog.Application.Behaviors
{
    internal class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.IsCommand())
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.MaximumTimeout
                };

                using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
                    TransactionScopeAsyncFlowOption.Enabled))
                {
                    var response = await next();

                    transaction.Complete();

                    return response;
                }
            }
            else
            {
                return await next();
            }
        }
    }
}
