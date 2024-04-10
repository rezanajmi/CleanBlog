
namespace CleanBlog.Web.Models
{
    public class ApiResponse
    {
        public bool Succeeded { get; } = true;
        public string message { get; } = null;
        public IList<string> errors { get; } = null;
        public object data { get; } = null;

        public ApiResponse(string message = default, IList<string> errors = default)
        {
            this.Succeeded = false;
            this.message = message;
            this.errors = errors;
        }

        public ApiResponse(object data)
        {
            this.data = data;
        }
    }
}