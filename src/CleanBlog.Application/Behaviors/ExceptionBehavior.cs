using CleanBlog.Domain.Bases;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanBlog.Application.Behaviors
{
    internal class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: IBaseRequest
    {
        private readonly ILogger<TRequest> logger;

        public ExceptionBehavior(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
            )
        {
            try
            {
                return await next();
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception exc)
            {
                logger.LogError(exc, exc.Message);
                throw;
            }
        }
    }
}
