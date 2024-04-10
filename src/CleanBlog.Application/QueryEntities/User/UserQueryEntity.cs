using CleanBlog.Domain.Bases;

namespace CleanBlog.Application.QueryEntities.User
{
    public class UserQueryEntity : BaseQueryEntity
    {
        public string Username { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }

        public UserQueryEntity() { /* for mapping */ }

        public UserQueryEntity(
            string id,
            string username,
            string fullname,
            string email
            )
        {
            Id = id;
            Username = username;
            FullName = fullname;
            Email = email;
        }
    }
}