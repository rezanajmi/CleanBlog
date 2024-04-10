using CleanBlog.Application.Abstractions;

namespace CleanBlog.Application.Queries.User
{
    public class GetUserByCredentialQuery : IQuery<Guid>
    {
        public string Username { get; }
        public string Password { get; }

        public GetUserByCredentialQuery(
            string username,
            string password
            )
        {
            Username = username;
            Password = password;
        }
    }
}
