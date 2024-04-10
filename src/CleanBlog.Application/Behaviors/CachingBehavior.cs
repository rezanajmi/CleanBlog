using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.SharedKernel.Utilities;
using CleanBlog.Domain.SharedKernel.Extensions;
using MediatR;

namespace CleanBlog.Application.Behaviors
{
    internal class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICache cache;

        public CachingBehavior(ICache cache)
        {
            this.cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request.IsQuery())
            {
                if (request is IUseCache)
                {
                    var cacheResult = await cache.GetAsync<TResponse>(StringUtility.GenerateChacheKeyWithObject(request));
                    if (cacheResult is not null)
                    {
                        return cacheResult;
                    }
                }

                var result = await next();

                if (request is IUseCache)
                {
                    await cache.SetAsync(StringUtility.GenerateChacheKeyWithObject(request), result);
                }

                return result;
            }
            else
            {
                return await next();
            }
        }
    }
}
