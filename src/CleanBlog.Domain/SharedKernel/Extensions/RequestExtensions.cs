using MediatR;

namespace CleanBlog.Domain.SharedKernel.Extensions
{
    public static class RequestExtensions
    {
        public static bool IsQuery(this IBaseRequest request)
        {
            if (request.GetType().Name.EndsWith("Query")) return true;
            else return false;
        }

        public static bool IsCommand(this IBaseRequest request)
        {
            if (request.GetType().Name.EndsWith("Command")) return true;
            else return false;
        }
    }
}
