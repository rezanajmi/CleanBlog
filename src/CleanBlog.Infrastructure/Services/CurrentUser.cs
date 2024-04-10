using CleanBlog.Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace CleanBlog.Infrastructure.Services
{
    internal class CurrentUser : ICurrentUser
    {
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            Id = httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            Username = httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
        } 

        public string Id { get; }
        public Guid Identity { get { return Guid.Parse(Id); } }
        public string Username { get; }
    }
}
