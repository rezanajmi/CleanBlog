
namespace CleanBlog.Domain.Bases
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message, IEnumerable<string> errors) : base(message)
        {
            if (_errors is null)
            {
                _errors = new List<string>();
            }
            foreach (var error in errors)
            {
                _errors.Add(error);
            }
        }

        private IList<string> _errors;

        public IList<string> Errors => _errors;

        public void AddError(string error)
        {
            _errors.Add(error);
        }
    }
}
