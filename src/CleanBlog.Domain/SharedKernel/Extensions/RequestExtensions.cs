using MediatR;

namespace CleanBlog.Domain.SharedKernel.Extensions
{
    public static class RequestExtensions
    {
        public static bool IsQuery<TResponse>(this IRequest<TResponse> request)
        {
            if (request.GetType().Name.EndsWith("Query")) return true;
            else return false;
        }

        public static bool IsCommand<TResponse>(this IRequest<TResponse> request)
        {
            if (request.GetType().Name.EndsWith("Command")) return true;
            else return false;
        }
    }
}
