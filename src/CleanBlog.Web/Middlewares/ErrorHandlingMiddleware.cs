using CleanBlog.Domain.Bases;
using CleanBlog.Web.Models;
using System.Net;

namespace CleanBlog.Web.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (BaseException baseExc)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new ApiResponse(baseExc.Message, baseExc.Errors));
            }
            catch
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new ApiResponse("Something unexpected occured in the server. Please contact support."));
            }
        }
    }
}