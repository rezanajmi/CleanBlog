
namespace CleanBlog.Domain.Abstractions
{
    public interface IDatabaseInitializer
    {
        Task MigrateAsync();
        Task SeedDataAsync();
    }
}
