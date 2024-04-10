
namespace CleanBlog.Domain.Abstractions
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(string to, string subject, string content, CancellationToken ct);
    }
}
