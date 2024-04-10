using CleanBlog.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanBlog.Infrastructure.CommandData.Configurations
{
    internal class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(p => p.Content)
                .IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
