using CleanBlog.Domain.Bases;

namespace CleanBlog.Application.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(IEnumerable<string> errors) : base("There is some validation errors.", errors)
        {
        }

        public ValidationException(string error) : this(new List<string>() { error })
        {
        }
    }
}
